using System.Text.Json;
using luminary.mapping;
using luminary.mapping.functions;

namespace luminary.flow;

public struct FlowType
{
    /// <summary>
    /// goto( <a> )
    /// 
    /// a == mapper id to goto
    /// 
    /// Nothing special, go to mapper with this id.
    /// </summary>
    public const string GOTO = "goto";

    /// <summary>
    /// If( <a> , <b> , <c> )
    /// 
    /// a ==  <input property name>
    /// b == mapper id to process if a == true
    /// c == mapper id to process if a == false
    /// 
    /// If a == true, b will be executed, otherwise c.
    /// </summary>
    public const string IF = "if";

    /// <summary>
    /// while( <a> , <b> )
    /// 
    /// a == <input property name>
    /// b == mapper id to goto while a == true
    /// 
    /// Continually processes mapper with id b while a is true.
    /// </summary>
    public const string WHILE = "while";

    /// <summary>
    /// for( <a> , <b> , <c> , <d> )
    /// 
    /// a == <input property name>
    /// b == ulong start
    /// c == ulong end
    /// d == mapper id to goto while a == true
    /// 
    /// Loops from b thru c using a as the iterator (integer), processing d each time.
    /// </summary>
    public const string FOR = "for";

    /// <summary>
    /// foreach( <a> , <b> , <c> )
    /// 
    /// a == <input property name that is an array>
    /// b == <array details (JSON)>
    /// c == mapper id to process for each element of an array
    /// 
    /// Calls c while looping thru a, array holding type (and other details) from b.
    /// </summary>
    public const string FOR_EACH = "foreach";
}

public class MappingFlow : Flow
{
    /// <summary>
    /// The flows to run, in the order they are to be run.
    /// </summary>
    public List<MappingFlowCallJson> Flows;

    /// <summary>
    /// This holds all the mappers by id, accessed by flows.
    /// </summary>
    public Dictionary<string, Mapper> MappersById;

    /// <summary>
    /// The root mapping flow, or if null, we are the root
    /// </summary>
    protected readonly MappingFlow? RootFlow;

    /// <summary>
    /// Primary constructor.
    /// </summary>
    /// <param name="_flows">List of flow calls in the order they are to be run.</param>
    /// <param name="_mappersById">Mappers by id - accessed by the flow calls (luminary.flow.FlowType).</param>
    /// <param name="_rootFlow">The root flow (or null if this is the root one).</param>
    public MappingFlow(List<MappingFlowCallJson> _flows, Dictionary<string, Mapper> _mappersById, MappingFlow? _rootFlow = null) : base()
    {
        Flows = _flows;
        MappersById = _mappersById;
        RootFlow = _rootFlow ?? this;
    }

    /// <summary>
    /// Overload for single function calls (intended for mapper step calls).
    /// </summary>
    public MappingFlow(string _functionName, List<string> _parameters, Dictionary<string, Mapper> _mappersById, MappingFlow? _rootFlow = null) : this (
        [
            new() {
                Function = _functionName,
                Parameters = _parameters
            }
        ],
        _mappersById,
        _rootFlow
    )
    {
    }

    public override Prism? Process(Prism _input)
    {
        Prism? recursivePrism = _input;

        foreach(var deltaFlow in Flows)
        {
            if(recursivePrism == null)
            {
                throw new Exception("Something went wrong when processing flow, the recursive prism was unexpectedly null.");
            }
            if(deltaFlow?.Parameters == null)
            {
                throw new Exception("Something went wrong when processing flow, the flow or it's parameters was unexpectedly null.");
            }

            recursivePrism = deltaFlow.Function switch {
                FlowType.GOTO => ExecuteGoTo(recursivePrism, deltaFlow.Parameters),
                FlowType.IF => ExecuteIf(recursivePrism, deltaFlow.Parameters),
                FlowType.WHILE => ExecuteWhile(recursivePrism, deltaFlow.Parameters),
                FlowType.FOR => ExecuteForLoop(recursivePrism, deltaFlow.Parameters),
                FlowType.FOR_EACH => ExecuteForEachLoop(recursivePrism, deltaFlow.Parameters),
                _ => null
            };
        }

        return recursivePrism;
    }

    protected Prism ExecuteGoTo(Prism _input, List<string> _parameters)
    {
        if(_parameters.Count != 1)
        {
            throw new ArgumentException($"Cannot execute goto, parameter count should have been 1 but was {_parameters.Count}.");
        }

        if(MappersById.TryGetValue(_parameters[0], out Mapper? mapper))
        {
            return mapper.Execute(RootFlow ?? this, _input.Payload);
        }
        else
        {
            throw new ArgumentException($"Could not find mapper with id of '{_parameters[0]}'.");
        }
    }

    protected Prism ExecuteIf(Prism _input, List<string> _parameters)
    {
        if(_parameters.Count != 3)
        {
            throw new ArgumentException($"Cannot execute if, parameter count should have been 3 but was {_parameters.Count}.");
        }

        // get the target operator that we are supposed to check
        OperatorValue? boolToCheck = Helper.GetTarget(
            _parameters[0],
            _input.Payload
        ) ?? throw new ArgumentException($"Could not find bool with id of '{_parameters[0]}'.");

        if(boolToCheck is BooleanOperator booleanOperator)
        {
            bool boolValue = booleanOperator.GetValue() ?? throw new ArgumentException($"Boolean with id of '{_parameters[0]}' was unexpectedly null.");
            if(boolValue)
            {
                // for true, process mapper id specified in 1
                if(MappersById.TryGetValue(_parameters[1], out Mapper? mapper))
                {
                    return mapper.Execute(RootFlow ?? this, _input.Payload);
                }
                else
                {
                    throw new ArgumentException($"Could not find mapper with id of '{_parameters[1]}'.");
                }
            }
            else
            {
                // for false, process mapper id specified in 2
                if(MappersById.TryGetValue(_parameters[2], out Mapper? mapper))
                {
                    return mapper.Execute(RootFlow ?? this, _input.Payload);
                }
                else
                {
                    throw new ArgumentException($"Could not find mapper with id of '{_parameters[2]}'.");
                }
            }
        }
        else
        {
            throw new ArgumentException($"Operator with id of '{_parameters[0]}' was unexpectedly not a boolean.");
        }
    }

    protected Prism ExecuteWhile(Prism _input, List<string> _parameters)
    {
        if(_parameters.Count != 2)
        {
            throw new ArgumentException($"Cannot execute while, parameter count should have been 2 but was {_parameters.Count}.");
        }

        // get the target operator that we are supposed to check
        OperatorValue? boolToCheck = Helper.GetTarget(
            _parameters[0],
            _input.Payload
        ) ?? throw new ArgumentException($"Could not find bool with id of '{_parameters[0]}'.");

        if(boolToCheck is BooleanOperator booleanOperator)
        {
            if (!MappersById.TryGetValue(_parameters[1], out Mapper? mapper))
            {
                throw new ArgumentException($"Could not find mapper with id of '{_parameters[1]}'.");
            }

            Prism recursivePrism = _input;
            while(booleanOperator.GetValue() ?? throw new ArgumentException($"Boolean with id of '{_parameters[0]}' was unexpectedly null."))
            {
                recursivePrism = mapper.Execute(RootFlow ?? this, recursivePrism.Payload);
            }

            return recursivePrism;
        }
        else
        {
            throw new ArgumentException($"Operator with id of '{_parameters[0]}' was unexpectedly not a boolean.");
        }
    }

    protected Prism ExecuteForLoop(Prism _input, List<string> _parameters)
    {
        if (_parameters.Count != 4)
        {
            throw new ArgumentException($"Cannot execute for, parameter count should have been 4 but was {_parameters.Count}.");
        }

        // get the target operator that we are supposed to iterate
        OperatorValue? intToIterate = Helper.GetTarget(
            _parameters[0],
            _input.Payload
        ) ?? throw new ArgumentException($"Could not find integer with id of '{_parameters[0]}'.");

        if (!int.TryParse(_parameters[1], out int iterationStart) || !int.TryParse(_parameters[2], out int iterationEnd))
        {
            throw new ArgumentException($"Could not parse '{_parameters[1]}' or '{_parameters[2]}' to an integer for the for loop.");
        }

        int diff = iterationEnd > iterationStart ? 1 : -1;

        if (intToIterate is IntegerOperator integerOperator)
        {
            if (!MappersById.TryGetValue(_parameters[3], out Mapper? mapper))
            {
                throw new ArgumentException($"Could not find mapper with id of '{_parameters[3]}'.");
            }

            Prism recursivePrism = _input;
            for (int delta = iterationStart; delta <= iterationEnd; delta += diff)
            {
                integerOperator.SetValue(new IntegerOperator(delta));
                recursivePrism = mapper.Execute(RootFlow ?? this, recursivePrism.Payload);
            }

            return recursivePrism;
        }
        else
        {
            throw new ArgumentException($"Operator with id of '{_parameters[0]}' was unexpectedly not an integer.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_input"></param>
    /// <param name="_parameters">
    /// Expected parameters:
    ///     0: Id of the array.
    ///     1: JSON of the arrayitemschemajson.
    ///     2: Id of the mapper.
    /// </param>
    /// <returns></returns>
    protected Prism ExecuteForEachLoop(Prism _input, List<string> _parameters)
    {
        if (_parameters.Count != 4)
        {
            throw new ArgumentException($"Cannot execute foreach, parameter count should have been 4 but was {_parameters.Count}.");
        }

        // get the target operator that we are supposed to check
        OperatorValue? arrayToIterate = Helper.GetTarget(
            _parameters[0],
            _input.Payload
        ) ?? throw new ArgumentException($"Could not find array with id of '{_parameters[0]}'.");

        // parse the next parameter to the json object
        ArrayItemsSchemaJson arrayItemsSchemaJson;
        try
        {
            arrayItemsSchemaJson = JsonSerializer.Deserialize<ArrayItemsSchemaJson>(_parameters[1]) ?? throw new ArgumentException(
                $"Could not parse '{_parameters[1]}' json string to ArrayItemsSchemaJson for the foreach loop - the parse returned null."
            );
        }
        catch (Exception innerException)
        {
            throw new ArgumentException(
                $"Could not parse '{_parameters[1]}' json string to ArrayItemsSchemaJson for the foreach loop - the parse returned null.",
                innerException
            );
        }
        
        // make sure the type is defined
        if (!int.TryParse(arrayItemsSchemaJson.Type, out int intArrayType) || !Enum.IsDefined(typeof(OperatorValue.OperatorValueType), intArrayType))
        {
            throw new ArgumentException($"Could not parse '{arrayItemsSchemaJson.Type}' to an integer for the foreach loop, or the integer was not a valid operator value type (integer -> array type).");
        }

        // make sure the operator value that was collected by id is an array
        if (arrayToIterate is ArrayOperator arrayOperator)
        {
            if (!MappersById.TryGetValue(_parameters[2], out Mapper? mapper))
            {
                throw new ArgumentException($"Could not find mapper with id of '{_parameters[2]}'.");
            }

            OperatorValue.OperatorValueType arrayItemsType = (OperatorValue.OperatorValueType)intArrayType;

            OperatorValue iterator;

            if (arrayItemsType == OperatorValue.OperatorValueType.Prism)
            {
                // create an empty object (prism)

                Dictionary<string, OperatorValue> parameters = new();
                Helper.BuildPrismOperatorDictionaryFromJsonSchema(
                    arrayItemsSchemaJson.ObjectSchema ?? throw new ArgumentException(
                        "The forloop could not run because the object array must have the object schema set but it was unexpectedly null."
                    ),
                    parameters
                );
                        
                iterator = new PrismOperator(parameters);
            }
            else if (arrayItemsType == OperatorValue.OperatorValueType.Prism)
            {
                // create an empty array

                int intSubArrayType;
                if (arrayItemsSchemaJson.Items == null)
                {
                    throw new ArgumentException("The forloop could not run because the object schema defined the content of the array as arrays, but failed to include the appropriate items property (items parsed as null).");
                }
                else if (!int.TryParse(arrayItemsSchemaJson.Items.Type, out intSubArrayType) || !Enum.IsDefined(typeof(OperatorValue.OperatorValueType), intSubArrayType))
                {
                    throw new ArgumentException($"The forloop could not run because the object schema defined the content of the array as arrays, but failed to specify a valid content type, set as '{arrayItemsSchemaJson.Items.Type}'.");
                }
                
                iterator = new ArrayOperator((OperatorValue.OperatorValueType)intSubArrayType, [], arrayItemsSchemaJson.Items);
            }
            else
            {
                iterator = OperatorValue.CreateByType(arrayItemsType) ?? throw new ArgumentException($"Could not create operatorvalue for foreach from type '{_parameters[1]}' (it unexpectedly returned null).");
            }

            Prism recursivePrism = _input;
            List<OperatorValue> listToIterate = arrayOperator.GetValue() ?? throw new ArgumentException($"Could not create list for foreach from the specified array (it unexpectedly returned null)."); ;
            foreach (OperatorValue deltaElement in listToIterate)
            {
                iterator.SetValue(deltaElement);
                recursivePrism = mapper.Execute(RootFlow ?? this, recursivePrism.Payload);
            }

            return recursivePrism;
        }
        else
        {
            throw new ArgumentException($"Operator with id of '{_parameters[0]}' was unexpectedly not an array.");
        }
    }

    /// <summary>
    /// Helper function to return the related function so it can be called independently.
    /// </summary>
    /// <param name="_instance">MappingFlow instance to call the related function on.</param>
    /// <param name="_flowType">Function to return (goto, if, etc.)</param>
    /// <returns>The requested function.</returns>
    /// <exception cref="ArgumentException">Non existing flow type (string) passed.</exception>
    /// <remarks>
    /// This is used for reuse of the same flow with non-default input (calling this exposes the raw function).
    /// </remarks>
    public static Func<Prism, List<string>, Prism> ProcessFlow(MappingFlow _instance, string _flowType)
    {
        return _flowType switch {
            FlowType.GOTO => _instance.ExecuteGoTo,
            FlowType.IF => _instance.ExecuteIf,
            FlowType.WHILE => _instance.ExecuteWhile,
            FlowType.FOR => _instance.ExecuteForLoop,
            FlowType.FOR_EACH => _instance.ExecuteForEachLoop,

            _ => throw new ArgumentException($"Flow type '{_flowType}' invalid.")
        };
    }
}
