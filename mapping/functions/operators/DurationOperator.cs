using luminary.util;

using System.Numerics;

namespace luminary.mapping.functions;

public class DurationOperator : OperatorValue
{
    protected Duration? NullableValue;

    public Duration? GetValue() => NullableValue;

    public DurationOperator(Duration? nullablevalue) : base(OperatorValueType.Duration)
    {
        NullableValue = nullablevalue;
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
        return new BigIntegerOperator(new BigInteger(CollectDoubleFromConversion(parameters)));
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters)
    {
        return new BigIntegerUnitsOperator(new BigInteger(CollectDoubleFromConversion(parameters)), ((StringOperator)parameters[0]).GetValue());
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        return new DecimalOperator((decimal)CollectDoubleFromConversion(parameters));
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        return new DecimalUnitsOperator((decimal)CollectDoubleFromConversion(parameters), ((StringOperator)parameters[0]).GetValue());
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        return new DoubleOperator(CollectDoubleFromConversion(parameters));
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters)
    {
        return new DoubleUnitsOperator(CollectDoubleFromConversion(parameters), ((StringOperator)parameters[0]).GetValue());
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        double _conversion = CollectDoubleFromConversion(parameters);

        int _output = (int)Math.Round(_conversion, 0);

        return new IntegerOperator(_output);
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        double _conversion = CollectDoubleFromConversion(parameters);

        int _output = (int)Math.Round(_conversion, 0);

        return new IntegerUnitsOperator(_output, ((StringOperator)parameters[0]).GetValue());
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
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue == ((DurationOperator)parameters[0]).NullableValue);
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(NullableValue != null);
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterorequal.");
        }

        if (parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue >= ((DurationOperator)parameters[0]).NullableValue);
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

        if (parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue > ((DurationOperator)parameters[0]).NullableValue);
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
        if (NullableValue != null)
        {
            return new DurationOperator(NullableValue);
        }

        if (parameters == null || parameters.Length == 0)
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
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessorequal.");
        }

        if (parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue <= ((DurationOperator)parameters[0]).NullableValue);
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

        if (parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue < ((DurationOperator)parameters[0]).NullableValue);
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if (parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a duration.");
        }

        Duration _param = ((DurationOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null."); ;

        return new DurationOperator(NullableValue + _param);
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaverage.");
        }

        if (parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not a duration.");
        }

        Duration _param = ((DurationOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Average value was not null but the GetValue unexpectedly returned null."); ;

        return new DurationOperator(
            new Duration(
                ((NullableValue?.Years ?? 0) + (_param?.Years ?? 0)) / 2d,
                ((NullableValue?.Months ?? 0) + (_param?.Months ?? 0)) / 2d,
                ((NullableValue?.Weeks ?? 0) + (_param?.Weeks ?? 0)) / 2d,
                ((NullableValue?.Days ?? 0) + (_param?.Days ?? 0)) / 2d,
                ((NullableValue?.Hours ?? 0) + (_param?.Hours ?? 0)) / 2d,
                ((NullableValue?.Minutes ?? 0) + (_param?.Minutes ?? 0)) / 2d,
                ((NullableValue?.Seconds ?? 0) + (_param?.Seconds ?? 0)) >> 1,
                ((NullableValue?.Nanoseconds ?? 0) + (_param?.Nanoseconds ?? 0)) >> 1
            )
        );
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
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathsubtract.");
        }

        if (parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not a duration.");
        }

        Duration _param = ((DurationOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null."); ;

        return new DurationOperator(NullableValue - _param);
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (parameters[0] is not DurationOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a duration.");
        }

        return new BooleanOperator(NullableValue != ((DurationOperator)parameters[0]).NullableValue);
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

    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if (parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DurationOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new DurationOperator(null);
        }

        if (Duration.TryParse(_input, out Duration? _value))
        {
            return new DurationOperator(_value);
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

    protected double GetDurationUnits(string? units)
    {
        if (NullableValue == null)
        {
            return 0d;
        }

        var _lowercaseUnits = units?.ToLower();

        return _lowercaseUnits switch
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
                    units
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