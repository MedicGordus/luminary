using System.Text.Json;
using luminary.mapping.functions;

namespace luminary.mapping;

/// <summary>
/// This class holds the mapping configuration for a series of steps.
/// </summary>
public class Mapper
{
    protected Dictionary<ulong, MapperStep> Steps;

    public readonly SchemaJson ExpectedInputPrismSchema;

    public Mapper(SchemaJson _expectedInputPrismSchema, Dictionary<ulong, MapperStep>? _steps)
    {
        ExpectedInputPrismSchema = _expectedInputPrismSchema;

        // make sure the steps are one thru length-1 so later during execution, the steps perform as expected
        //
        //  Note that the step 0 is considered the mapper input
        //
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

    public Prism Execute(MappingFlow _rootFlow, PrismOperator _inputPrismPayload)
    {
        // build filled prism as it is used as input for each step below
        var outputPrism = new Prism(
            _inputPrismPayload,
            ExpectedInputPrismSchema
        );

        //// progress thru mapping steps, update output each time
        //
        var context = new MapperContext(_rootFlow);
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
                deltaStep.ProcessMappingActions(context),
                deltaStep.PrismSchema
            );
            context.Add(outputPrism, stepCounter);
            stepCounter += 1;
        }
        //
        ////

        return outputPrism;
    }

    /// <summary>
    /// Obtains the last step's schema and returns it. If there are no steps, returns null.
    /// </summary>
    /// <returns>The last step's schema (or null if there are no steps).</returns>
    public SchemaJson? GetOutputSchema()
    {
        if(Steps.Count == 0)
        {
            return null;
        }

        return Steps[Steps.Keys.Max()].PrismSchema;
    }
}