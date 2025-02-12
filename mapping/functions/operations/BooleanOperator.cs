
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
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisexor.");
        }

        if(parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot bitwisexor when the parameter is not a boolean.");
        }

        bool? _paramValue = ((BooleanOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot bitwisexor when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value ^ _paramValue.Value);
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanand.");
        }

        if(parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot booleanand when the parameter is not a boolean.");
        }

        bool? _paramValue = ((BooleanOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot booleanand when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value & _paramValue.Value);
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute booleannot.");
        }

        return new BooleanOperator(!NullableValue.Value);
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanor.");
        }

        if(parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot booleanor when the parameter is not a boolean.");
        }

        bool? _paramValue = ((BooleanOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot booleanor when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value | _paramValue.Value);
    }

    public override OperatorValue? Concatenate(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            return new IntegerOperator(null);
        }

        return new BigIntegerOperator(NullableValue.Value ? BigInteger.One : BigInteger.Zero);
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            return new IntegerOperator(null);
        }

        return new DecimalOperator(NullableValue.Value ? 1m : 0m);
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            return new IntegerOperator(null);
        }

        return new DoubleOperator(NullableValue.Value ? 1d : 0d);
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            return new IntegerOperator(null);
        }

        return new IntegerOperator(NullableValue.Value ? 1 : 0);
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? parameters)
    {
        return new StringOperator(NullableValue?.ToString());
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(NullableValue != null);
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
        if(NullableValue == null)
        {
            if(parameters == null || parameters.Length == 0 || parameters[0] == null)
            {
                throw new ArgumentException("Parameters null or parameter missing. Cannot execute ifnotfilled.");
            }

            if(parameters[0] is not BooleanOperator)
            {
                throw new ArgumentException("Cannot ifnotfilled when the parameter is not a boolean.");
            }

            return new BooleanOperator(((BooleanOperator)parameters[0]).GetValue());
        }

        return new BooleanOperator(NullableValue.Value);
    }

    public override OperatorValue? Includes(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? IndexOf(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? EqualsIgnoreCase(OperatorValue[]? parameters)
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
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if(parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a boolean.");
        }

        bool? _paramValue = ((BooleanOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot notequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value != _paramValue.Value);
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

    public override string ToJsonStringValue()
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tojsonstringvalue.");
        }

        return NullableValue.Value ? "true" : "false";
    }

    public override OperatorValue? ToLower(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override string ToStringValue()
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        return NullableValue.Value.ToString();
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
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if(parameters[0] is not BooleanOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a boolean.");
        }

        bool? _paramValue = ((BooleanOperator)parameters[0]).GetValue();

        if(_paramValue == null)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is null.");
        }

        return new BooleanOperator(NullableValue.Value == _paramValue.Value);
    }

    public override void SetValue(OperatorValue value)
    {
        if(value is not BooleanOperator)
        {
            throw new ArgumentException("Cannot setvalue when the parameter is not a boolean.");
        }

        NullableValue = ((BooleanOperator)value).GetValue();
    }
    
    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a BooleanOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }
    
    public static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new BooleanOperator(null);
        }

        if(bool.TryParse(_input, out bool _value))
        {
            return new BooleanOperator(_value);
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
        NullableValue =_input;
    }
}