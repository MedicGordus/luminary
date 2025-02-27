
using System.Numerics;

namespace luminary.mapping.functions;

public class BigIntegerUnitsOperator : BigIntegerOperator
{
    protected string? NullableUnits;

    public string? GetUnits() => NullableUnits;

    public BigIntegerUnitsOperator(BigInteger? nullableValue, string? nullableUnit) : base(nullableValue, OperatorValueType.BigIntegerUnits)
    {
        NullableUnits = nullableUnit;
    }

    #region "overloaded functions with unit checks"

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBooleanAnd(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot booleanand when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBitwiseMod(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot bitwisemod when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBitwiseRightShift(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot bitwiserightshift when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBitwiseXor(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot bitwisexor when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableValueEqual(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableGreaterOrEqual(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableGreatherThan(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableLessOrEqual(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableLessThan(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathAdd(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathAverage(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathDivide(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathDivide(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathMultiply(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathPower(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathPower(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathpower when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathSubtract(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableNotEqual(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        return new BooleanOperator(_parameterValue | (_paramUnit != NullableUnits));
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBooleanOr(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot booleanor when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBooleanNot(parameters);

        if (parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot booleannot when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnits;

        if (_paramUnit != NullableUnits)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnits,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnits
        );
    }

    #endregion

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(NullableValue != null & NullableUnits != null);
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters)
    {
        return new BigIntegerUnitsOperator(NullableValue, NullableUnits);
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute the numeric conversion.");
        }

        if (decimal.TryParse(NullableValue.Value.ToString(), out var _value))
        {
            return new DecimalUnitsOperator(_value, NullableUnits);
        }

        throw new Exception("Number conversion via parse failed.");
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute the numeric conversion.");
        }

        if (double.TryParse(NullableValue.Value.ToString(), out var _value))
        {
            return new DoubleUnitsOperator(_value, NullableUnits);
        }

        throw new Exception("Number conversion via parse failed.");
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute the numeric conversion.");
        }

        if (int.TryParse(NullableValue.Value.ToString(), out var _value))
        {
            return new IntegerUnitsOperator(_value, NullableUnits);
        }

        throw new Exception("Number conversion via parse failed.");
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? parameters)
    {
        return StringOperator.BuildFromString(
            string.Format(
                "{0} {1}",
                NullableValue,
                NullableUnits
            )
        );
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
        if (NullableValue != null && NullableUnits != null)
        {
            return new BigIntegerUnitsOperator(NullableValue, NullableUnits);
        }

        if (parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return parameters[0];
    }

    public new static OperatorValue BuildFromParameters(string[] parameters)
    {
        if (parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a BigIntegerUnitsOperator, null or missing parameter.");
        }

        if (BigInteger.TryParse(parameters[0], out BigInteger _value))
        {
            return new BigIntegerUnitsOperator(_value, parameters[1]);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to BigIntegerUnitsOperator.",
                    parameters[0],
                    parameters[1]
                )
            );
        }
    }

    public new static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new BigIntegerUnitsOperator((BigInteger?)null, null);
        }

        (BigInteger _bigIntegerValue, string _bigIntegerUnits) = UnitHelper.ParseStringToBigIntegerUnits(_input);
        return new BigIntegerUnitsOperator(_bigIntegerValue, _bigIntegerUnits);
    }

    public override void SetValue(OperatorValue _source)
    {
        if (_source is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((BigIntegerUnitsOperator)_source).NullableValue;
        NullableUnits = ((BigIntegerUnitsOperator)_source).NullableUnits;
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