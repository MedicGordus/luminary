
using System.Text;

namespace luminary.mapping.functions;

public class PrismOperator : OperatorValue
{
    protected Dictionary<string, OperatorValue>? NullableValue;

    public Dictionary<string, OperatorValue>? GetValue() => NullableValue;

    public PrismOperator(Dictionary<string, OperatorValue>? _value) : base(OperatorValueType.Prism)
    {
        NullableValue = _value;
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

    public override OperatorValue? BooleanAnd(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Concatenate(OperatorValue[]? _parameters)
    {
        if (_parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Parameter null or parameter missing. Cannot execute concatenate.");
        }

        if (_parameters[0] is not PrismOperator)
        {
            throw new ArgumentException("Cannot concatenate when the parameter is not a prism.");
        }

        var param = ((PrismOperator)_parameters[0]).NullableValue;

        Dictionary<string, OperatorValue> output = [];

        if (NullableValue != null)
        {
            foreach (KeyValuePair<string, OperatorValue> deltaProperty in NullableValue)
            {
                output.Add(deltaProperty.Key, deltaProperty.Value);
            }
        }

        if (param != null)
        {
            foreach (KeyValuePair<string, OperatorValue> deltaProperty in param)
            {
                output.Add(deltaProperty.Key, deltaProperty.Value);
            }
        }

        return new PrismOperator(output);
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
        return new StringOperator(ToStringValue());
    }

    public override OperatorValue? EndsWith(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Filled(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
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
        if (NullableValue == null || NullableValue.Count == 0)
        {
            if (_parameters == null || _parameters.Length == 0 || _parameters[0] is not OperatorValue)
            {
                throw new ArgumentException("Parameters null, parameter missing, or wrong parameter type. Cannot execute ifnotfilled.");
            }

            return _parameters[0];
        }

        return new PrismOperator(NullableValue);
    }

    public override OperatorValue? Includes(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? IndexOf(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Join(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Length(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
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

    public override OperatorValue? Replace(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
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

    public override OperatorValue? ValueEqual(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override string ToStringValue()
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        StringBuilder output = new();

        foreach (KeyValuePair<string, OperatorValue> deltaKeyValuePair in NullableValue)
        {
            output.Append(deltaKeyValuePair.Key);
            output.Append('|');
            output.Append(deltaKeyValuePair.Value.ToStringValue());
            output.Append(',');
        }

        return output.ToString();
    }

    public override string ToJsonStringValue()
    {
        if (NullableValue == null)
        {
            return "null";
        }

        StringBuilder output = new StringBuilder();

        output.Append('{');

        if (NullableValue.Count != 0)
        {
            foreach (KeyValuePair<string, OperatorValue> deltaKeyValuePair in NullableValue)
            {
                output.Append(
                    string.Format(
                        "\"{0}\":",
                        deltaKeyValuePair.Key
                    )
                );

                output.Append(
                    deltaKeyValuePair.Value.ToJsonStringValue()
                );

                output.Append(',');
            }

            // remove the last comma
            output.Length -= 1;
        }

        output.Append('}');

        return output.ToString();
    }

    public override void SetValue(OperatorValue _value)
    {
        if (_value is not PrismOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a prism.");
        }

        NullableValue = ((PrismOperator)_value).NullableValue;
    }

    public OperatorValue? GetOperatorByName(string _name)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute getoperatorbyname.");
        }

        return NullableValue.TryGetValue(_name, out var value) ? value : null;
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        throw new InvalidOperationException();
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        throw new InvalidOperationException();
    }
}