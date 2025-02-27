
using System.Numerics;

namespace luminary.mapping.functions;

public class IntegerUnitsOperator : IntegerOperator
{
    protected string? NullableUnits;

    public string? GetUnits() => NullableUnits;

    public IntegerUnitsOperator(int? _nullableValue, string? _nullableUnits) : base(_nullableValue, OperatorValueType.IntegerUnits)
    {
        NullableUnits = _nullableUnits;
    }

    #region "overloaded functions with unit checks"

    public override OperatorValue? BooleanAnd(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableBooleanAnd(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot booleanand when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableBitwiseMod(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot bitwisemod when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new BigIntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableBitwiseRightShift(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot bitwiserightshift when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableBitwiseXor(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot bitwisexor when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool parameterValue = base.InheritableValueEqual(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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
        int parameterValue = base.InheritableMathAdd(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathAverage(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableMathAverage(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathDivide(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableMathDivide(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableMathMultiply(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathPower(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableMathPower(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathpower when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableMathSubtract(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool parameterValue = base.InheritableNotEqual(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot notequal when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

        return new BooleanOperator(parameterValue | (paramUnit != NullableUnits));
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableBooleanOr(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot booleanor when the parameter is not a integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? _parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        int parameterValue = base.InheritableBooleanNot(_parameters);

#nullable disable
        if (_parameters[0] is not IntegerUnitsOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot booleannot when the parameter is not an integerunits.");
        }

        var paramUnit = ((IntegerUnitsOperator)_parameters[0]).NullableUnits;

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

        return new IntegerUnitsOperator(
            parameterValue,
            NullableUnits
        );
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
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute the numeric conversion.");
        }

        if (double.TryParse(NullableValue.Value.ToString(), out var value))
        {
            return new DoubleUnitsOperator(value, NullableUnits);
        }

        throw new Exception("Number conversion via parse failed.");
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters)
    {
        return new IntegerUnitsOperator(NullableValue, NullableUnits);
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
            return new IntegerUnitsOperator(NullableValue, NullableUnits);
        }

        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return _parameters[0];
    }

    public new static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a IntegerUnitsOperator, null or missing parameter.");
        }

        if (int.TryParse(_parameters[0], out int value))
        {
            return new IntegerUnitsOperator(value, _parameters[1]);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to IntegerUnitsOperator.",
                    _parameters[0],
                    _parameters[1]
                )
            );
        }
    }

    public new static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new IntegerUnitsOperator(null, null);
        }

        (int intValue, string intUnits) = UnitHelper.ParseStringToIntegerUnits(_input);
        return new IntegerUnitsOperator(intValue, intUnits);
    }

    public override void SetValue(OperatorValue _source)
    {
        if (_source is not IntegerUnitsOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((IntegerUnitsOperator)_source).NullableValue;
        NullableUnits = ((IntegerUnitsOperator)_source).NullableUnits;
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