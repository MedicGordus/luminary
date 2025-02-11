
namespace luminary.functions;

public class TimeOperator : OperatorValue
{
    protected TimeOnly? NullableValue;

    public TimeOnly? GetValue() => NullableValue;

    public TimeOperator(TimeOnly? nullableValue) : base(OperatorValueType.Time)
    {
        NullableValue = nullableValue;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Concatenate(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? parameters)
    {
        return new StringOperator(NullableValue?.ToString());
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if(parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a timeonly.");
        }

        TimeOnly? _paramValue = ((TimeOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value == _paramValue.Value);
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(NullableValue != null);
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterorequal.");
        }

        if(parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a timeonly.");
        }

        TimeOnly _check = ((TimeOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterorequal.");

        return new BooleanOperator(NullableValue >= _check);
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

        if(parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a timeonly.");
        }

        TimeOnly _check = ((TimeOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterthan.");

        return new BooleanOperator(NullableValue > _check);
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
        if(NullableValue != null)
        {
            return new TimeOperator(NullableValue);
        }

        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return parameters[0];
    }

    public override OperatorValue? Includes(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? IndexOf(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Join(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Length(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessorequal.");
        }

        if(parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a timeonly.");
        }

        TimeOnly _check = ((TimeOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessorequal.");

        return new BooleanOperator(NullableValue <= _check);
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

        if(parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a timeonly.");
        }

        TimeOnly _check = ((TimeOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessthan.");

        return new BooleanOperator(NullableValue < _check);
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        return MathAddOrSubtract(parameters, true);
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathDivide(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathFloor(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathPower(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathRound(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? parameters)
    {
        return MathAddOrSubtract(parameters, false);
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if(parameters[0] is not TimeOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a timeonly.");
        }

        return new BooleanOperator(NullableValue.Value != ((TimeOperator)parameters[0]).NullableValue);
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Replace(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Split(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? StartsWith(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Substring(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ToLower(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ToUpper(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Trim(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override string ToJsonStringValue()
    {
        if(NullableValue == null)
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
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        return NullableValue.Value.ToString();
    }

    public override void SetValue(OperatorValue value)
    {
    }
    
    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a TimeOperator, null or missing parameter.");
        }
        
        return BuildFromString(parameters[0]);
    }
    
    public static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new TimeOperator(null);
        }

        if(TimeOnly.TryParse(_input, out TimeOnly _value))
        {
            return new TimeOperator(_value);
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
    
    protected OperatorValue?  MathAddOrSubtract(OperatorValue[]? parameters, bool _adding)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if(parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a duration.");
        }

        Duration? _paramValue = ((DurationOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is null.");
        }

        Duration.DurationParts _parts = _paramValue.GetParts();
        if(_parts.HasFlag(Duration.DurationParts.ContainsDatePortion))
        {
            // this is a timeonly, but we are adding a duration with date.
            //  because time parts differ in size that are minute or longer
            //  due to leap seconds, leap years, etc, we need a fixed date
            //  to accurately calculate the addition.
            
            if(parameters.Length < 2 || parameters[1] == null)
            {
                throw new ArgumentException("Parameter null or parameter missing. Cannot execute mathaddorsubtract.");
            }

            if(parameters[1] is not DateTimeOffsetOperator)
            {
                throw new ArgumentException("Cannot mathaddorsubtract when the second parameter is not a datetimeoffset.");
            }

            // collect the date portion
            //  (which includes the neccessary timezone offset - any other time information is discarded)
            DateTimeOffset? _param2Value = ((DateTimeOffsetOperator)parameters[1]).GetValue();
            if(_param2Value == null)
            {
                throw new ArgumentException("Cannot mathaddorsubtract when the datetimeoffset parameter is null.");
            }

            DateTimeOffset _dto;
            if(_adding)
            {
                _dto = _paramValue.AddToDateTimeOffset(
                    new DateTimeOffset(
                        _param2Value.Value.Year,
                        _param2Value.Value.Month,
                        _param2Value.Value.Day,
                        NullableValue.Value.Hour,
                        NullableValue.Value.Minute,
                        NullableValue.Value.Second,
                        _param2Value.Value.Offset
                    )
                );
            }
            else
            {
                _dto = _paramValue.SubtractFromDateTimeOffset(
                    new DateTimeOffset(
                        _param2Value.Value.Year,
                        _param2Value.Value.Month,
                        _param2Value.Value.Day,
                        NullableValue.Value.Hour,
                        NullableValue.Value.Minute,
                        NullableValue.Value.Second,
                        _param2Value.Value.Offset
                    )
                );
            }

            return new DateTimeOffsetOperator(
                _dto
            );
        }
        else if(_parts.HasFlag(Duration.DurationParts.ContainstTimePortion))
        {
            //
            // while it is not 100% safe, our rule will be to assume normal time durations here:
            //
            //  detail: technically because of leap seconds, minutes and higher intervals could
            //  differ in size.
            //
            
            TimeOnly _to;
            if(_adding)
            {
                _to = _paramValue.AddToTimeOnly(
                    NullableValue.Value
                );
            }
            else
            {
                _to = _paramValue.SubtractFromTimeOnly(
                    NullableValue.Value
                );
            }
            return new TimeOperator(
                _to
            );
        }

        // at this point, the duration is null which shouldn't be possible
        throw new Exception("Unable to mathadd as the parts appeared to be null which logically isn't possible.");
    }
}