
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
        if (NullableValue == null || _parameters == null)
        {
            throw new ArgumentException("Value null or parameters null. Cannot execute concatenate.");
        }

        List<OperatorValue> newArray = new List<OperatorValue>();
        foreach (OperatorValue deltaValue in _parameters)
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

    protected List<OperatorValue> AppendArrayList(ArrayOperator _array)
    {
        List<OperatorValue> subList = new List<OperatorValue>();

        if (_array != null && _array.NullableValue != null)
        {
            foreach (OperatorValue deltaValue in _array.NullableValue)
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

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? EndsWith(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Filled(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(NullableValue != null && NullableValue.Count != 0);
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
        if (NullableValue != null)
        {
            return new ArrayOperator(ArrayType, NullableValue);
        }

        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return _parameters[0];
    }

    public override OperatorValue? Includes(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute includes.");
        }

        // pass thru of parameters to ValueEqual to all elements in array and returns
        return new BooleanOperator(NullableValue.Any(_item => ((BooleanOperator?)_item.ValueEqual(_parameters))?.GetValue() ?? false));
    }

    public override OperatorValue? IndexOf(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute indexof.");
        }

        // pass thru of parameters to ValueEqual and returns the first match, or if none, null
        return NullableValue.FirstOrDefault(_item => ((BooleanOperator?)_item.ValueEqual(_parameters))?.GetValue() ?? false);
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Join(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute join.");
        }

        List<string> stringList = new();
        foreach (OperatorValue deltaValue in NullableValue)
        {
            stringList.Add(deltaValue.ToStringValue());
        }

        string? separator = _parameters[0].ToStringValue();

        return new StringOperator(string.Join(separator, stringList));
    }

    public override OperatorValue? Length(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("value null. Cannot execute length.");
        }

        return new IntegerOperator(NullableValue.Count);
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? LessThan(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathAdd(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? MathAverage(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
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
        throw new InvalidOperationException();
    }

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Replace(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length < 2)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute replace.");
        }

        List<OperatorValue> outputContents = [];
        OperatorValue toRemove = _parameters[0];
        OperatorValue toAdd = _parameters[1];

        // check which match and replace those that do (be aware: THESE ARE BYREF, NOT COPIES)
        foreach (OperatorValue deltaValue in NullableValue)
        {
            if (((BooleanOperator?)deltaValue.ValueEqual([toRemove]))?.GetValue() ?? false)
            {
                outputContents.Add(toAdd);
            }
            else
            {
                outputContents.Add(deltaValue);
            }
        }

        return new ArrayOperator(ArrayType, outputContents);
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

    public override string ToJsonStringValue()
    {
        if (NullableValue == null)
        {
            return "null";
        }

        StringBuilder output = new StringBuilder();
        output.Append('[');

        if (NullableValue.Count != 0)
        {
            foreach (var deltaValue in NullableValue)
            {
                output.Append(deltaValue.ToJsonStringValue());
                output.Append(',');
            }

            // remove trailing comma
            output.Length -= 1;
        }

        output.Append(']');
        return output.ToString();
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

    public override void SetValue(OperatorValue _value)
    {
        if (_value is not ArrayOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not an array.");
        }

        ArrayType = ((ArrayOperator)_value).ArrayType;
        NullableValue = ((ArrayOperator)_value).NullableValue;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="_parameters"></param>
    /// <returns>EMPTY ARRAY - caller is expected to fill it</returns>
    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        // this is a special case where the ArrayOperator is created blank and the caller fills it

        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a ArrayOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
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

        if (OperatorValue.OperatorValueTypeLookup.TryGetValue(_input, out var arrayType))
        {
            return new ArrayOperator(arrayType, []);
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