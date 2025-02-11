
using System.Numerics;
using luminary.mapping;

namespace luminary.functions;

public class DateTimeOffsetOperator : OperatorValue
{

    protected DateTimeOffset? NullableValue;

    public DateTimeOffset? GetValue() => NullableValue;

    public DateTimeOffsetOperator(DateTimeOffset? _nullableValue, OperatorValueType _type = OperatorValueType.DateTimeOffset) : base(_type)
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
        return new StringOperator(NullableValue?.ToString(DateTimeHelper.DATE_TIME_OFFSET_TO_STRING_FORMAT));
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    protected bool InheritableValueEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if(parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? _paramValue = ((DateTimeOffsetOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is null.");
        }

        return NullableValue.Value == _paramValue.Value;
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableValueEqual(parameters));
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

        if(parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? _paramValue = ((DateTimeOffsetOperator)parameters[0]).GetValue();

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

        if(parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? _paramValue = ((DateTimeOffsetOperator)parameters[0]).GetValue();

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

            if(parameters[0] is not DateTimeOffsetOperator)
            {
                throw new ArgumentException("Cannot ifnotfilled when the parameter is not a datetimeoffset.");
            }

            return new DateTimeOffsetOperator(((DateTimeOffsetOperator)parameters[0]).GetValue());
        }

        return new DateTimeOffsetOperator(NullableValue.Value);
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

        if(parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? _paramValue = ((DateTimeOffsetOperator)parameters[0]).GetValue();

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

        if(parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? _paramValue = ((DateTimeOffsetOperator)parameters[0]).GetValue();

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

    protected bool InheritableNotEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if(parameters[0] is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a datetimeoffset.");
        }

        DateTimeOffset? _paramValue = ((DateTimeOffsetOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot notequal when the parameter is null.");
        }

        return NullableValue.Value != _paramValue.Value;
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableNotEqual(parameters));
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

        return NullableValue.Value.ToString(DateTimeHelper.DATE_TIME_OFFSET_TO_STRING_FORMAT);
    }

    public override void SetValue(OperatorValue value)
    {
        if(value is not DateTimeOffsetOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a datetimeoffset.");
        }

        NullableValue = ((DateTimeOffsetOperator)value).GetValue();
    }
    
    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DateTimeOffsetOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }
    
    public static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new DateTimeOffsetOperator(null);
        }

        if(DateTimeHelper.TrySpecialParseExactDateTimeOffset(_input,  out DateTimeOffset _value))
        {
            return new DateTimeOffsetOperator(_value);
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
    protected OperatorValue?  MathAddOrSubtract(OperatorValue[]? parameters, bool _adding)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaddorsubtract.");
        }

        if(parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathaddorsubtract when the parameter is not a duration.");
        }

        Duration? _paramValue = ((DurationOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot mathaddorsubtract when the parameter is null.");
        }


        DateTimeOffset _dto;
        if(_adding)
        {
            _dto = _paramValue.AddToDateTimeOffset(
                NullableValue.Value
            );
        }
        else
        {
            _dto = _paramValue.SubtractFromDateTimeOffset(
                NullableValue.Value
            );
        }

        return new DateTimeOffsetOperator(
            _dto
        );
    }
}