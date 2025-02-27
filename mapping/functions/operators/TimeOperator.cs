
using luminary.util;


namespace luminary.mapping.functions;

public class TimeOperator : OperatorValue
{
    protected TimeOnly? NullableValue;

    public TimeOnly? GetValue() => NullableValue;

    public TimeOperator(TimeOnly? _nullableValue) : base(OperatorValueType.Time)
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
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
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

        if (_parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a timeonly.");
        }

        TimeOnly? paramValue = ((TimeOperator)_parameters[0]).GetValue();

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

        if (_parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a timeonly.");
        }

        TimeOnly check = ((TimeOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterorequal.");

        return new BooleanOperator(NullableValue >= check);
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

        if (_parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a timeonly.");
        }

        TimeOnly check = ((TimeOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterthan.");

        return new BooleanOperator(NullableValue > check);
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? _parameters)
    {
        if (NullableValue != null)
        {
            return new TimeOperator(NullableValue);
        }

        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return _parameters[0];
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

        if (_parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a timeonly.");
        }

        TimeOnly check = ((TimeOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessorequal.");

        return new BooleanOperator(NullableValue <= check);
    }

    public override OperatorValue? LessThan(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

        if (_parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a timeonly.");
        }

        TimeOnly check = ((TimeOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessthan.");

        return new BooleanOperator(NullableValue < check);
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

        if (_parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a timeonly.");
        }

        return new BooleanOperator(NullableValue.Value != ((TimeOperator)_parameters[0]).NullableValue);
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

    public override string ToJsonStringValue()
    {
        if (NullableValue == null)
        {
            return "null";
        }

        return string.Format(
            "\"{0}\"",
            NullableValue.Value.ToString()
        );
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
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a TimeOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new TimeOperator(null);
        }

        if (TimeOnly.TryParse(_input, out TimeOnly value))
        {
            return new TimeOperator(value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to TimeOnly.",
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
        if (parts.HasFlag(Duration.DurationParts.ContainsDatePortion))
        {
            // this is a timeonly, but we are adding a duration with date.
            //  because time parts differ in size that are minute or longer
            //  due to leap seconds, leap years, etc, we need a fixed date
            //  to accurately calculate the addition.

            if (_parameters.Length < 2 || _parameters[1] == null)
            {
                throw new ArgumentException("Parameter null or parameter missing. Cannot execute mathaddorsubtract.");
            }

            if (_parameters[1] is not DateTimeOffsetOperator)
            {
                throw new ArgumentException("Cannot mathaddorsubtract when the second parameter is not a datetimeoffset.");
            }

            // collect the date portion
            //  (which includes the neccessary timezone offset - any other time information is discarded)
            DateTimeOffset? param2Value = ((DateTimeOffsetOperator)_parameters[1]).GetValue();
            if (param2Value == null)
            {
                throw new ArgumentException("Cannot mathaddorsubtract when the datetimeoffset parameter is null.");
            }

            DateTimeOffset dto;
            if (_adding)
            {
                dto = paramValue.AddToDateTimeOffset(
                    new DateTimeOffset(
                        param2Value.Value.Year,
                        param2Value.Value.Month,
                        param2Value.Value.Day,
                        NullableValue.Value.Hour,
                        NullableValue.Value.Minute,
                        NullableValue.Value.Second,
                        param2Value.Value.Offset
                    )
                );
            }
            else
            {
                dto = paramValue.SubtractFromDateTimeOffset(
                    new DateTimeOffset(
                        param2Value.Value.Year,
                        param2Value.Value.Month,
                        param2Value.Value.Day,
                        NullableValue.Value.Hour,
                        NullableValue.Value.Minute,
                        NullableValue.Value.Second,
                        param2Value.Value.Offset
                    )
                );
            }

            return new DateTimeOffsetOperator(
                dto
            );
        }
        else if (parts.HasFlag(Duration.DurationParts.ContainstTimePortion))
        {
            //
            // while it is not 100% safe, our rule will be to assume normal time durations here:
            //
            //  detail: technically because of leap seconds, minutes and higher intervals could
            //  differ in size.
            //

            TimeOnly to;
            if (_adding)
            {
                to = paramValue.AddToTimeOnly(
                    NullableValue.Value
                );
            }
            else
            {
                to = paramValue.SubtractFromTimeOnly(
                    NullableValue.Value
                );
            }
            return new TimeOperator(
                to
            );
        }

        // at this point, the duration is null which shouldn't be possible
        throw new Exception("Unable to mathadd as the parts appeared to be null which logically isn't possible.");
    }
}