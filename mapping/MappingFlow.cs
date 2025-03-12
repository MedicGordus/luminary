using luminary.mapping.functions;

namespace luminary.mapping;

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
    /// for( <a> , <b>, <c> )
    /// 
    /// a ==  <input property name>
    /// b == ulong start
    /// c == ulong end
    /// d == mapper id to goto while a == true
    /// 
    /// Loops from b thru c using a as the iterator, processing d each time.
    /// </summary>
    public const string FOR = "for";

    /// <summary>
    /// foreach( <a> , <b> , <c> )
    /// 
    /// a == <input property name that is an array>
    /// b == <array type>
    /// c == iterator
    /// d == mapper id to process for each element of an array
    /// 
    /// Calls d while looping thru a, array holding type b. Uses c as the iterator.
    /// </summary>
    public const string FOR_EACH = "foreach";
}

public class MappingFlow
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
    protected readonly MappingFlow RootFlow;

    public MappingFlow(List<MappingFlowCallJson> _flows, Dictionary<string, Mapper> _mappersById, MappingFlow? _rootFlow = null)
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

    public Prism? Process(Prism _input)
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
            return mapper.Execute(RootFlow, _input.Payload);
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
                    return mapper.Execute(RootFlow, _input.Payload);
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
                    return mapper.Execute(RootFlow, _input.Payload);
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
                recursivePrism = mapper.Execute(RootFlow, recursivePrism.Payload);
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
        if(_parameters.Count != 3)
        {
            throw new ArgumentException($"Cannot execute for, parameter count should have been 4 but was {_parameters.Count}.");
        }

        // get the target operator that we are supposed to iterate
        OperatorValue? intToIterate = Helper.GetTarget(
            _parameters[0],
            _input.Payload
        ) ?? throw new ArgumentException($"Could not find integer with id of '{_parameters[0]}'.");

        if(!int.TryParse(_parameters[1], out int iterationStart) || !int.TryParse(_parameters[1], out int iterationEnd))
        {
            throw new ArgumentException($"Could not parse '{_parameters[1]}' or '{_parameters[2]}' to an integer for the for loop.");
        }

        int diff = iterationEnd > iterationStart ? 1 : -1;

        if(intToIterate is IntegerOperator integerOperator)
        {
            if (!MappersById.TryGetValue(_parameters[3], out Mapper? mapper))
            {
                throw new ArgumentException($"Could not find mapper with id of '{_parameters[3]}'.");
            }

            Prism recursivePrism = _input;
            for(int delta = iterationStart; delta <= iterationEnd; delta += diff)
            {
                integerOperator.SetValue(new IntegerOperator(delta));
                recursivePrism = mapper.Execute(RootFlow, recursivePrism.Payload);
            }

            return recursivePrism;
        }
        else
        {
            throw new ArgumentException($"Operator with id of '{_parameters[0]}' was unexpectedly not an array.");
        }
    }

    protected Prism ExecuteForEachLoop(Prism _input, List<string> _parameters)
    {
        if(_parameters.Count != 4)
        {
            throw new ArgumentException($"Cannot execute foreach, parameter count should have been 4 but was {_parameters.Count}.");
        }

        // get the target operator that we are supposed to check
        OperatorValue? arrayToIterate = Helper.GetTarget(
            _parameters[0],
            _input.Payload
        ) ?? throw new ArgumentException($"Could not find array with id of '{_parameters[0]}'.");

        if(!int.TryParse(_parameters[1], out int intArrayType))
        {
            throw new ArgumentException($"Could not parse '{_parameters[1]}' to an integer for the foreach loop (integer -> array type).");
        }

        if(arrayToIterate is ArrayOperator arrayOperator)
        {
            if (!MappersById.TryGetValue(_parameters[3], out Mapper? mapper))
            {
                throw new ArgumentException($"Could not find mapper with id of '{_parameters[3]}'.");
            }

            OperatorValue iterator = OperatorValue.CreateByType((OperatorValue.OperatorValueType)intArrayType) ?? throw new ArgumentException($"Could not create operatorvalue for foreach from type '{_parameters[1]}' (it unexpectedly returned null).");


            Prism recursivePrism = _input;
            List<OperatorValue> listToIterate = arrayOperator.GetValue() ?? throw new ArgumentException($"Could not create list for foreach from the specified array (it unexpectedly returned null).");;
            foreach(OperatorValue deltaElement in listToIterate)
            {
                iterator.SetValue(deltaElement);
                recursivePrism = mapper.Execute(RootFlow, recursivePrism.Payload);
            }

            return recursivePrism;
        }
        else
        {
            throw new ArgumentException($"Operator with id of '{_parameters[0]}' was unexpectedly not an array.");
        }
    }

    protected static Func<Prism, List<string>, Prism> ProcessFlow(MappingFlow _instance, string _flowName)
    {
        return _flowName switch {
            FlowType.GOTO => _instance.ExecuteGoTo,
            FlowType.IF => _instance.ExecuteIf,
            FlowType.WHILE => _instance.ExecuteWhile,
            FlowType.FOR => _instance.ExecuteForLoop,
            FlowType.FOR_EACH => _instance.ExecuteForEachLoop,

            _ => throw new ArgumentException($"Flow type '{_flowName}' invalid.")
        };
    }
}
