
using System.Security.Cryptography.X509Certificates;

namespace luminary.functions;

public class ArrayOperator : OperatorValue
{
    public List<OperatorValue> Value;

    public ArrayOperator(List<OperatorValue> value) : base(OperatorValueType.Array)
    {
        Value = value;
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
        if(Value == null || parameters == null)
        {
            throw new ArgumentException("Value null or parameters null. Cannot execute concatenate.");
        }

        List<OperatorValue> newArray = new List<OperatorValue>();
        foreach(OperatorValue deltaValue in parameters)
        {
            if(deltaValue is ArrayOperator)
            {
                newArray.AddRange(AppendArrayList((ArrayOperator)deltaValue));
            }
            else
            {
                newArray.Add(deltaValue);
            }
        }

        return new ArrayOperator(newArray);
    }

    protected List<OperatorValue> AppendArrayList(ArrayOperator array)
    {
        List<OperatorValue> subList = new List<OperatorValue>();
        foreach(OperatorValue deltaValue in array.Value)
        {
            if(deltaValue is ArrayOperator)
            {
                subList.AddRange(AppendArrayList((ArrayOperator)deltaValue));
            }
            else
            {
                subList.Add(deltaValue);
            }
        }

        return subList;
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
        throw new InvalidOperationException();
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(Value != null && Value.Count != 0);
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
        if(Value != null)
        {
            return new ArrayOperator(Value);
        }

        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return parameters[0];
    }

    public override OperatorValue? Includes(OperatorValue[]? parameters)
    {
        return todo();
    }

    public override OperatorValue? IndexOf(OperatorValue[]? parameters)
    {
        return todo();
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Join(OperatorValue[]? parameters)
    {
        if(Value == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute join.");
        }

        List<string> stringList = new();
        foreach(OperatorValue deltaValue in Value)
        {
            stringList.Add(deltaValue.ToStringValue());
        }

        string? separator = parameters[0].ToStringValue();

        return new StringOperator(string.Join(separator, stringList));
    }

    public override OperatorValue? Length(OperatorValue[]? parameters)
    {
        if(Value == null)
        {
            throw new ArgumentException("value null. Cannot execute length.");
        }

        return new IntegerOperator(Value.Count);
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
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
        throw new InvalidOperationException();
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Replace(OperatorValue[]? parameters)
    {
        return todo();
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
        return todo();
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
        List<string> stringList = new();
        foreach(OperatorValue deltaValue in Value)
        {
            stringList.Add(deltaValue.ToStringValue());
        }

        return string.Join(',', stringList);
    }

    public override void SetValue(OperatorValue value)
    {
        throw new NotImplementedException();
    }
}