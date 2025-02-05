
namespace luminary.functions;

public class IntegerOperator : OperatorValue
{
    protected int? NullableValue;

    public int? GetValue() => NullableValue;
    public IntegerOperator(int? _nullableValue) : base(OperatorValueType.Integer)
    {
        NullableValue = _nullableValue;
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

    public override void SetValue(OperatorValue source)
    {
        if(source is not IntegerOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((IntegerOperator)source).NullableValue;
    }
    
    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create an IntegerOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }
    
    public static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new IntegerOperator(null);
        }

        if(int.TryParse(_input, out int _value))
        {
            return new IntegerOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to int.",
                    _input
                )
            );
        }
    }

    public void SetNativeValue(int? _input)
    {
        NullableValue =_input;
    }
}