
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Markup;

namespace luminary.functions;

public class ArrayOperator : OperatorValue
{
    public List<OperatorValue> Value;

    public OperatorValueType ArrayType;

    public ArrayOperator(OperatorValueType arrayType, List<OperatorValue> value) : base(OperatorValueType.Array)
    {
        Value = value;
        ArrayType = arrayType;
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

        return new ArrayOperator(ArrayType, newArray);
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
            return new ArrayOperator(ArrayType, Value);
        }

        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return parameters[0];
    }

    public override OperatorValue? Includes(OperatorValue[]? parameters)
    {
        if(Value == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute includes.");
        }

        // pass thru of parameters to ValueEqual to all elements in array and returns
        return new BooleanOperator(Value.Any(_item => ((BooleanOperator?)_item.ValueEqual(parameters))?.GetValue() ?? false));
    }

    public override OperatorValue? IndexOf(OperatorValue[]? parameters)
    {
        // pass thru of parameters to ValueEqual and returns the first match, or if none, null
        return Value.FirstOrDefault(_item => ((BooleanOperator?)_item.ValueEqual(parameters))?.GetValue() ?? false);
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
        if(Value == null || parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute replace.");
        }

        List<OperatorValue> _outputContents = [];
        OperatorValue _toRemove = parameters[0];
        OperatorValue _toAdd = parameters[1];

        // check which match and replace those that do (be aware: THESE ARE BYREF, NOT COPIES)
        foreach(OperatorValue _deltaValue in Value)
        {
            if(((BooleanOperator?)_deltaValue.ValueEqual([_toRemove]))?.GetValue() ?? false)
            {
                _outputContents.Add(_toAdd);
            }
            else
            {
                _outputContents.Add(_deltaValue);
            }
        }

        return new ArrayOperator(ArrayType, _outputContents);
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
        StringBuilder _output = new StringBuilder();
        _output.Append('[');

        if(Value.Count != 0)
        {
            foreach(var _deltaValue in Value)
            {
                _output.Append(_deltaValue.ToJsonStringValue());
                _output.Append(',');
            }
            _output.Length -=1;
        }

        _output.Append(']');
        return _output.ToString();
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
        ArrayType = ((ArrayOperator)value).ArrayType;
        Value = ((ArrayOperator)value).Value;
    }
}