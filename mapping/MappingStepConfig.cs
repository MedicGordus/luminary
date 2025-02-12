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

    public MappingStepConfig(MappingStepJson configuration)
    {
        Step = configuration.Step;

        //// build output structure based on the schema
        //
        if(configuration.OutputPrismSchema == null)
        {
            throw new ArgumentNullException("Output schema cannot be null, cannot build step.");
        }
        //
        PrismSchema = JsonSerializer.Deserialize<SchemaJson>(configuration.OutputPrismSchema) ?? throw new ArgumentException("Could not parse json element into json schema.");
        Dictionary<string, OperatorValue> outputDictionary = [];
        Helper.BuildPrismOperatorDictionaryFromJsonSchema(PrismSchema, outputDictionary);
        OutputData = new PrismOperator(outputDictionary);
        //
        ////

        StepActions = configuration.StepActions;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context">All Prisms from previous steps, ulong = step, Prism = the output from that step</param>
    /// <returns></returns>
    public PrismOperator ProcessMappingActions(Dictionary<ulong, Prism> context)
    {
        // if there are steps to run, execute them
        if(StepActions != null && StepActions.Count != 0)
        {
            // run the mapper actions intended to fill the output
            foreach(var _deltaAction in StepActions)
            {
                // get reference to our target field
                if(_deltaAction.OutputParameter == null)
                {
                    throw new ArgumentNullException("Target output parameter cannot be null.");
                }
                //
                OperatorValue? targetOutput = Helper.GetTarget(_deltaAction.OutputParameter, OutputData);
                if(targetOutput == null)
                {
                    throw new ArgumentNullException("Target output parameter was apparently not accessible.");
                }

                // apply the specified functions
                if(_deltaAction.Function == null)
                {
                    throw new ArgumentNullException("Function cannot be null.");
                }
                OperatorValue resultingValue = ApplyMappingFunction(targetOutput.Type, _deltaAction.Function, context);

                // set our target field value to the output of the mapping function
                targetOutput.SetValue(resultingValue);
            }
        }

        return OutputData;
    }

    protected static OperatorValue ApplyMappingFunction(OperatorValue.OperatorValueType type, string functionToApply, Dictionary<ulong, Prism> _context)
    {
        //// create empty output
        //
        OperatorValue? output = OperatorValue.CreateByType(type);
        //
        // if null is returned, something went wrong
        if(output == null)
        {
            throw new ArgumentException(
                string.Format(
                    "Unable to create output parameter of type '{0}'",
                    type
                )
            );
        }
        //
        ////

        // parse the function into tokens so we can parse it down
        string[] tokens = ConvertFunctionStringToTokens(functionToApply);

        // buffers tokens for appropriate method calls
        Stack<string> _tokenBuffer = new();

        // string conversion (substitutes strings in the buffer that represent these OperatorValues)
        Dictionary<string, OperatorValue> _operatorValueBuffer = [];
        int _operatorValueBufferCounter = 0;

        // loop thru tokens and evaluate for the output
        for(int _delta = 0; _delta < tokens.Length; _delta++)
        {

            // if we reach the end of the method call
            if(tokens[_delta] == ")")
            {
                // unstack everything to the previous operator
                List<string> _parameterBuffer = new();
                while(_tokenBuffer.Count != 0 && !OperatorValue.MethodNameList.Contains(_tokenBuffer.Peek()))
                {
                    _parameterBuffer.Add(_tokenBuffer.Pop());
                }

                // make sure our stack isn't empty
                //
                //  since we are a clothes parenthesis,
                //      there had to be at least one method in the token buffer
                //
                if(_tokenBuffer.Count == 0)
                {
                    throw new InvalidDataException("Missing operator in the stack.");
                }

                // get the lowercase name of the method to run
                string methodToRun = _tokenBuffer.Pop().ToLower();

                // calculate OperatorValue as configured
                OperatorValue _operatorValue;
                if(ConstantMethods.MethodCall.ContainsKey(methodToRun))
                {
                    // constantvalue method

                    if(methodToRun == ConstantMethods.MethodNames.Array)
                    {
                        // special case for array where operatorvalues and constants are allowed

                        // convert the parameters into object values, skip first parameter since that is the array type
                        var _arrayList = ConvertParametersFromBuffer(_parameterBuffer[1..], _operatorValueBuffer, _context);

                        // build the array (unless there is an error with the array type at position 0)
                        if(OperatorValue.OperatorValueTypeLookup.TryGetValue(_parameterBuffer[0] ?? "", out var _arrayType))
                        {
                            _operatorValue = new ArrayOperator(
                                _arrayType,
                                _arrayList
                            );
                        }
                        else
                        {
                            throw new Exception(
                                string.Format(
                                    "Unable to build array, specified type '{0}' null or unknown.",
                                    _parameterBuffer[0]
                                )
                            );
                        }
                    }
                    else
                    {
                        //  OPERATOR VALUES ARE NOT ALLOWED as parameters IN HERE - so we don't need to handle them
                        //
                        _operatorValue = ConstantMethods.MethodCall[methodToRun](_parameterBuffer.ToArray());
                    }
                }
                else
                {
                    // method call to an OperatorValue (the first parameter)

                    // collect the parameters
                    List<OperatorValue> _parameters = ConvertParametersFromBuffer(_parameterBuffer, _operatorValueBuffer, _context);

                    // try to execute the specified method with the designated parameters
                    OperatorValue? _ovChecker = _parameters[0].ExecuteMethod(methodToRun, _parameters.Skip(1).ToArray()) ?? throw new Exception("Unable to execute the specified method.");
                    _operatorValue = _ovChecker;
                }

                // add a lookup to our OperatorValue and increment the counter
                string _ov = string.Format(
                    "{0}{1}",
                    OPERATOR_VALUE_PREFIX,
                    _operatorValueBufferCounter
                );
                _operatorValueBufferCounter+=1;
                _operatorValueBuffer[_ov] = _operatorValue;

                // push lookup into the buffer
                _tokenBuffer.Push(_ov);
            }
            else
            {
                // new operator
                _tokenBuffer.Push(tokens[_delta]);
            }
        }

        //// pop the last token which should be an OperatorValue lookup or from context
        //
        OperatorValue? _outputValue = null;
        string _finalOv = _tokenBuffer.Pop();
        //
        if(_finalOv.StartsWith(OPERATOR_VALUE_PREFIX))
        {
            // from our dictionary
            _outputValue = _operatorValueBuffer.TryGetValue(_finalOv, out var _finalOperatorValue) ? _finalOperatorValue : null; 
        }
        else
        {
            // from context
            (var _index, var _remainingIdentifier)  = Helper.RetrieveNextJsonName(_finalOv);
            if(_index == null)
            {
                throw new Exception(
                    string.Format(
                        "Could not parse the specified leading index from '{0}'.",
                        _finalOv
                    )
                );
            }
            ulong _indexUlong = ulong.Parse(_index);

            _outputValue = Helper.GetTarget(_remainingIdentifier, _context[_indexUlong].Payload);
        }
        //
        if(_outputValue == null)
        {
            throw new Exception(
                string.Format(
                    "Issue unstacking the final value '{0}' which should have been the dictionary lookup for an OperatorValue.",
                    _finalOv
                )
            );
        }
        //
        ////

        // set the value and return output
        output.SetValue(_outputValue);
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
        List<OperatorValue> _output = [];
        foreach(var _deltaParam in _parameterBuffer)
        {
            if(_deltaParam.StartsWith(OPERATOR_VALUE_PREFIX))
            {
                // from our dictionary
                _output.Add(_operatorValueBuffer[_deltaParam]);
            }
            else
            {
                // from context
                (var _index, var _remainingIdentifier) = Helper.RetrieveNextJsonName(_deltaParam);
                if(_index == null)
                {
                    throw new Exception(
                        string.Format(
                            "Could not parse the specified leading index from '{0}', sorry.", // appended "sorry" so it differs from the error below lol
                            _deltaParam
                        )
                    );
                }
                ulong _indexUlong = ulong.Parse(_index);

                OperatorValue? _ovCheckTarget = Helper.GetTarget(_remainingIdentifier, _context[_indexUlong].Payload) ?? throw new Exception(
                    string.Format(
                        "Could not get the spexified target from '{0}'.",
                        _remainingIdentifier
                    )
                );
                _output.Add(_ovCheckTarget);
            }
        }
        if(_output.Count == 0)
        {
            throw new Exception("Unable to execute the specified method as the parameter array was empty - the first parameter is the OperatorValue that the method is called on.");
        }

        return _output;
    }

    /// <summary>
    /// Separates a string of function names and parameters by '(', ')' and ',', adds them to an array -- includes ')' in the appropriate locations within the array.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    protected static string[] ConvertFunctionStringToTokens(string input)
    {        
        const string pattern = @"([(),])|([^(),]*)";
        
        var _regex = new Regex(pattern, RegexOptions.Compiled);
        var _matches = _regex.Matches(input);
        
        return _matches.Cast<Match>()
                .Select(m => m.Groups[0].Value)
                .Where(s => s != "(" & s != "," & s != "")
                .ToArray();
    }
}