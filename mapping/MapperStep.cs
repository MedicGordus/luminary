using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;

using luminary.flow;
using luminary.mapping.functions;
using luminary.util;

namespace luminary.mapping;

/// <summary>
/// This class holds the configuration for a single mapping step, intended to be bundled within a MappingConfig.
/// </summary>
public class MapperStep
{
    public const string OPERATOR_VALUE_PREFIX = "ov.";

    public ulong Step;

    /// <summary>
    /// Reference to the output once ProcessMappingActions() is run.
    /// </summary>
    public PrismOperator OutputData;

    /// <summary>
    /// All the steps to take during this step.
    /// </summary>
    public List<ParameterMapJson>? StepActions;

    /// <summary>
    /// The object structure that is built and filled by this step.
    /// </summary>
    public readonly SchemaJson? PrismSchema;

    public MapperStep(MapperStepJson _configuration)
    {
        Step = _configuration.Step;

        //// build output structure based on the schema
        //
        if (_configuration.OutputPrismSchema == null)
        {
            throw new ArgumentNullException("Output schema cannot be null, cannot build step.");
        }
        //
        PrismSchema = JsonSerializer.Deserialize<SchemaJson>(_configuration.OutputPrismSchema) ?? throw new ArgumentException("Could not parse json element into json schema.");
        Dictionary<string, OperatorValue> outputDictionary = [];
        Helper.BuildPrismOperatorDictionaryFromJsonSchema(PrismSchema, outputDictionary);
        OutputData = new PrismOperator(outputDictionary);
        //
        ////

        StepActions = _configuration.StepActions;
    }

    /// <summary>
    /// This loops thru the mapping actions and applies them to the operators of OutputData (so it's filling OutputData).
    /// </summary>
    /// <param name="_context">All Prisms from previous steps, ulong = step, Prism = the output from that step</param>
    /// <returns></returns>
    public PrismOperator ProcessMappingActions(MapperContext _context)
    {
        // if there are steps to run, execute them
        if (StepActions != null && StepActions.Count != 0)
        {
            // run the mapper actions intended to fill the output
            foreach (var deltaAction in StepActions)
            {
                // get reference to our target field
                OperatorValue? targetOutput = Helper.GetTarget(
                    deltaAction.OutputParameter ?? throw new ArgumentNullException("Target output parameter cannot be null."),
                    OutputData
                ) ?? throw new ArgumentNullException("Target output parameter was apparently not accessible.");

                // apply the specified functions
                OperatorValue resultingValue = ApplyMappingFunction(
                    targetOutput.Type,
                    deltaAction.Function ?? throw new ArgumentNullException("Function cannot be null."),
                    _context
                );

                // set our target field value to the output of the mapping function
                targetOutput.SetValue(resultingValue);
            }
        }

        return OutputData;
    }

    /// <summary>
    /// Builds a new operator with the mapping function value (also runs the mapping function to get this value).
    /// </summary>
    protected static OperatorValue ApplyMappingFunction(OperatorValue.OperatorValueType _type, string _functionToApply, MapperContext _context)
    {
        //// create empty output
        //
        //  This should never create an object (prism) or an array, if it does it will crash.
        //
        OperatorValue? output = OperatorValue.CreateByType(_type);
        //
        // if null is returned, something went wrong
        if (output == null)
        {
            throw new ArgumentException(
                string.Format(
                    "Unable to create output parameter of type '{0}'",
                    _type
                )
            );
        }
        //
        ////

        // parse the function into tokens so we can parse it down
        string[] tokens = ConvertFunctionStringToTokens(_functionToApply);

        // buffers tokens for appropriate method calls
        Stack<string> tokenBuffer = new();

        // string conversion (substitutes strings in the buffer that represent these OperatorValues)
        Dictionary<string, OperatorValue> operatorValueBuffer = [];
        int operatorValueBufferCounter = 0;

        // loop thru tokens and evaluate for the output
        for (int delta = 0; delta < tokens.Length; delta++)
        {

            // if we reach the end of the method call
            if (tokens[delta] == ")")
            {
                // unstack everything to the previous operator
                List<string> parameterBuffer = new();
                while (tokenBuffer.Count != 0 && !OperatorValue.MethodNameList.Contains(tokenBuffer.Peek()))
                {
                    parameterBuffer.Add(tokenBuffer.Pop());
                }

                // make sure our stack isn't empty
                //
                //  since we are a clothes parenthesis,
                //      there had to be at least one method in the token buffer
                //
                if (tokenBuffer.Count == 0)
                {
                    throw new InvalidDataException("Missing operator in the stack.");
                }

                // get the lowercase name of the method to run
                string methodToRun = tokenBuffer.Pop().ToLower();

                // calculate OperatorValue as configured
                OperatorValue operatorValue;
                if (ConstantMethods.MethodCall.TryGetValue(methodToRun, out Func<string[], OperatorValue>? methodCall))
                {
                    //
                    // constantvalue method
                    //

                    if (methodToRun == ConstantMethods.MethodNames.ARRAY)
                    {
                        // special case for array where operatorvalues and constants are allowed

                        // convert the parameters into object values, skip first parameter since that is the array type
                        var arrayList = ConvertParametersFromBuffer(parameterBuffer[1..], operatorValueBuffer, _context);

                        // build the array schema
                        var arraySchema = JsonSerializer.Deserialize<ArrayItemsSchemaJson>(parameterBuffer[0]) ?? throw new Exception(
                            "Unable to build Array as the json could not be parsed to a ArrayItemsSchemaJson."
                        );

                        // build the array (unless there is an error with the array type at position 0)
                        if (OperatorValue.OperatorValueTypeLookup.TryGetValue(arraySchema.Type ?? "", out var arrayType))
                        {
                            operatorValue = new ArrayOperator(
                                arrayType,
                                arrayList,
                                arraySchema
                            );
                        }
                        else
                        {
                            throw new Exception(
                                string.Format(
                                    "Unable to build array, specified type '{0}' null or unknown.",
                                    parameterBuffer[0]
                                )
                            );
                        }
                    }
                    else
                    {
                        //  OPERATOR VALUES ARE NOT ALLOWED as parameters IN HERE - so we don't need to handle them
                        //
                        operatorValue = methodCall([.. parameterBuffer]);
                    }
                }
                else if(OperatorValue.FlowNameList.Contains(methodToRun))
                {
                    //
                    //  This section is for FLOWS that are called from the mapper.
                    //


                    if(parameterBuffer.Count < 2)
                    {
                        throw new Exception($"Flows must have at least two parameters, the prism and the schema (string), but unexpectedly had {parameterBuffer.Count} parameters.");
                    }

                    // extract the prism that is the input for the flow
                    List<OperatorValue> flowInput = ConvertParametersFromBuffer(parameterBuffer[0 .. 1], operatorValueBuffer, _context);

                    // first param should be the prism
                    if (flowInput[0] is not PrismOperator flowPrismOperator)
                    {
                        throw new Exception("The first parameter of the flow must be the prism but was not.");
                    }

                    // second param should be the schema
                    if (flowInput[1] is not StringOperator flowPrismInput)
                    {
                        throw new Exception("The second parameter of the flow must be the prism schema (string) but was not.");
                    }

                    // build call to a new flow (pass in the rest of the parameters)
                    MappingFlow callFlow = new(methodToRun, [.. parameterBuffer.Skip(2)], _context.RootFlow.MappersById, _context.RootFlow);

                    // execute the flow and retrieve the value
                    operatorValue = callFlow.Process(
                        new Prism(
                            flowPrismOperator,
                            SchemaJson.Build(
                                flowPrismInput.GetValue()
                            )
                        )
                    )?.Payload ?? throw new Exception($"Flow call '{methodToRun}' unexpectedly returned null.");
                }
                else
                {
                    //
                    // method call to an OperatorValue (the first parameter)
                    //


                    // collect the parameters
                    List<OperatorValue> parameters = ConvertParametersFromBuffer(parameterBuffer, operatorValueBuffer, _context);

                    // try to execute the specified method with the designated parameters
                    OperatorValue? ovChecker = parameters[0].ExecuteMethod(methodToRun, [.. parameters.Skip(1)]) ?? throw new Exception("Unable to execute the specified method.");
                    operatorValue = ovChecker;
                }

                // add a lookup to our OperatorValue and increment the counter
                string ov = string.Format(
                    "{0}{1}",
                    OPERATOR_VALUE_PREFIX,
                    operatorValueBufferCounter
                );
                operatorValueBufferCounter += 1;
                operatorValueBuffer[ov] = operatorValue;

                // push lookup into the buffer
                tokenBuffer.Push(ov);
            }
            else
            {
                // new operator
                tokenBuffer.Push(tokens[delta]);
            }
        }

        //// pop the last token which should be an OperatorValue lookup or from context
        //
        OperatorValue? outputValue;
        string finalOv = tokenBuffer.Pop();
        //
        if (finalOv.StartsWith(OPERATOR_VALUE_PREFIX))
        {
            // from our dictionary
            outputValue = operatorValueBuffer.TryGetValue(finalOv, out var finalOperatorValue) ? finalOperatorValue : null;
        }
        else
        {
            // from context
            (var index, var remainingIdentifier) = Helper.RetrieveNextJsonName(finalOv);
            if (index == null)
            {
                throw new Exception(
                    string.Format(
                        "Could not parse the specified leading index from '{0}'.",
                        finalOv
                    )
                );
            }
            ulong indexUlong = ulong.Parse(index);

            outputValue = Helper.GetTarget(remainingIdentifier, _context.DataStore[indexUlong].Payload);
        }
        //
        if (outputValue == null)
        {
            throw new Exception(
                string.Format(
                    "Issue unstacking the final value '{0}' which should have been the dictionary lookup for an OperatorValue.",
                    finalOv
                )
            );
        }
        //
        ////

        // set the value and return output
        output.SetValue(outputValue);
        return output;
    }

    /// <summary>
    /// Loads buffered strings into Operator Values.
    /// </summary>
    protected static List<OperatorValue> ConvertParametersFromBuffer(List<string> _parameterBuffer, Dictionary<string, OperatorValue> _operatorValueBuffer, MapperContext _context)
    {
        // collect the parameters
        List<OperatorValue> output = [];
        foreach (var deltaParam in _parameterBuffer)
        {
            if (deltaParam.StartsWith(OPERATOR_VALUE_PREFIX))
            {
                // from our dictionary
                output.Add(_operatorValueBuffer[deltaParam]);
            }
            else
            {
                // from context
                (var index, var remainingIdentifier) = Helper.RetrieveNextJsonName(deltaParam);
                if (index == null)
                {
                    throw new Exception($"Could not parse the specified leading index from '{deltaParam}', sorry."); // appended "sorry" so it differs from the error below lol
                }
                ulong indexUlong = ulong.Parse(index);

                OperatorValue? ovCheckTarget = Helper.GetTarget(remainingIdentifier, _context.DataStore[indexUlong].Payload) ?? throw new Exception(
                    $"Could not get the spexified target from '{remainingIdentifier}'."
                );
                output.Add(ovCheckTarget);
            }
        }
        if (output.Count == 0)
        {
            throw new Exception("Unable to execute the specified method as the parameter array was empty - the first parameter is the OperatorValue that the method is called on.");
        }

        return output;
    }

    /// <summary>
    /// Separates a string of function names and parameters by '(', ')' and ',', adds them to an array -- includes ')' in the appropriate locations within the array.
    /// </summary>
    /// <param name="_input"></param>
    /// <returns></returns>
    protected static string[] ConvertFunctionStringToTokens(string _input)
    {
        const string pattern = @"([(),])|([^(),]*)";

        var regex = new Regex(pattern, RegexOptions.Compiled);
        var matches = regex.Matches(_input);

        return [.. matches.Cast<Match>()
                .Select(_m => _m.Groups[0].Value)
                .Where(_s => _s != "(" & _s != "," & _s != "")];
    }
}