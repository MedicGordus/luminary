using System.Numerics;
using System.Text;
using System.Text.Json;

namespace luminary.mapping.functions;

public class StringOperator : OperatorValue
{
    protected string? NullableValue;

    public string? GetValue() => NullableValue;

    public StringOperator(string? _nullableValue) : base(OperatorValueType.String)
    {
        NullableValue = _nullableValue;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwiseleftshift.");
        }

        if (NullableValue.Length == 0)
        {
            return new StringOperator(NullableValue);
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot bitwiseleftshift when the parameter is not an integer.");
        }

        int? numberOfCharsToMove = ((IntegerOperator)_parameters[0]).GetValue();

        if (numberOfCharsToMove == null)
        {
            throw new ArgumentException("Cannot bitwiseleftshift when the parameter is null.");
        }
        // Extract the characters to move
        string leftPart = NullableValue.Substring(0, numberOfCharsToMove.Value);

        // Extract the rest of the strzing after the characters to move
        string rightPart = NullableValue.Substring(numberOfCharsToMove.Value);

        // Concatenate the rest of the string with the characters moved to the right
        return new StringOperator(rightPart + leftPart);
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null || NullableValue == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwiserightshift.");
        }

        if (NullableValue.Length == 0)
        {
            return new StringOperator(NullableValue);
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot bitwiserightshift when the parameter is not an integer.");
        }

        int? numberOfCharsToMove = ((IntegerOperator)_parameters[0]).GetValue();

        if (numberOfCharsToMove == null)
        {
            throw new ArgumentException("Cannot bitwiserightshift when the parameter is null.");
        }

        int adjustedNumberOfCharsToMove = NullableValue.Length - numberOfCharsToMove.Value;

        // Extract the characters to move
        string leftPart = NullableValue.Substring(0, adjustedNumberOfCharsToMove);

        // Extract the rest of the strzing after the characters to move
        string rightPart = NullableValue.Substring(adjustedNumberOfCharsToMove);

        // Concatenate the rest of the string with the characters moved to the right
        return new StringOperator(rightPart + leftPart);
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Concatenate(OperatorValue[]? _parameters)
    {
        // if there is nothing to concatenate, return copy of ourself
        if (_parameters == null || _parameters.Length == 0)
        {
            return new StringOperator(NullableValue);
        }

        StringBuilder sb = new StringBuilder(NullableValue);
        foreach (OperatorValue? deltaValue in _parameters)
        {
            if (deltaValue != null)
            {
                if (deltaValue is ArrayOperator)
                {
                    sb.Append(AppendArrayAsString((ArrayOperator)deltaValue));
                }
                else
                {
                    sb.Append(deltaValue.ToStringValue());
                }
            }
        }

        return new StringOperator(sb.ToString());
    }

    protected string AppendArrayAsString(ArrayOperator _array)
    {
        StringBuilder sb = new StringBuilder();
        if (_array.NullableValue != null)
        {
            foreach (OperatorValue deltaValue in _array.NullableValue)
            {
                if (deltaValue is ArrayOperator)
                {
                    sb.Append(AppendArrayAsString((ArrayOperator)deltaValue));
                }
                else
                {
                    sb.Append(deltaValue.ToStringValue());
                }
            }
        }
        return sb.ToString();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentNullException("Cannot parse null string to big integer.");
        }

        return new BigIntegerOperator(BigInteger.Parse(NullableValue));
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters)
    {
        var (value, unit) = UnitHelper.ParseStringToBigIntegerUnits(NullableValue);

        return new BigIntegerUnitsOperator(value, unit);
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentNullException("Cannot parse null string to decimal.");
        }

        return new DecimalOperator(decimal.Parse(NullableValue));
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? _parameters)
    {
        var (value, unit) = UnitHelper.ParseStringToDecimalUnits(NullableValue);

        return new DecimalUnitsOperator(value, unit);
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentNullException("Cannot parse null string to double.");
        }

        return new DoubleOperator(double.Parse(NullableValue));
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? _parameters)
    {
        var (value, unit) = UnitHelper.ParseStringToDoubleUnits(NullableValue);

        return new DoubleUnitsOperator(value, unit);
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentNullException("Cannot parse null string to integer.");
        }

        return new IntegerOperator(int.Parse(NullableValue));
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters)
    {
        var (value, unit) = UnitHelper.ParseStringToIntegerUnits(NullableValue);

        return new IntegerUnitsOperator(value, unit);
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? _parameters)
    {
        return new StringOperator(NullableValue);
    }

    public override OperatorValue? EndsWith(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute endswith.");
        }

        return new BooleanOperator(NullableValue.EndsWith(_parameters[0].ToStringValue()));
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (NullableValue == null)
        {
            // return if they are both null or not
            return new BooleanOperator(_parameters[0].ToStringValue() == null);
        }

        return new BooleanOperator(NullableValue == _parameters[0].ToStringValue());
    }

    public override OperatorValue? Filled(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(NullableValue != null && NullableValue.Length > 0);
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
            return new StringOperator(NullableValue);
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
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute includes.");
        }

        return new BooleanOperator(NullableValue.Contains(_parameters[0].ToStringValue()));
    }

    public override OperatorValue? IndexOf(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute indexof.");
        }

        return new IntegerOperator(NullableValue.IndexOf(_parameters[0].ToStringValue()));
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (NullableValue == null)
        {
            // return if they are both null or not
            return new BooleanOperator(_parameters[0].ToStringValue() == null);
        }

        return new BooleanOperator(NullableValue.ToLower() == _parameters[0].ToStringValue().ToLower());
    }

    public override OperatorValue? Join(OperatorValue[]? _parameters)
    {
        // this is not an array, so it can return itself
        return new StringOperator(NullableValue);
    }

    public override OperatorValue? Length(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute operatorvalue.");
        }

        return new IntegerOperator(NullableValue.Length);
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
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (NullableValue == null)
        {
            // return if one is not and the other is not (or not)
            return new BooleanOperator(_parameters[0].ToStringValue() != null);
        }

        return new BooleanOperator(NullableValue != _parameters[0].ToStringValue());
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Replace(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length < 2)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute replace.");
        }

        string oldReplace = _parameters[0].ToStringValue();
        string newReplace = _parameters[1].ToStringValue();

        return new StringOperator(NullableValue.Replace(oldReplace, newReplace));
    }

    public override OperatorValue? Split(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute split.");
        }

        string? separator = _parameters[0].ToStringValue();
        string[] stringArray = NullableValue.Split(separator);
        List<OperatorValue> strings = new();
        foreach (string deltaString in stringArray)
        {
            strings.Add(new StringOperator(deltaString));
        }

        return new ArrayOperator(
            OperatorValueType.String,
            strings,
            new ()
            {
                Type = SchemaJson.JsonTypes.STRING
            }
        );
    }

    public override OperatorValue? StartsWith(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute startswith.");
        }

        return new BooleanOperator(NullableValue.StartsWith(_parameters[0].ToStringValue()));
    }

    public override OperatorValue? Substring(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute startswith.");
        }

        int start = int.Parse(_parameters[0].ToStringValue());
        int? length = null;
        string? newString;

        if (_parameters.Length <= 2)
        {
            length = int.Parse(_parameters[1].ToStringValue());
        }

        if (length == null)
        {
            newString = NullableValue.Substring(start);
        }
        else
        {
            newString = NullableValue.Substring(start, length.Value);
        }

        return new StringOperator(newString);
    }

    public override string ToJsonStringValue()
    {
        if (NullableValue == null)
        {
            return "null";
        }
        else
        {
            return string.Format(
                "\"{0}\"",
                JsonSerializer.Serialize(NullableValue)
            );
        }
    }

    public override OperatorValue? ToLower(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tolower.");
        }

        return new StringOperator(NullableValue.ToLower());
    }

    public override OperatorValue? ToUpper(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute toupper.");
        }

        return new StringOperator(NullableValue.ToUpper());
    }

    public override OperatorValue? Trim(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute trim.");
        }

        return new StringOperator(NullableValue.Trim());
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

        return NullableValue;
    }

    public override void SetValue(OperatorValue _source)
    {
        if (_source is not StringOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((StringOperator)_source).NullableValue;
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a StringOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        return new StringOperator(_input);
    }
}