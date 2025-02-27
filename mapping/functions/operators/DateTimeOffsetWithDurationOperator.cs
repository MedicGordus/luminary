
using luminary.mapping;
using luminary.util;

namespace luminary.mapping.functions;

public class DateTimeOffsetWithDurationOperator : DateTimeOffsetOperator
{
    protected Duration? NullableDuration;

    public Duration? GetDuration() => NullableDuration;

    public DateTimeOffsetWithDurationOperator(DateTimeOffset? _nullableDateTimeOffsetValue, Duration? _nullableDuration) : base(_nullableDateTimeOffsetValue, OperatorValueType.DateTimeOffsetWithDuration)
    {
        NullableDuration = _nullableDuration;
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }
    public override OperatorValue? ConvertToDecimal(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || NullableDuration == null)
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

    public override OperatorValue? ValueEqual(OperatorValue[]? _parameters)
    {
        if (InheritableValueEqual(_parameters) == false)
        {
            return new BooleanOperator(false);
        }

#nullable disable
        if (_parameters[0] is not DateTimeOffsetWithDurationOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a datetimeoffsetwithduration.");
        }

        Duration? paramValue = ((DateTimeOffsetWithDurationOperator)_parameters[0]).GetDuration();

        if (NullableDuration == null || paramValue == null)
        {
            throw new ArgumentException("Cannot valueequal when the duration or parameter is null.");
        }

        return new BooleanOperator(NullableDuration == paramValue);
    }

    public override OperatorValue? Filled(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(!(NullableValue == null || NullableDuration == null));
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || NullableDuration == null)
        {
            if (_parameters == null || _parameters.Length == 0 || _parameters[0] == null)
            {
                throw new ArgumentException("Parameters null or parameter missing. Cannot execute ifnotfilled.");
            }

            if (_parameters[0] is not DateTimeOffsetWithDurationOperator)
            {
                throw new ArgumentException("Cannot ifnotfilled when the parameter is not a datetimeoffsetwithduration.");
            }

            var dto = (DateTimeOffsetWithDurationOperator)_parameters[0];
            return new DateTimeOffsetWithDurationOperator(dto.GetValue(), dto.GetDuration());
        }

        return new DateTimeOffsetWithDurationOperator(NullableValue.Value, NullableDuration);
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? LessThan(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        if (InheritableNotEqual(_parameters))
        {
            return new BooleanOperator(true);
        }

#nullable disable
        if (_parameters[0] is not DateTimeOffsetWithDurationOperator)
#nullable enable
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a datetimeoffsetwithduration.");
        }

        Duration? paramValue = ((DateTimeOffsetWithDurationOperator)_parameters[0]).GetDuration();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot notequal when the parameter is null.");
        }

        return new BooleanOperator(NullableDuration != paramValue);
    }

    public override string ToJsonStringValue()
    {
        if (NullableValue == null || NullableDuration == null)
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

    public override void SetValue(OperatorValue _value)
    {
        if (_value is not DateTimeOffsetWithDurationOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a datetimeoffsetwithduration.");
        }

        var dto = (DateTimeOffsetWithDurationOperator)_value;
        NullableValue = dto.GetValue();
        NullableDuration = dto.GetDuration();
    }

    public new static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a DateTimeOffsetWithDurationOperator, null or missing parameter.");
        }

        if (
                DateTimeHelper.TrySpecialParseExactDateTimeOffset(_parameters[0], out DateTimeOffset value)
            &&
                Duration.TryParse(_parameters[1], out Duration? durationValue)
        )
        {
            return new DateTimeOffsetWithDurationOperator(value, durationValue);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to DateTimeOffsetWithDurationOperator.",
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
            return new DateTimeOffsetWithDurationOperator(null, null);
        }

        int dtzSeparatorIndex = _input.IndexOf(Helper.DATE_TIME_ZONE_DURATION_SEPARATOR);
        if (dtzSeparatorIndex == -1)
        {
            throw new Exception(
                string.Format(
                    "DateTimeOffsetWithDuration unparsable, no separator '{0}' found within input, '{1}'.",
                    Helper.DATE_TIME_ZONE_DURATION_SEPARATOR,
                    _input
                )
            );
        }
        string dtzValue = _input[..dtzSeparatorIndex];
        string dtzDuration = _input[(dtzSeparatorIndex + 1)..];

        return BuildFromParameters([dtzValue, dtzDuration]);
    }

    public override OperatorValue? MathAdd(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }
}