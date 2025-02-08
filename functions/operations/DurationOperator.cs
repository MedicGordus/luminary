
using System.Numerics;

namespace luminary.functions;

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
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
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
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
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
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
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
    }

    public override string ToStringValue()
    {
    }

    public override void SetValue(OperatorValue value)
    {
    }
    
    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DurationOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }
    
    public static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new DurationOperator(null);
        }
        
        if(Duration.TryParse(_input, out Duration? _value))
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
        if(NullableValue == null)
        {
            throw new Exception("Cannot getdurationunits when the Value is null.");
        }

        var _lowercaseUnits = units?.ToLower();

        return _lowercaseUnits switch {
            "ns" or "nanosecond" or "nanoseconds"
                => NullableValue.Value.TotalNanoseconds,

            "ms" or "millisecond" or "milliseconds"
                => NullableValue.Value.TotalMilliseconds,

            "s" or "second" or "seconds"
                => NullableValue.Value.TotalSeconds,

            "min" or "minute" or "minutes"
                => NullableValue.Value.TotalMinutes,

            "h" or "hr" or "hour" or "hours"
                => NullableValue.Value.TotalHours,

            "d" or "day" or "days"
                => NullableValue.Value.TotalDays,

            "wk" or "week" or "weeks"
                => NullableValue.Value.TotalDays / 7,

            _ => throw new ArgumentException(
                string.Format(
                    "Unknown duration unit type '{0}', accepted unit types: ns, ms, s, min, h, d, wk (or the full word, plural or singular for each).",
                    units
                )
            )
        };
    }

    protected double CollectDoubleFromConversion(OperatorValue[]? _parameters)
    {
        if(NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute conversion.");
        }

        if(_parameters[0] is not StringOperator)
        {
            throw new ArgumentException("Cannot execute conversion when the unit parameter is not a string.");
        }

        return GetDurationUnits(((StringOperator)_parameters[0]).GetValue());
    }

}