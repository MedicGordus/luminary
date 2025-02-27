using System.Text.Json;
using luminary.mapping.functions;

namespace luminary.mapping;

public class MappingConfig
{
    protected Dictionary<ulong, MappingStepConfig> Steps;

    public readonly JsonDocument ExpectedInputPrismSchema;

    public MappingConfig(JsonDocument _expectedInputPrismSchema, Dictionary<ulong, MappingStepConfig>? _steps)
    {
        ExpectedInputPrismSchema = _expectedInputPrismSchema;

        // make sure the steps are zero thru length-1 so later during execution, the steps perform as expected
        if (_steps != null && _steps.Count != 0)
        {
            for (ulong delta = 1; delta < (ulong)_steps.Count; delta++)
            {
                if (!_steps.ContainsKey(delta))
                {
                    throw new ArgumentException(
                        string.Format(
                            "Invalid steps, must start at 1 and have no gaps (issue at position {0}, step count is {1}).",
                            delta,
                            _steps.Count
                        )
                    );
                }
            }
        }
        else
        {
            throw new ArgumentException("Steps cannot be empty.");
        }

        Steps = _steps;
    }

    public Prism Execute(JsonDocument _inputPayload)
    {
        //// build empty prism operator from expected input
        //
        SchemaJson schema = JsonSerializer.Deserialize<SchemaJson>(ExpectedInputPrismSchema) ?? throw new ArgumentException("Could not parse json element into json schema.");
        var inputPayloadPrismDictionary = new Dictionary<string, OperatorValue>();
        Helper.BuildPrismOperatorDictionaryFromJsonSchema(schema, inputPayloadPrismDictionary);
        //
        ////

        //// map data from the payload (this way the entire input isn't "wastefully" mapped, only what is defined in the schema)
        //
        Helper.MapDataPerSchema(inputPayloadPrismDictionary, _inputPayload.RootElement);
        //
        ////

        // at this point, an empty prism dictionary was structured, and then input data was parsed across from the payload

        return Execute(
            new PrismOperator(inputPayloadPrismDictionary),
            schema
        );
    }

    public Prism Execute(PrismOperator _inputPrismPayload, SchemaJson _inputSchema)
    {
        // build filled prism as it is used as input for each step below
        var outputPrism = new Prism(
            _inputPrismPayload,
            _inputSchema
        );

        //// progress thru mapping steps, update output each time
        //
        var context = new MappingContext();
        ulong stepCounter = 0;
        //
        // add the input into slot 0
        context.Add(outputPrism, stepCounter);
        //
        // increment to collect step 1
        stepCounter += 1;
        //
        // loop thru steps (validation in constructor)
        while (Steps.TryGetValue(stepCounter, out var deltaStep))
        {
            outputPrism = new Prism(
                deltaStep.ProcessMappingActions(context.GetAll()),
                deltaStep.PrismSchema
            );
            context.Add(outputPrism);
            stepCounter += 1;
        }
        //
        ////

        return outputPrism;
    }
}