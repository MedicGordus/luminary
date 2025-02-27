using luminary.mapping;
using luminary.util;


using System.Numerics;

namespace luminary.mapping.functions;

public class DateTimeOffsetOperator : OperatorValue
{

    protected DateTimeOffset? NullableValue;

    public DateTimeOffset? GetValue() => NullableValue;

    public DateTimeOffsetOperator(DateTimeOffset? _nullableValue, OperatorValueType _type = OperatorValueType.DateTimeOffset) : base(_type)
    {
        NullableValue = _nullableValue;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Concatenate(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? _parameters)
    {
        long? epoch = DateTimeHelper.GetValueMillisecondsSinceEpoch(NullableValue);

        if (epoch == null)
        {
            return new BigIntegerOperator(null);
        }

        if (BigInteger.TryParse(epoch.ToString(), out var converted))
        {
            return new BigIntegerOperator(converted);
        }
        else
        {
            throw new Exception("Could not parse datetime, milliseconds since epoch, to big integer.");
        }
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? _parameters)
    {
        long? epoch = DateTimeHelper.GetValueMillisecondsSinceEpoch(NullableValue);

        if (epoch == null)
        {
            return new DecimalOperator(null);
        }

        return new DecimalOperator((decimal)epoch);
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? _parameters)
    {
        long? epoch = DateTimeHelper.GetValueMillisecondsSinceEpoch(NullableValue);

        if (epoch == null)
        {
            return new DecimalOperator(null);
        }

        return new DoubleOperator((double)epoch);
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? _parameters)
    {
        return new StringOperator(NullableValue?.ToString(DateTimeHelper.DATE_TIME_OFFSET_TO_STRING_FORMAT));
    }

    public override OperatorValue? EndsWith(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    protected bool InheritableValueEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (_parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? paramValue = ((DateTimeOffsetOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is null.");
        }

        return NullableValue.Value == paramValue.Value;
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableValueEqual(_parameters));
    }

    public override OperatorValue? Filled(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(NullableValue != null);
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterorequal.");
        }

        if (_parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? paramValue = ((DateTimeOffsetOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value >= paramValue.Value);
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

        if (_parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? paramValue = ((DateTimeOffsetOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value > paramValue.Value);
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            if (_parameters == null || _parameters.Length == 0 || _parameters[0] == null)
            {
                throw new ArgumentException("Parameters null or parameter missing. Cannot execute ifnotfilled.");
            }

            if (_parameters[0] is not DateTimeOffsetOperator)
            {
                throw new ArgumentException("Cannot ifnotfilled when the parameter is not a datetimeoffset.");
            }

            return new DateTimeOffsetOperator(((DateTimeOffsetOperator)_parameters[0]).GetValue());
        }

        return new DateTimeOffsetOperator(NullableValue.Value);
    }

    public override OperatorValue? Includes(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? IndexOf(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Join(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Length(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessorequal.");
        }

        if (_parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? paramValue = ((DateTimeOffsetOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value <= paramValue.Value);
    }

    public override OperatorValue? LessThan(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

        if (_parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? paramValue = ((DateTimeOffsetOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value < paramValue.Value);
    }

    public override OperatorValue? MathAdd(OperatorValue[]? _parameters)
    {
        return MathAddOrSubtract(_parameters, true);
    }

    public override OperatorValue? MathAverage(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathDivide(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathFloor(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathPower(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathRound(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? _parameters)
    {
        return MathAddOrSubtract(_parameters, false);
    }

    protected bool InheritableNotEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (_parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? paramValue = ((DateTimeOffsetOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot notequal when the parameter is null.");
        }

        return NullableValue.Value != paramValue.Value;
    }

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableNotEqual(_parameters));
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Replace(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Split(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? StartsWith(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Substring(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override string ToJsonStringValue()
    {
        if (NullableValue == null)
        {
            return "null";
        }

        return string.Format(
            "\"{0}\"",
            ToStringValue()
        );
    }

    public override OperatorValue? ToLower(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ToUpper(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Trim(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override string ToStringValue()
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        return NullableValue.Value.ToString(DateTimeHelper.DATE_TIME_OFFSET_TO_STRING_FORMAT);
    }

    public override void SetValue(OperatorValue _value)
    {
        if (_value is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a datetimeoffset.");
        }

        NullableValue = ((DateTimeOffsetOperator)_value).GetValue();
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DateTimeOffsetOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new DateTimeOffsetOperator(null);
        }

        if (DateTimeHelper.TrySpecialParseExactDateTimeOffset(_input, out DateTimeOffset value))
        {
            return new DateTimeOffsetOperator(value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to DateTimeOffsetOperator.",
                    _input
                )
            );
        }
    }
    protected OperatorValue? MathAddOrSubtract(OperatorValue[]? _parameters, bool _adding)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaddorsubtract.");
        }

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathaddorsubtract when the parameter is not a duration.");
        }

        Duration? paramValue = ((DurationOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot mathaddorsubtract when the parameter is null.");
        }


        DateTimeOffset dto;
        if (_adding)
        {
            dto = paramValue.AddToDateTimeOffset(
                NullableValue.Value
            );
        }
        else
        {
            dto = paramValue.SubtractFromDateTimeOffset(
                NullableValue.Value
            );
        }

        return new DateTimeOffsetOperator(
            dto
        );
    }
}