
namespace luminary.functions;

public class DoubleUnitsOperator : OperatorValue
{
    protected double? NullableValue;
    protected string? NullableUnits;

    public DoubleUnitsOperator(double? nullableValue, string? nullableUnits) : base(OperatorValueType.DoubleUnits)
    {
        NullableValue = nullableValue;
        NullableUnits = nullableUnits;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? Concatenate(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? Includes(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? IndexOf(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? Join(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? Length(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? MathDivide(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? MathFloor(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? MathPower(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? MathRound(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? Replace(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? Split(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? StartsWith(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? Substring(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override string ToJsonStringValue()
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ToLower(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? ToUpper(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? Trim(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        throw new NotImplementedException();
    }

    public override string ToStringValue()
    {
        throw new NotImplementedException();
    }

    public override void SetValue(OperatorValue value)
    {
        throw new NotImplementedException();
    }
    
    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a DoubleUnitsOperator, null or missing parameter.");
        }

        if(double.TryParse(parameters[0], out double _value))
        {
            return new DoubleUnitsOperator(_value, parameters[1]);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to DoubleUnitsOperator.",
                    parameters[0],
                    parameters[1]
                )
            );
        }
    }
    
    public static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new DoubleUnitsOperator(null, null);
        }

        (double _doubleValue, string _doubleUnits) = UnitHelper.ParseStringToDoubleUnits(_input);
        return new DoubleUnitsOperator(_doubleValue, _doubleUnits);
    }
}