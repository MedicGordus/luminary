
using System.Numerics;

namespace luminary.mapping.functions;

public class BooleanOperator : OperatorValue
{
    public static readonly OperatorValue TRUE = new BooleanOperator(true);
    public static readonly OperatorValue FALSE = new BooleanOperator(false);

    protected bool? NullableValue;

    public bool? GetValue() => NullableValue;

    public BooleanOperator(bool? _nullableValue) : base(OperatorValueType.Boolean)
    {
        NullableValue = _nullableValue;
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
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisexor.");
        }

        if (_parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot bitwisexor when the parameter is not a boolean.");
        }

        bool? paramValue = ((BooleanOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot bitwisexor when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value ^ paramValue.Value);
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanand.");
        }

        if (_parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot booleanand when the parameter is not a boolean.");
        }

        bool? paramValue = ((BooleanOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot booleanand when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value & paramValue.Value);
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute booleannot.");
        }

        return new BooleanOperator(!NullableValue.Value);
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanor.");
        }

        if (_parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot booleanor when the parameter is not a boolean.");
        }

        bool? paramValue = ((BooleanOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot booleanor when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value | paramValue.Value);
    }

    public override OperatorValue? Concatenate(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            return new IntegerOperator(null);
        }

        return new BigIntegerOperator(NullableValue.Value ? BigInteger.One : BigInteger.Zero);
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            return new IntegerOperator(null);
        }

        return new DecimalOperator(NullableValue.Value ? 1m : 0m);
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            return new IntegerOperator(null);
        }

        return new DoubleOperator(NullableValue.Value ? 1d : 0d);
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            return new IntegerOperator(null);
        }

        return new IntegerOperator(NullableValue.Value ? 1 : 0);
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? _parameters)
    {
        return new StringOperator(NullableValue?.ToString());
    }

    public override OperatorValue? EndsWith(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Filled(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(NullableValue != null);
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
        if (NullableValue == null)
        {
            if (_parameters == null || _parameters.Length == 0 || _parameters[0] == null)
            {
                throw new ArgumentException("Parameters null or parameter missing. Cannot execute ifnotfilled.");
            }

            if (_parameters[0] is not BooleanOperator)
            {
                throw new ArgumentException("Cannot ifnotfilled when the parameter is not a boolean.");
            }

            return new BooleanOperator(((BooleanOperator)_parameters[0]).GetValue());
        }

        return new BooleanOperator(NullableValue.Value);
    }

    public override OperatorValue? Includes(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? IndexOf(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? _parameters)
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
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (_parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a boolean.");
        }

        bool? paramValue = ((BooleanOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot notequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value != paramValue.Value);
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

    public override string ToJsonStringValue()
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tojsonstringvalue.");
        }

        return NullableValue.Value ? "true" : "false";
    }

    public override OperatorValue? ToLower(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override string ToStringValue()
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        return NullableValue.Value.ToString();
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
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (_parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a boolean.");
        }

        bool? paramValue = ((BooleanOperator)_parameters[0]).GetValue();

        if (paramValue == null)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value == paramValue.Value);
    }

    public override void SetValue(OperatorValue _value)
    {
        if (_value is not BooleanOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a boolean.");
        }

        NullableValue = ((BooleanOperator)_value).GetValue();
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a BooleanOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new BooleanOperator(null);
        }

        if (bool.TryParse(_input, out bool value))
        {
            return new BooleanOperator(value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to BooleanOperator.",
                    _input
                )
            );
        }
    }

    public void SetNativeValue(bool? _input)
    {
        NullableValue = _input;
    }
}