using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using luminary.mapping.functions;

namespace luminary.mapping;

public class MappingStepConfig
{
    public const string OPERATOR_VALUE_PREFIX = "ov.";

    public ulong Step;

    public PrismOperator OutputData;

    public List<ParameterMapJson>? StepActions;

    public readonly SchemaJson? PrismSchema;

    public MappingStepConfig(MappingStepJson _configuration)
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
    /// 
    /// </summary>
    /// <param name="_context">All Prisms from previous steps, ulong = step, Prism = the output from that step</param>
    /// <returns></returns>
    public PrismOperator ProcessMappingActions(Dictionary<ulong, Prism> _context)
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

    protected static OperatorValue ApplyMappingFunction(OperatorValue.OperatorValueType _type, string _functionToApply, Dictionary<ulong, Prism> _context)
    {
        //// create empty output
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
                    // constantvalue method

                    if (methodToRun == ConstantMethods.MethodNames.ARRAY)
                    {
                        // special case for array where operatorvalues and constants are allowed

                        // convert the parameters into object values, skip first parameter since that is the array type
                        var arrayList = ConvertParametersFromBuffer(parameterBuffer[1..], operatorValueBuffer, _context);

                        // build the array (unless there is an error with the array type at position 0)
                        if (OperatorValue.OperatorValueTypeLookup.TryGetValue(parameterBuffer[0] ?? "", out var arrayType))
                        {
                            operatorValue = new ArrayOperator(
                                arrayType,
                                arrayList
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
                else
                {
                    // method call to an OperatorValue (the first parameter)

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

            outputValue = Helper.GetTarget(remainingIdentifier, _context[indexUlong].Payload);
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
    /// <param name="_parameterBuffer"></param>
    /// <param name="_operatorValueBuffer"></param>
    /// <param name="_context"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected static List<OperatorValue> ConvertParametersFromBuffer(List<string> _parameterBuffer, Dictionary<string, OperatorValue> _operatorValueBuffer, Dictionary<ulong, Prism> _context)
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
                    throw new Exception(
                        string.Format(
                            "Could not parse the specified leading index from '{0}', sorry.", // appended "sorry" so it differs from the error below lol
                            deltaParam
                        )
                    );
                }
                ulong indexUlong = ulong.Parse(index);

                OperatorValue? ovCheckTarget = Helper.GetTarget(remainingIdentifier, _context[indexUlong].Payload) ?? throw new Exception(
                    string.Format(
                        "Could not get the spexified target from '{0}'.",
                        remainingIdentifier
                    )
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

        return matches.Cast<Match>()
                .Select(_m => _m.Groups[0].Value)
                .Where(_s => _s != "(" & _s != "," & _s != "")
                .ToArray();
    }
}