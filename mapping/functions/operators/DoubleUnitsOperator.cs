
using System.Numerics;

namespace luminary.mapping.functions;

public class DoubleUnitsOperator : DoubleOperator
{
    protected string? NullableUnits;

    public string? GetUnits() => NullableUnits;

    public DoubleUnitsOperator(double? _nullableValue, string? _nullableUnits) : base(_nullableValue, OperatorValueType.DoubleUnits)
    {
        NullableUnits = _nullableUnits;
    }



    #region "overloaded functions with unit checks"

    public override OperatorValue? ValueEqual(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool parameterValue = base.InheritableValueEqual(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new BooleanOperator(
            parameterValue
        );
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool parameterValue = base.InheritableGreaterOrEqual(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new BooleanOperator(
            parameterValue
        );
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool parameterValue = base.InheritableGreatherThan(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new BooleanOperator(
            parameterValue
        );
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool parameterValue = base.InheritableLessOrEqual(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new BooleanOperator(
            parameterValue
        );
    }

    public override OperatorValue? LessThan(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool parameterValue = base.InheritableLessThan(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new BooleanOperator(
            parameterValue
        );
    }

    public override OperatorValue? MathAdd(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        double parameterValue = base.InheritableMathAdd(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new DoubleUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathAverage(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        double parameterValue = base.InheritableMathAverage(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new DoubleUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathDivide(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        double parameterValue = base.InheritableMathDivide(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new DoubleUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        double parameterValue = base.InheritableMathMultiply(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new DoubleUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathPower(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        double parameterValue = base.InheritableMathPower(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathpower when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new DoubleUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        double parameterValue = base.InheritableMathSubtract(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new DoubleUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        double parameterValue = base.InheritableMathCeiling(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathceiling when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new DoubleUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathFloor(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        double parameterValue = base.InheritableMathFloor(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathfloor when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new DoubleUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathRound(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        double parameterValue = base.InheritableMathRound(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathround when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        if (paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    paramUnit
                )
            );
        }

        return new DoubleUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool parameterValue = base.InheritableNotEqual(_parameters);

#nullable disable
        if (_parameters[0] is not DoubleUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a doubleunits.");
        }

        var paramUnit = ((DoubleUnitsOperator)_parameters[0]).NullableUnits;

        return new BooleanOperator(parameterValue | (paramUnit != NullableUnits));
    }

    #endregion

    public override OperatorValue? Filled(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(NullableValue != null & NullableUnits != null);
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute the numeric conversion.");
        }

        if (BigInteger.TryParse(NullableValue.Value.ToString(), out var value))
        {
            return new BigIntegerUnitsOperator(value, NullableUnits);
        }

        throw new Exception("Number conversion via parse failed.");
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute the numeric conversion.");
        }

        if (decimal.TryParse(NullableValue.Value.ToString(), out var value))
        {
            return new DecimalUnitsOperator(value, NullableUnits);
        }

        throw new Exception("Number conversion via parse failed.");
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? _parameters)
    {
        return new DoubleUnitsOperator(NullableValue, NullableUnits);
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute the numeric conversion.");
        }

        if (int.TryParse(NullableValue.Value.ToString(), out var value))
        {
            return new IntegerUnitsOperator(value, NullableUnits);
        }

        throw new Exception("Number conversion via parse failed.");
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? _parameters)
    {
        return StringOperator.BuildFromString(
            string.Format(
                "{0} {1}",
                NullableValue,
                NullableUnits
            )
        );
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? _parameters)
    {
        if (NullableValue != null && NullableUnits != null)
        {
            return new DoubleUnitsOperator(NullableValue, NullableUnits);
        }

        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return _parameters[0];
    }

    public new static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DoubleUnitsOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public new static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new DoubleUnitsOperator(null, null);
        }

        (double doubleValue, string doubleUnits) = UnitHelper.ParseStringToDoubleUnits(_input);
        return new DoubleUnitsOperator(doubleValue, doubleUnits);
    }

    public override void SetValue(OperatorValue _source)
    {
        if (_source is not DoubleUnitsOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((DoubleUnitsOperator)_source).NullableValue;
        NullableUnits = ((DoubleUnitsOperator)_source).NullableUnits;
    }

    public override string ToJsonStringValue()
    {
        if (NullableValue == null || NullableUnits == null)
        {
            return "null";
        }

        return string.Format(
            "\"{0} {1}\"",
            NullableValue.Value.ToString(),
            NullableUnits.ToString()
        );
    }

    public override string ToStringValue()
    {
        if (NullableValue == null || NullableUnits == null)
        {
            throw new ArgumentException("Value null and/or units null. Cannot execute tostringvalue.");
        }

        return string.Format(
            "{0} {1}",
            NullableValue.Value.ToString(),
            NullableUnits.ToString()
        );
    }
}