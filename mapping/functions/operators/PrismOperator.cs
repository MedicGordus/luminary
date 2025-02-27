
using System.Text;

namespace luminary.mapping.functions;

public class PrismOperator : OperatorValue
{
    protected Dictionary<string, OperatorValue>? NullableValue;

    public Dictionary<string, OperatorValue>? GetValue() => NullableValue;

    public PrismOperator(Dictionary<string, OperatorValue>? value) : base(OperatorValueType.Prism)
    {
        NullableValue = value;
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

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Concatenate(OperatorValue[]? parameters)
    {
        if (parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Parameter null or parameter missing. Cannot execute concatenate.");
        }

        if (parameters[0] is not PrismOperator)
        {
            throw new ArgumentException("Cannot concatenate when the parameter is not a prism.");
        }

        var _param = ((PrismOperator)parameters[0]).NullableValue;

        Dictionary<string, OperatorValue> _output = [];

        if (NullableValue != null)
        {
            foreach (KeyValuePair<string, OperatorValue> _deltaProperty in NullableValue)
            {
                _output.Add(_deltaProperty.Key, _deltaProperty.Value);
            }
        }

        if (_param != null)
        {
            foreach (KeyValuePair<string, OperatorValue> _deltaProperty in _param)
            {
                _output.Add(_deltaProperty.Key, _deltaProperty.Value);
            }
        }

        return new PrismOperator(_output);
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
        return new StringOperator(ToStringValue());
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
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
        if (NullableValue == null || NullableValue.Count == 0)
        {
            if (parameters == null || parameters.Length == 0 || parameters[0] is not OperatorValue)
            {
                throw new ArgumentException("Parameters null, parameter missing, or wrong parameter type. Cannot execute ifnotfilled.");
            }

            return parameters[0];
        }

        return new PrismOperator(NullableValue);
    }

    public override OperatorValue? Includes(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? IndexOf(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Join(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Length(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
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

    public override OperatorValue? Replace(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
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

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override string ToStringValue()
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        StringBuilder _output = new();

        foreach (KeyValuePair<string, OperatorValue> _deltaKeyValuePair in NullableValue)
        {
            _output.Append(_deltaKeyValuePair.Key);
            _output.Append('|');
            _output.Append(_deltaKeyValuePair.Value.ToStringValue());
            _output.Append(',');
        }

        return _output.ToString();
    }

    public override string ToJsonStringValue()
    {
        if (NullableValue == null)
        {
            return "null";
        }

        StringBuilder _output = new StringBuilder();

        _output.Append('{');

        if (NullableValue.Count != 0)
        {
            foreach (KeyValuePair<string, OperatorValue> _deltaKeyValuePair in NullableValue)
            {
                _output.Append(
                    string.Format(
                        "\"{0}\":",
                        _deltaKeyValuePair.Key
                    )
                );

                _output.Append(
                    _deltaKeyValuePair.Value.ToJsonStringValue()
                );

                _output.Append(',');
            }

            // remove the last comma
            _output.Length -= 1;
        }

        _output.Append('}');

        return _output.ToString();
    }

    public override void SetValue(OperatorValue value)
    {
        if (value is not PrismOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a prism.");
        }

        NullableValue = ((PrismOperator)value).NullableValue;
    }

    public OperatorValue? GetOperatorByName(string name)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute getoperatorbyname.");
        }

        return NullableValue.TryGetValue(name, out var _value) ? _value : null;
    }

    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        throw new InvalidOperationException();
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        throw new InvalidOperationException();
    }
}