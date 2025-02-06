using System.Numerics;
using System.Text;
using System.Text.Json;

namespace luminary.functions;

public class StringOperator : OperatorValue
{
    protected string? NullableValue;

    public string? GetValue() => NullableValue;

    public StringOperator(string? _nullableValue) : base(OperatorValueType.String)
    {
        NullableValue = _nullableValue;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwiseleftshift.");
        }

        if(NullableValue.Length == 0)
        {
            return new StringOperator(NullableValue);
        }

        if(parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot bitwiseleftshift when the parameter is not an integer.");
        }

        int? _numberOfCharsToMove = ((IntegerOperator)parameters[0]).GetValue();

        if(_numberOfCharsToMove == null)
        {
            throw new ArgumentException("Cannot bitwiseleftshift when the parameter is null.");
        }
        // Extract the characters to move
        string leftPart = NullableValue.Substring(0, _numberOfCharsToMove.Value);
        
        // Extract the rest of the strzing after the characters to move
        string rightPart = NullableValue.Substring(_numberOfCharsToMove.Value);

        // Concatenate the rest of the string with the characters moved to the right
        return new StringOperator(rightPart + leftPart);
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null || NullableValue == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwiserightshift.");
        }

        if(NullableValue.Length == 0)
        {
            return new StringOperator(NullableValue);
        }

        if(parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot bitwiserightshift when the parameter is not an integer.");
        }

        int? _numberOfCharsToMove = ((IntegerOperator)parameters[0]).GetValue();

        if(_numberOfCharsToMove == null)
        {
            throw new ArgumentException("Cannot bitwiserightshift when the parameter is null.");
        }

        int _adjustedNumberOfCharsToMove = NullableValue.Length - _numberOfCharsToMove.Value;

        // Extract the characters to move
        string leftPart = NullableValue.Substring(0, _adjustedNumberOfCharsToMove);
        
        // Extract the rest of the strzing after the characters to move
        string rightPart = NullableValue.Substring(_adjustedNumberOfCharsToMove);

        // Concatenate the rest of the string with the characters moved to the right
        return new StringOperator(rightPart + leftPart);
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Concatenate(OperatorValue[]? parameters)
    {
        // if there is nothing to concatenate, return copy of ourself
        if(parameters == null || parameters.Length == 0)
        {
            return new StringOperator(NullableValue);
        }

        StringBuilder sb = new StringBuilder(NullableValue);
        foreach(OperatorValue? deltaValue in parameters)
        {
            if(deltaValue != null)
            {
                if(deltaValue is ArrayOperator)
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

    protected string AppendArrayAsString(ArrayOperator array)
    {
        StringBuilder sb = new StringBuilder();
        foreach(OperatorValue deltaValue in array.Value)
        {
            if(deltaValue is ArrayOperator)
            {
                sb.Append(AppendArrayAsString((ArrayOperator)deltaValue));
            }
            else
            {
                sb.Append(deltaValue.ToStringValue());
            }
        }
        return sb.ToString();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentNullException("Cannot parse null string to big integer.");
        }

        return new BigIntegerOperator(BigInteger.Parse(NullableValue));
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters)
    {
        var (_value, _unit) = UnitHelper.ParseStringToBigIntegerUnits(NullableValue);
        
        return new BigIntegerUnitsOperator(_value, _unit);
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentNullException("Cannot parse null string to decimal.");
        }

        return new DecimalOperator(decimal.Parse(NullableValue));
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        var (_value, _unit) = UnitHelper.ParseStringToDecimalUnits(NullableValue);
        
        return new DecimalUnitsOperator(_value, _unit);
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentNullException("Cannot parse null string to double.");
        }

        return new DoubleOperator(double.Parse(NullableValue));
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters)
    {
        var (_value, _unit) = UnitHelper.ParseStringToDoubleUnits(NullableValue);
        
        return new DoubleUnitsOperator(_value, _unit);
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentNullException("Cannot parse null string to integer.");
        }

        return new IntegerOperator(int.Parse(NullableValue));
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        var (_value, _unit) = UnitHelper.ParseStringToIntegerUnits(NullableValue);
        
        return new IntegerUnitsOperator(_value, _unit);
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? parameters)
    {
        return new StringOperator(NullableValue);
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute endswith.");
        }

        return new BooleanOperator(NullableValue.EndsWith(parameters[0].ToStringValue()));
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute valueequal.");
        }

        if(NullableValue == null)
        {
            // return if they are both null or not
            return new BooleanOperator(parameters[0].ToStringValue() == null);
        }

        return new BooleanOperator(NullableValue.Equals(parameters[0].ToStringValue()));
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(NullableValue != null && NullableValue.Length > 0);
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
        if(NullableValue != null)
        {
            return new StringOperator(NullableValue);
        }

        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return parameters[0];
    }

    public override OperatorValue? Includes(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute includes.");
        }

        return new BooleanOperator(NullableValue.Contains(parameters[0].ToStringValue()));
    }

    public override OperatorValue? IndexOf(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute indexof.");
        }

        return new IntegerOperator(NullableValue.IndexOf(parameters[0].ToStringValue()));
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute valueequal.");
        }

        if(NullableValue == null)
        {
            // return if they are both null or not
            return new BooleanOperator(parameters[0].ToStringValue() == null);
        }

        return new BooleanOperator(NullableValue.ToLower().Equals(parameters[0].ToStringValue().ToLower()));
    }

    public override OperatorValue? Join(OperatorValue[]? parameters)
    {
        // this is not an array, so it can return itself
        return new StringOperator(NullableValue);
    }

    public override OperatorValue? Length(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute operatorvalue.");
        }

        return new IntegerOperator(NullableValue.Length);
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
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute valueequal.");
        }

        if(NullableValue == null)
        {
            // return if one is not and the other is not (or not)
            return new BooleanOperator(parameters[0].ToStringValue() != null);
        }

        return new BooleanOperator(!NullableValue.Equals(parameters[0].ToStringValue()));
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Replace(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute replace.");
        }

        string oldReplace = parameters[0].ToStringValue();
        string newReplace = parameters[1].ToStringValue();

        return new StringOperator(NullableValue.Replace(oldReplace, newReplace));
    }

    public override OperatorValue? Split(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute split.");
        }

        string? separator = parameters[0].ToStringValue();
        string[] stringArray = NullableValue.Split(separator);
        List<OperatorValue> strings = new();
        foreach(string deltaString in stringArray)
        {
            strings.Add(new StringOperator(deltaString));
        }

        return new ArrayOperator(OperatorValueType.String, strings);
    }

    public override OperatorValue? StartsWith(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute startswith.");
        }

        return new BooleanOperator(NullableValue.StartsWith(parameters[0].ToStringValue()));
    }

    public override OperatorValue? Substring(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute startswith.");
        }

        int start = int.Parse(parameters[0].ToStringValue());
        int? length = null;
        string? newString;

        if(parameters.Length <= 2)
        {
            length = int.Parse(parameters[1].ToStringValue());
        }

        if(length == null)
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
        if(NullableValue == null)
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

    public override OperatorValue? ToLower(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tolower.");
        }

        return new StringOperator(NullableValue.ToLower());
    }

    public override OperatorValue? ToUpper(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute toupper.");
        }

        return new StringOperator(NullableValue.ToUpper());
    }

    public override OperatorValue? Trim(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute trim.");
        }

        return new StringOperator(NullableValue.Trim());
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override string ToStringValue()
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        return NullableValue;
    }

    public override void SetValue(OperatorValue source)
    {
        if(source is not StringOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((StringOperator)source).NullableValue;
    }

    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a StringOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }
    
    public static OperatorValue BuildFromString(string? _input)
    {
        return new StringOperator(_input);
    }
}