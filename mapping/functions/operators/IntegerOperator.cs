
namespace luminary.mapping.functions;

public class IntegerOperator : OperatorValue
{
    protected int? NullableValue;

    public int? GetValue() => NullableValue;
    public IntegerOperator(int? _nullableValue, OperatorValueType _type = OperatorValueType.Integer) : base(_type)
    {
        NullableValue = _nullableValue;
    }

    protected int InheritableBooleanAnd(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanand.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot booleanand when the parameter is not an integer.");
        }

        int paramValue = ((IntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute booleanand.");

        return NullableValue.Value & paramValue;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableBooleanAnd(_parameters));
    }

    protected int InheritableBitwiseLeftShift(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwiseleftshift.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot bitwiseleftshift when the parameter is not an integer.");
        }

        return NullableValue.Value << ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Parameter not null but GetValue() unexpectedly returned null. Cannot execute bitwiseleftshift"); ;
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableBitwiseLeftShift(_parameters));
    }

    protected int InheritableBitwiseMod(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisemod.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot bitwisemod when the parameter is not an integer.");
        }

        int toMod = ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Parameter was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value % toMod;
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableBitwiseMod(_parameters));
    }

    protected int InheritableBitwiseRightShift(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwiserightshift.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot rightshift when the parameter is not an integer.");
        }

        return NullableValue >> ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Parameter was not null but the GetValue unexpectedly returned null."); ;
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableBitwiseRightShift(_parameters));
    }

    protected int InheritableBitwiseXor(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisexor.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot rightshift when the parameter is not an integer.");
        }

        int paramValue = ((IntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute bitwisexor.");

        return NullableValue.Value ^ paramValue;
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableBitwiseXor(_parameters));
    }

    public override OperatorValue? Concatenate(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? _parameters)
    {
        return BigIntegerOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? _parameters)
    {
        return DecimalOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? _parameters)
    {
        return DoubleOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? _parameters)
    {
        return IntegerOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute converttointegerunits.");
        }

        if (_parameters[0] is not StringOperator)
        {
            throw new ArgumentException("Cannot converttointegerunits when the unit parameter is not a string.");
        }

        return new IntegerUnitsOperator(NullableValue, ((StringOperator)_parameters[0]).GetValue());
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? _parameters)
    {
        return StringOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? EndsWith(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    protected bool InheritableValueEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not an integer.");
        }

        return NullableValue == ((IntegerOperator)_parameters[0]).NullableValue;
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableValueEqual(_parameters));
    }

    public override OperatorValue? Filled(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(NullableValue != null);
    }

    protected bool InheritableGreaterOrEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterorequal.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not an integer.");
        }

        int check = ((IntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterorequal.");

        return NullableValue >= check;
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableGreaterOrEqual(_parameters));
    }

    protected bool InheritableGreatherThan(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not an integer.");
        }

        int check = ((IntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterthan.");

        return NullableValue > check;
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableGreatherThan(_parameters));
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? _parameters)
    {
        if (NullableValue != null)
        {
            return new IntegerOperator(NullableValue);
        }

        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return _parameters[0];
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

    protected bool InheritableLessOrEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessorequal.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not an integer.");
        }

        int check = ((IntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessorequal.");

        return NullableValue <= check;
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableLessOrEqual(_parameters));
    }

    protected bool InheritableLessThan(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not an integer.");
        }

        int check = ((IntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessthan.");

        return NullableValue < check;
    }

    public override OperatorValue? LessThan(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableLessThan(_parameters));
    }

    protected int InheritableMathAdd(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not an integer.");
        }

        int toAdd = ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value + toAdd;
    }

    public override OperatorValue? MathAdd(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableMathAdd(_parameters));
    }

    protected int InheritableMathAverage(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaverage.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not an integer.");
        }

        int toAverage = ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Average value was not null but the GetValue unexpectedly returned null.");

        return (NullableValue.Value + toAverage) >> 1;
    }

    public override OperatorValue? MathAverage(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableMathAverage(_parameters));
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    protected int InheritableMathDivide(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathdivide.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not an integer.");
        }

        int toDivide = ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Divide value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value / toDivide;
    }

    public override OperatorValue? MathDivide(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableMathDivide(_parameters));
    }

    public override OperatorValue? MathFloor(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    protected int InheritableMathMultiply(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathmultiply.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not an integer.");
        }

        int toMultiply = ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Multiply value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value * toMultiply;
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableMathMultiply(_parameters));
    }

    protected int InheritableMathPower(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathpower.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathpower when the parameter is not an integer.");
        }

        int toPow = ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Power value was not null but the GetValue unexpectedly returned null.");

        return (int)Math.Pow(NullableValue.Value, toPow);
    }

    public override OperatorValue? MathPower(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableMathPower(_parameters));
    }

    public override OperatorValue? MathRound(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    protected int InheritableMathSubtract(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathsubtract.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not an integer.");
        }

        int toSubtract = ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Subtract value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value - toSubtract;
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableMathSubtract(_parameters));
    }

    protected bool InheritableNotEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not an integer.");
        }

        return NullableValue != ((IntegerOperator)_parameters[0]).NullableValue;
    }

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableNotEqual(_parameters));
    }

    protected int InheritableBooleanOr(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanor.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot booleanor when the parameter is not an integer.");
        }

        int paramValue = ((IntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute booleanor.");

        return NullableValue.Value | paramValue;
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableBooleanOr(_parameters));
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
        return NullableValue?.ToString() ?? "null";
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

    protected int InheritableBooleanNot(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute booleannot.");
        }

        return ~NullableValue.Value;
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? _parameters)
    {
        return new IntegerOperator(InheritableBooleanNot(_parameters));
    }

    public override string ToStringValue()
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        return NullableValue.Value.ToString();
    }

    public override void SetValue(OperatorValue _source)
    {
        if (_source is not IntegerOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((IntegerOperator)_source).NullableValue;
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create an IntegerOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new IntegerOperator(null);
        }

        if (int.TryParse(_input, out int value))
        {
            return new IntegerOperator(value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to int.",
                    _input
                )
            );
        }
    }

    public void SetNativeValue(int? _input)
    {
        NullableValue = _input;
    }
}