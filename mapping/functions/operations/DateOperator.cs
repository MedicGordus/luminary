
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
        long? _epoch = DateTimeHelper.GetValueMillisecondsSinceEpoch(NullableValue);

        if(_epoch == null)
        {
            return new BigIntegerOperator(null);
        }

        if(BigInteger.TryParse(_epoch.ToString(), out var _converted))
        {
            return new BigIntegerOperator(_converted);
        }
        else
        {
            throw new Exception("Could not parse datetime, milliseconds since epoch, to big integer.");
        }
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        long? _epoch = DateTimeHelper.GetValueMillisecondsSinceEpoch(NullableValue);

        if(_epoch == null)
        {
            return new DecimalOperator(null);
        }

        return new DecimalOperator((decimal)_epoch);
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        long? _epoch = DateTimeHelper.GetValueMillisecondsSinceEpoch(NullableValue);

        if(_epoch == null)
        {
            return new DecimalOperator(null);
        }

        return new DoubleOperator((double)_epoch);
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

        if(parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a dateonly.");
        }

        DateOnly? _paramValue = ((DateOperator)parameters[0]).GetValue();

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

        if(parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a dateonly.");
        }

        DateOnly? _paramValue = ((DateOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value >= _paramValue.Value);
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

        if(parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a dateonly.");
        }

        DateOnly? _paramValue = ((DateOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value > _paramValue.Value);
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            if(parameters == null || parameters.Length == 0 || parameters[0] == null)
            {
                throw new ArgumentException("Parameters null or parameter missing. Cannot execute ifnotfilled.");
            }

            if(parameters[0] is not DateOperator)
            {
                throw new ArgumentException("Cannot ifnotfilled when the parameter is not a dateonly.");
            }

            return new DateOperator(((DateOperator)parameters[0]).GetValue());
        }

        return new DateOperator(NullableValue.Value);
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

        if(parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a dateonly.");
        }

        DateOnly? _paramValue = ((DateOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value <= _paramValue.Value);
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

        if(parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not a dateonly.");
        }

        DateOnly? _paramValue = ((DateOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value < _paramValue.Value);
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

        if(parameters[0] is not DateOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a dateonly.");
        }

        DateOnly? _paramValue = ((DateOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot notequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value != _paramValue.Value);
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

    public override string ToJsonStringValue()
    {
        if(NullableValue == null)
        {
            return "null";
        }

        return string.Format(
            "\"{0}\"",
            ToStringValue()
        );
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
        if(value is not DateOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a dateonly.");
        }

        NullableValue = ((DateOperator)value).GetValue();
    }
    
    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DateOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }
    
    public static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new DateOperator(null);
        }
        
        if(DateOnly.TryParse(_input, out DateOnly _value))
        {
            return new DateOperator(_value);
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
        if(_parts.HasFlag(Duration.DurationParts.ContainstTimePortion))
        {
            // this is a dateonly, but we are adding a duration with time.
            //  because time parts differ in size that are minute or longer
            //  due to leap seconds, leap years, etc, we need a fixed date
            //  to accurately calculate the addition.
            
            if(parameters.Length < 2 || parameters[1] == null)
            {
                throw new ArgumentException("Parameter null or parameter missing. Cannot execute mathaddorsubtract.");
            }

            if(parameters[1] is not DurationOperator)
            {
                throw new ArgumentException("Cannot mathaddorsubtract when the second parameter is not a duration.");
            }

            // collect the timezone offset
            Duration? _param2Value = ((DurationOperator)parameters[1]).GetValue();
            if(_param2Value == null)
            {
                throw new ArgumentException("Cannot mathaddorsubtract when the duration parameter is null.");
            }

            DateTimeOffset _dto;
            if(_adding)
            {
                _dto = _paramValue.AddToDateTimeOffset(
                    new DateTimeOffset(
                        NullableValue.Value.Year,
                        NullableValue.Value.Month,
                        NullableValue.Value.Day,
                        0,
                        0,
                        0,
                        _param2Value.ConvertToTimeSpan()
                    )
                );
            }
            else
            {
                _dto = _paramValue.SubtractFromDateTimeOffset(
                    new DateTimeOffset(
                        NullableValue.Value.Year,
                        NullableValue.Value.Month,
                        NullableValue.Value.Day,
                        0,
                        0,
                        0,
                        _param2Value.ConvertToTimeSpan()
                    )
                );
            }

            return new DateTimeOffsetOperator(
                _dto
            );
        }
        else if(_parts.HasFlag(Duration.DurationParts.ContainsDatePortion))
        {   
            DateOnly _do;
            if(_adding)
            {
                _do = _paramValue.AddToDateOnly(
                    NullableValue.Value
                );
            }
            else
            {
                _do = _paramValue.SubtractFromDateOnly(
                    NullableValue.Value
                );
            }
            return new DateOperator(
                _do
            );
        }

        // at this point, the duration is null which shouldn't be possible
        throw new Exception("Unable to mathadd as the parts appeared to be null which logically isn't possible.");
    }
}