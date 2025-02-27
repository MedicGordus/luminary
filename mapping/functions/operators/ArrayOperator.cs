
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Markup;

namespace luminary.mapping.functions;

public class ArrayOperator : OperatorValue
{
    public List<OperatorValue>? NullableValue;

    public List<OperatorValue>? GetValue() => NullableValue;

    public OperatorValueType ArrayType;

    public ArrayOperator(OperatorValueType _arrayType, List<OperatorValue> _nullableValue) : base(OperatorValueType.Array)
    {
        NullableValue = _nullableValue;
        ArrayType = _arrayType;
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
        if (NullableValue == null || parameters == null)
        {
            throw new ArgumentException("Value null or parameters null. Cannot execute concatenate.");
        }

        List<OperatorValue> newArray = new List<OperatorValue>();
        foreach (OperatorValue deltaValue in parameters)
        {
            if (deltaValue is ArrayOperator)
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

        if (array != null && array.NullableValue != null)
        {
            foreach (OperatorValue deltaValue in array.NullableValue)
            {
                if (deltaValue is ArrayOperator)
                {
                    subList.AddRange(AppendArrayList((ArrayOperator)deltaValue));
                }
                else
                {
                    subList.Add(deltaValue);
                }
            }
        }

        if (NullableValue != null)
        {
            subList.AddRange(NullableValue);
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
        return new BooleanOperator(NullableValue != null && NullableValue.Count != 0);
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
        if (NullableValue != null)
        {
            return new ArrayOperator(ArrayType, NullableValue);
        }

        if (parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return parameters[0];
    }

    public override OperatorValue? Includes(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute includes.");
        }

        // pass thru of parameters to ValueEqual to all elements in array and returns
        return new BooleanOperator(NullableValue.Any(_item => ((BooleanOperator?)_item.ValueEqual(parameters))?.GetValue() ?? false));
    }

    public override OperatorValue? IndexOf(OperatorValue[]? parameters)
    {
        // pass thru of parameters to ValueEqual and returns the first match, or if none, null
        return NullableValue.FirstOrDefault(_item => ((BooleanOperator?)_item.ValueEqual(parameters))?.GetValue() ?? false);
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Join(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute join.");
        }

        List<string> stringList = new();
        foreach (OperatorValue deltaValue in NullableValue)
        {
            stringList.Add(deltaValue.ToStringValue());
        }

        string? separator = parameters[0].ToStringValue();

        return new StringOperator(string.Join(separator, stringList));
    }

    public override OperatorValue? Length(OperatorValue[]? parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("value null. Cannot execute length.");
        }

        return new IntegerOperator(NullableValue.Count);
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
        if (NullableValue == null || parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute replace.");
        }

        List<OperatorValue> _outputContents = [];
        OperatorValue _toRemove = parameters[0];
        OperatorValue _toAdd = parameters[1];

        // check which match and replace those that do (be aware: THESE ARE BYREF, NOT COPIES)
        foreach (OperatorValue _deltaValue in NullableValue)
        {
            if (((BooleanOperator?)_deltaValue.ValueEqual([_toRemove]))?.GetValue() ?? false)
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
        if (NullableValue == null)
        {
            return "null";
        }

        StringBuilder _output = new StringBuilder();
        _output.Append('[');

        if (NullableValue.Count != 0)
        {
            foreach (var _deltaValue in NullableValue)
            {
                _output.Append(_deltaValue.ToJsonStringValue());
                _output.Append(',');
            }

            // remove trailing comma
            _output.Length -= 1;
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
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        List<string> stringList = new();
        foreach (OperatorValue deltaValue in NullableValue)
        {
            stringList.Add(deltaValue.ToStringValue());
        }

        return string.Join(',', stringList);
    }

    public override void SetValue(OperatorValue value)
    {
        if (value is not ArrayOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not an array.");
        }

        ArrayType = ((ArrayOperator)value).ArrayType;
        NullableValue = ((ArrayOperator)value).NullableValue;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns>EMPTY ARRAY - caller is expected to fill it</returns>
    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        // this is a special case where the ArrayOperator is created blank and the caller fills it

        if (parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a ArrayOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_input"></param>
    /// <returns>EMPTY ARRAY - caller is expected to fill it</returns>
    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            throw new ArgumentException("Cannot create an array without a designated OperatorValueType.");
        }

        if (OperatorValue.OperatorValueTypeLookup.TryGetValue(_input, out var _arrayType))
        {
            return new ArrayOperator(_arrayType, []);
        }
        else
        {
            throw new Exception(
                string.Format(
                    "Unable to build blank array, specified type '{0}' null or unknown.",
                    _input
                )
            );
        }
    }

}