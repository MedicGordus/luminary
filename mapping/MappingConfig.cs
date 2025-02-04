using System.Text.Json;
using luminary.functions;

namespace luminary.mapping;

public class MappingConfig
{
    protected Dictionary<ulong, MappingStepConfig> Steps;

    protected JsonDocument ExpectedInputPrismSchema;

    public MappingConfig(JsonDocument expectedInputPrismSchema, Dictionary<ulong, MappingStepConfig>? steps)
    {
        ExpectedInputPrismSchema = expectedInputPrismSchema;

        // make sure the steps are zero thru length-1 so later during execution, the steps perform as expected
        if(steps != null && steps.Count != 0)
        {
            for(ulong _delta = 0; _delta < (ulong)steps.Count; _delta++)
            {
                if(!steps.ContainsKey(_delta))
                {
                    throw new ArgumentException(
                        string.Format(
                            "Invalid steps, must start at 0 and have no gaps (issue at position {0}, step count is {1}).",
                            _delta,
                            steps.Count
                        )
                    );
                }
            }
        }
        else
        {
            throw new ArgumentException("Steps cannot be empty.");
        }

        Steps = steps;
    }
    
    public Prism Execute(JsonDocument inputPayload)
    {
        //// build empty prism operator from expected input
        //
        SchemaJson _schema = JsonSerializer.Deserialize<SchemaJson>(ExpectedInputPrismSchema) ?? throw new ArgumentException("Could not parse json element into json schema.");
        var _inputPayloadPrismDictionary = new Dictionary<string, OperatorValue>();
        Helper.BuildPrismOperatorDictionaryFromJsonSchema(_schema, _inputPayloadPrismDictionary);
        //
        ////
        
        //// map data from the payload (this way the entire input isn't "wastefully" mapped, only what is defined in the schema)
        //
        Helper.MapDataPerSchema(_inputPayloadPrismDictionary, inputPayload.RootElement);
        //
        ////
        
        // at this point, an empty prism dictionary was structured, and then input data was parsed across from the payload

        return Execute(new PrismOperator(_inputPayloadPrismDictionary));
    }
    
    public Prism Execute(PrismOperator inputPrismPayload)
    {
        // build filled prism as it is used as input for each step below
        var _outputPrism = new Prism(inputPrismPayload);

        //// progress thru mapping steps, update output each time
        //
        var _context = new MappingContext();
        ulong _stepCounter = 0;
        //
        // add the input into slot 0
        _context.Add(_outputPrism, _stepCounter);
        //
        // loop thru steps (validation in constructor)
        while(Steps.TryGetValue(_stepCounter, out var _deltaStep))
        {
            _outputPrism = _deltaStep.ProcessMappingActions(_context.GetAll());
            _context.Add(_outputPrism);
            _stepCounter+=1;
        }
        //
        ////

        return _outputPrism;
    }
}