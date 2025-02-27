using luminary.util;

using System.Numerics;

namespace luminary.mapping.functions;

public class DateOperator : OperatorValue
{
    protected DateOnly? NullableValue;

    public DateOnly? GetValue() => NullableValue;


    public DateOperator(DateOnly? _nullableValue) : base(OperatorValueType.Date)
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
        return new StringOperator(NullableValue?.ToString());
    }

    public override OperatorValue? EndsWith(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (_parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a dateonly.");
        }

        DateOnly? paramValue = ((DateOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value == paramValue.Value);
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

        if (_parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a dateonly.");
        }

        DateOnly? paramValue = ((DateOperator)_parameters[0]).GetValue();

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

        if (_parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a dateonly.");
        }

        DateOnly? paramValue = ((DateOperator)_parameters[0]).GetValue();

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

            if (_parameters[0] is not DateOperator)
            {
                throw new ArgumentException("Cannot ifnotfilled when the parameter is not a dateonly.");
            }

            return new DateOperator(((DateOperator)_parameters[0]).GetValue());
        }

        return new DateOperator(NullableValue.Value);
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

        if (_parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a dateonly.");
        }

        DateOnly? paramValue = ((DateOperator)_parameters[0]).GetValue();

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

        if (_parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not a dateonly.");
        }

        DateOnly? paramValue = ((DateOperator)_parameters[0]).GetValue();

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

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (_parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a dateonly.");
        }

        DateOnly? paramValue = ((DateOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot notequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value != paramValue.Value);
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

        return NullableValue.Value.ToString();
    }

    public override void SetValue(OperatorValue _value)
    {
        if (_value is not DateOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a dateonly.");
        }

        NullableValue = ((DateOperator)_value).GetValue();
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DateOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new DateOperator(null);
        }

        if (DateOnly.TryParse(_input, out DateOnly value))
        {
            return new DateOperator(value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to DateOnly.",
                    _input
                )
            );
        }
    }

    protected OperatorValue? MathAddOrSubtract(OperatorValue[]? _parameters, bool _adding)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a duration.");
        }

        Duration? paramValue = ((DurationOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is null.");
        }

        Duration.DurationParts parts = paramValue.GetParts();
        if (parts.HasFlag(Duration.DurationParts.ContainstTimePortion))
        {
            // this is a dateonly, but we are adding a duration with time.
            //  because time parts differ in size that are minute or longer
            //  due to leap seconds, leap years, etc, we need a fixed date
            //  to accurately calculate the addition.

            if (_parameters.Length < 2 || _parameters[1] == null)
            {
                throw new ArgumentException("Parameter null or parameter missing. Cannot execute mathaddorsubtract.");
            }

            if (_parameters[1] is not DurationOperator)
            {
                throw new ArgumentException("Cannot mathaddorsubtract when the second parameter is not a duration.");
            }

            // collect the timezone offset
            Duration? param2Value = ((DurationOperator)_parameters[1]).GetValue();
            if (param2Value == null)
            {
                throw new ArgumentException("Cannot mathaddorsubtract when the duration parameter is null.");
            }

            DateTimeOffset dto;
            if (_adding)
            {
                dto = paramValue.AddToDateTimeOffset(
                    new DateTimeOffset(
                        NullableValue.Value.Year,
                        NullableValue.Value.Month,
                        NullableValue.Value.Day,
                        0,
                        0,
                        0,
                        param2Value.ConvertToTimeSpan()
                    )
                );
            }
            else
            {
                dto = paramValue.SubtractFromDateTimeOffset(
                    new DateTimeOffset(
                        NullableValue.Value.Year,
                        NullableValue.Value.Month,
                        NullableValue.Value.Day,
                        0,
                        0,
                        0,
                        param2Value.ConvertToTimeSpan()
                    )
                );
            }

            return new DateTimeOffsetOperator(
                dto
            );
        }
        else if (parts.HasFlag(Duration.DurationParts.ContainsDatePortion))
        {
            DateOnly dateOnly;
            if (_adding)
            {
                dateOnly = paramValue.AddToDateOnly(
                    NullableValue.Value
                );
            }
            else
            {
                dateOnly = paramValue.SubtractFromDateOnly(
                    NullableValue.Value
                );
            }
            return new DateOperator(
                dateOnly
            );
        }

        // at this point, the duration is null which shouldn't be possible
        throw new Exception("Unable to mathadd as the parts appeared to be null which logically isn't possible.");
    }
}