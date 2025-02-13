
using luminary.mapping;
using luminary.util;

namespace luminary.mapping.functions;

public class DateTimeOffsetWithDurationOperator : DateTimeOffsetOperator
{
    protected Duration? NullableDuration;

    public Duration? GetDuration() => NullableDuration;

    public DateTimeOffsetWithDurationOperator(DateTimeOffset? nullableDateTimeOffsetValue, Duration? nullableDuration) : base(nullableDateTimeOffsetValue, OperatorValueType.DateTimeOffsetWithDuration)
    {
        NullableDuration = nullableDuration;
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }
    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? parameters)
    {
        if(NullableValue == null || NullableDuration == null)
        {
            return new StringOperator(null);
        }

        return new StringOperator(
            string.Format(
                "{0}{1}{2}",
                NullableValue.Value.ToString(DateTimeHelper.DATE_TIME_OFFSET_TO_STRING_FORMAT),
                Helper.DATE_TIME_ZONE_DURATION_SEPARATOR,
                NullableDuration
            )
        );
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        if(InheritableValueEqual(parameters) == false)
        {
            return new BooleanOperator(false);
        }

        if(parameters[0] is not DateTimeOffsetWithDurationOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a datetimeoffsetwithduration.");
        }

        Duration? _paramValue = ((DateTimeOffsetWithDurationOperator)parameters[0]).GetDuration();

        if(NullableDuration == null || _paramValue == null)
        {
            throw new ArgumentException("Cannot valueequal when the duration or parameter is null.");
        }

        return new BooleanOperator(NullableDuration == _paramValue);
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(!(NullableValue == null || NullableDuration == null));
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
        if(NullableValue == null || NullableDuration == null)
        {
            if(parameters == null || parameters.Length == 0 || parameters[0] == null)
            {
                throw new ArgumentException("Parameters null or parameter missing. Cannot execute ifnotfilled.");
            }

            if(parameters[0] is not DateTimeOffsetWithDurationOperator)
            {
                throw new ArgumentException("Cannot ifnotfilled when the parameter is not a datetimeoffsetwithduration.");
            }

            var _dto = (DateTimeOffsetWithDurationOperator)parameters[0];
            return new DateTimeOffsetWithDurationOperator(_dto.GetValue(), _dto.GetDuration());
        }

        return new DateTimeOffsetWithDurationOperator(NullableValue.Value, NullableDuration);
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        if(InheritableNotEqual(parameters))
        {
            return new BooleanOperator(true);
        }

        if(parameters[0] is not DateTimeOffsetWithDurationOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a datetimeoffsetwithduration.");
        }

        Duration? _paramValue = ((DateTimeOffsetWithDurationOperator)parameters[0]).GetDuration();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot notequal when the parameter is null.");
        }

        return new BooleanOperator(NullableDuration != _paramValue);
    }

    public override string ToJsonStringValue()
    {
        if(NullableValue == null || NullableDuration == null)
        {
            return "null";
        }

        return string.Format(
            "\"{0}\"",
            ToStringValue()
        );
    }

    public override string ToStringValue()
    {
        return string.Format(
            "\"{0}{1}{2}\"",
            NullableValue?.ToString(DateTimeHelper.DATE_TIME_OFFSET_TO_STRING_FORMAT) ?? "null",
            Helper.DATE_TIME_ZONE_DURATION_SEPARATOR,
            NullableDuration?.ToString() ?? "null"
        );
    }

    public override void SetValue(OperatorValue value)
    {
        if(value is not DateTimeOffsetWithDurationOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a datetimeoffsetwithduration.");
        }

        var _dto = (DateTimeOffsetWithDurationOperator)value;
        NullableValue = _dto.GetValue();
        NullableDuration = _dto.GetDuration();
    }
    
    public new static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a DateTimeOffsetWithDurationOperator, null or missing parameter.");
        }
        
        if(
                DateTimeHelper.TrySpecialParseExactDateTimeOffset(parameters[0], out DateTimeOffset _value)
            &&
                Duration.TryParse(parameters[1], out Duration? _durationValue)
        )
        {
            return new DateTimeOffsetWithDurationOperator(_value, _durationValue);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to DateTimeOffsetWithDurationOperator.",
                    parameters[0],
                    parameters[1]
                )
            );
        }
    }
    
    public new static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new DateTimeOffsetWithDurationOperator(null, null);
        }

        int _dtzSeparatorIndex = _input.IndexOf(Helper.DATE_TIME_ZONE_DURATION_SEPARATOR);
        if(_dtzSeparatorIndex == -1)
        {
            throw new Exception(
                string.Format(
                    "DateTimeOffsetWithDuration unparsable, no separator '{0}' found within input, '{1}'.",
                    Helper.DATE_TIME_ZONE_DURATION_SEPARATOR,
                    _input
                )
            );
        }
        string _dtzValue = _input[.._dtzSeparatorIndex];
        string _dtzDuration = _input[(_dtzSeparatorIndex + 1)..];

        return BuildFromParameters([ _dtzValue, _dtzDuration ]);
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }
}