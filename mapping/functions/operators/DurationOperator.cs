using luminary.util;

using System.Numerics;

namespace luminary.mapping.functions;

public class DurationOperator : OperatorValue
{
    protected Duration? NullableValue;

    public Duration? GetValue() => NullableValue;

    public DurationOperator(Duration? _nullablevalue) : base(OperatorValueType.Duration)
    {
        NullableValue = _nullablevalue;
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
        return new BigIntegerOperator(new BigInteger(CollectDoubleFromConversion(_parameters)));
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters)
    {
#nullable disable
        return new BigIntegerUnitsOperator(new BigInteger(CollectDoubleFromConversion(_parameters)), ((StringOperator)_parameters[0]).GetValue());
#nullable enable
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? _parameters)
    {
        return new DecimalOperator((decimal)CollectDoubleFromConversion(_parameters));
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? _parameters)
    {
#nullable disable
        return new DecimalUnitsOperator((decimal)CollectDoubleFromConversion(_parameters), ((StringOperator)_parameters[0]).GetValue());
#nullable enable
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(CollectDoubleFromConversion(_parameters));
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? _parameters)
    {
#nullable disable
        return new DoubleUnitsOperator(CollectDoubleFromConversion(_parameters), ((StringOperator)_parameters[0]).GetValue());
#nullable enable
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? _parameters)
    {
        double conversion = CollectDoubleFromConversion(_parameters);

        int output = (int)Math.Round(conversion, 0);

        return new IntegerOperator(output);
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters)
    {
        double conversion = CollectDoubleFromConversion(_parameters);

        int output = (int)Math.Round(conversion, 0);

#nullable disable
        return new IntegerUnitsOperator(output, ((StringOperator)_parameters[0]).GetValue());
#nullable enable
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

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue == ((DurationOperator)_parameters[0]).NullableValue);
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

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue >= ((DurationOperator)_parameters[0]).NullableValue);
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue > ((DurationOperator)_parameters[0]).NullableValue);
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? _parameters)
    {
        if (NullableValue != null)
        {
            return new DurationOperator(NullableValue);
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

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue <= ((DurationOperator)_parameters[0]).NullableValue);
    }

    public override OperatorValue? LessThan(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue < ((DurationOperator)_parameters[0]).NullableValue);
    }

    public override OperatorValue? MathAdd(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a duration.");
        }

        Duration param = ((DurationOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null."); ;

        return new DurationOperator(NullableValue + param);
    }

    public override OperatorValue? MathAverage(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaverage.");
        }

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not a duration.");
        }

        Duration param = ((DurationOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Average value was not null but the GetValue unexpectedly returned null."); ;

        return new DurationOperator(
            new Duration(
                ((NullableValue?.Years ?? 0) + (param?.Years ?? 0)) / 2d,
                ((NullableValue?.Months ?? 0) + (param?.Months ?? 0)) / 2d,
                ((NullableValue?.Weeks ?? 0) + (param?.Weeks ?? 0)) / 2d,
                ((NullableValue?.Days ?? 0) + (param?.Days ?? 0)) / 2d,
                ((NullableValue?.Hours ?? 0) + (param?.Hours ?? 0)) / 2d,
                ((NullableValue?.Minutes ?? 0) + (param?.Minutes ?? 0)) / 2d,
                ((NullableValue?.Seconds ?? 0) + (param?.Seconds ?? 0)) >> 1,
                ((NullableValue?.Nanoseconds ?? 0) + (param?.Nanoseconds ?? 0)) >> 1
            )
        );
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
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathsubtract.");
        }

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not a duration.");
        }

        Duration param = ((DurationOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null."); ;

        return new DurationOperator(NullableValue - param);
    }

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (_parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue != ((DurationOperator)_parameters[0]).NullableValue);
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
            NullableValue.ToString()
        );
    }

    public override string ToStringValue()
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        return NullableValue.ToString();
    }

    public override void SetValue(OperatorValue _source)
    {
        if (_source is not DurationOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((DurationOperator)_source).NullableValue;
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DurationOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new DurationOperator(null);
        }

        if (Duration.TryParse(_input, out Duration? value))
        {
            return new DurationOperator(value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to DurationOperator.",
                    _input
                )
            );
        }
    }

    protected double GetDurationUnits(string? _units)
    {
        if (NullableValue == null)
        {
            return 0d;
        }

        var lowercaseUnits = _units?.ToLower();

        return lowercaseUnits switch
        {
            "ns" or "nanosecond" or "nanoseconds"
                => NullableValue.Nanoseconds,

            "s" or "second" or "seconds"
                => NullableValue.Seconds,

            "min" or "minute" or "minutes"
                => NullableValue.Minutes,

            "h" or "hr" or "hour" or "hours"
                => NullableValue.Hours,

            "d" or "day" or "days"
                => NullableValue.Days,

            "wk" or "week" or "weeks"
                => NullableValue.Weeks,

            "mon" or "month" or "months"
                => NullableValue.Months,

            "yr" or "year" or "years"
                => NullableValue.Years,

            _ => throw new ArgumentException(
                string.Format(
                    "Unknown duration unit type '{0}', accepted unit types: ns, s, min, h, d, wk, mon, yr (or the full word, plural or singular for each).",
                    _units
                )
            )
        } ?? 0d;
    }

    protected double CollectDoubleFromConversion(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute conversion.");
        }

        if (_parameters[0] is not StringOperator)
        {
            throw new ArgumentException("Cannot execute conversion when the unit parameter is not a string.");
        }

        return GetDurationUnits(((StringOperator)_parameters[0]).GetValue());
    }

}