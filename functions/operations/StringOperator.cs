using System.Numerics;
using System.Text;
using System.Text.Json;

namespace luminary.functions;

public class StringOperator : OperatorValue
{
    protected string? Value;

    public StringOperator(string? value) : base(OperatorValueType.String)
    {
        Value = value;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? parameters)
    {
        if(parameters == null || parameters.Length == 0 || parameters[0] == null || Value == null)
        {
            throw new ArgumentException("Input null or value null.");
        }

        if(Value.Length == 0)
        {
            return new StringOperator(Value);
        }

        string? strNumberOfCharsToMove = parameters[0].ToJsonStringValue();
        if(strNumberOfCharsToMove == null)
        {
            throw new ArgumentException("Unable to parse number of characters to move.");
        }

        int numberOfCharsToMove = int.Parse(strNumberOfCharsToMove);

        // Extract the characters to move
        string leftPart = Value.Substring(0, numberOfCharsToMove);
        
        // Extract the rest of the strzing after the characters to move
        string rightPart = Value.Substring(numberOfCharsToMove);

        // Concatenate the rest of the string with the characters moved to the right
        return new StringOperator(rightPart + leftPart);
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? parameters)
    {
        if(parameters == null || parameters.Length == 0 || parameters[0] == null || Value == null)
        {
            throw new ArgumentException("Input null or value null.");
        }

        if(Value.Length == 0)
        {
            return new StringOperator(Value);
        }

        string? strNumberOfCharsToMove = parameters[0].ToJsonStringValue();
        if(strNumberOfCharsToMove == null)
        {
            throw new ArgumentException("Unable to parse number of characters to move.");
        }

        int numberOfCharsToMove = Value.Length - int.Parse(strNumberOfCharsToMove);

        // Extract the characters to move
        string leftPart = Value.Substring(0, numberOfCharsToMove);
        
        // Extract the rest of the strzing after the characters to move
        string rightPart = Value.Substring(numberOfCharsToMove);

        // Concatenate the rest of the string with the characters moved to the right
        return new StringOperator(rightPart + leftPart);
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Concatenate(OperatorValue[]? parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            return new StringOperator(Value);
        }

        StringBuilder sb = new StringBuilder(Value);
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
        if(Value == null)
        {
            throw new ArgumentNullException("Cannot parse null string to big integer.");
        }

        return new BigIntegerOperator(BigInteger.Parse(Value));
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters)
    {
        var (_value, _unit) = UnitHelper.ParseStringToBigIntegerUnits(Value);
        
        return new BigIntegerUnitsOperator(_value, _unit);
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        if(Value == null)
        {
            throw new ArgumentNullException("Cannot parse null string to decimal.");
        }

        return new DecimalOperator(decimal.Parse(Value));
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        var (_value, _unit) = UnitHelper.ParseStringToDecimalUnits(Value);
        
        return new DecimalUnitsOperator(_value, _unit);
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        if(Value == null)
        {
            throw new ArgumentNullException("Cannot parse null string to double.");
        }

        return new DoubleOperator(double.Parse(Value));
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters)
    {
        var (_value, _unit) = UnitHelper.ParseStringToDoubleUnits(Value);
        
        return new DoubleUnitsOperator(_value, _unit);
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        if(Value == null)
        {
            throw new ArgumentNullException("Cannot parse null string to integer.");
        }

        return new IntegerOperator(int.Parse(Value));
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        var (_value, _unit) = UnitHelper.ParseStringToIntegerUnits(Value);
        
        return new IntegerUnitsOperator(_value, _unit);
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? parameters)
    {
        return new StringOperator(Value);
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        if(Value == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute endswith.");
        }

        return new BooleanOperator(Value.EndsWith(parameters[0].ToStringValue()));
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute valueequal.");
        }

        if(Value == null)
        {
            // return if they are both null or not
            return new BooleanOperator(parameters[0].ToStringValue() == null);
        }

        return new BooleanOperator(Value.Equals(parameters[0].ToStringValue()));
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(Value != null && Value.Length > 0);
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
            return new StringOperator(Value);
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
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute includes.");
        }

        return new BooleanOperator(Value.Contains(parameters[0].ToStringValue()));
    }

    public override OperatorValue? IndexOf(OperatorValue[]? parameters)
    {
        if(Value == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute indexof.");
        }

        return new IntegerOperator(Value.IndexOf(parameters[0].ToStringValue()));
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute valueequal.");
        }

        if(Value == null)
        {
            // return if they are both null or not
            return new BooleanOperator(parameters[0].ToStringValue() == null);
        }

        return new BooleanOperator(Value.ToLower().Equals(parameters[0].ToStringValue().ToLower()));
    }

    public override OperatorValue? Join(OperatorValue[]? parameters)
    {
        // this is not an array, so it can return itself
        return new StringOperator(Value);
    }

    public override OperatorValue? Length(OperatorValue[]? parameters)
    {
        if(Value == null)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute operatorvalue.");
        }

        return new IntegerOperator(Value.Length);
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

        if(Value == null)
        {
            // return if one is not and the other is not (or not)
            return new BooleanOperator(parameters[0].ToStringValue() != null);
        }

        return new BooleanOperator(!Value.Equals(parameters[0].ToStringValue()));
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Replace(OperatorValue[]? parameters)
    {
        if(Value == null || parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute replace.");
        }

        string oldReplace = parameters[0].ToStringValue();
        string newReplace = parameters[1].ToStringValue();

        return new StringOperator(Value.Replace(oldReplace, newReplace));
    }

    public override OperatorValue? Split(OperatorValue[]? parameters)
    {
        if(Value == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("value null, parameters null or parameter missing. Cannot execute split.");
        }

        string? separator = parameters[0].ToStringValue();
        string[] stringArray = Value.Split(separator);
        List<OperatorValue> strings = new();
        foreach(string deltaString in stringArray)
        {
            strings.Add(new StringOperator(deltaString));
        }

        return new ArrayOperator(OperatorValueType.String, strings);
    }

    public override OperatorValue? StartsWith(OperatorValue[]? parameters)
    {
        if(Value == null || parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute startswith.");
        }

        return new BooleanOperator(Value.StartsWith(parameters[0].ToStringValue()));
    }

    public override OperatorValue? Substring(OperatorValue[]? parameters)
    {
        if(Value == null || parameters == null || parameters.Length == 0)
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
            newString = Value.Substring(start);
        }
        else
        {
            newString = Value.Substring(start, length.Value);
        }

        return new StringOperator(newString);
    }

    public override string ToJsonStringValue()
    {
        if(Value == null)
        {
            return "null";
        }
        else
        {
            return string.Format(
                "\"{0}\"",
                JsonSerializer.Serialize(Value)
            );
        }
    }

    public override OperatorValue? ToLower(OperatorValue[]? parameters)
    {
        if(Value == null)
        {
            throw new ArgumentException("Value null. Cannot execute tolower.");
        }

        return new StringOperator(Value.ToLower());
    }

    public override OperatorValue? ToUpper(OperatorValue[]? parameters)
    {
        if(Value == null)
        {
            throw new ArgumentException("Value null. Cannot execute toupper.");
        }

        return new StringOperator(Value.ToUpper());
    }

    public override OperatorValue? Trim(OperatorValue[]? parameters)
    {
        if(Value == null)
        {
            throw new ArgumentException("Value null. Cannot execute trim.");
        }

        return new StringOperator(Value.Trim());
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override string ToStringValue()
    {
        if(Value == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        return Value;
    }

    public override void SetValue(OperatorValue source)
    {
        if(source is not StringOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        Value = ((StringOperator)source).Value;
    }
}