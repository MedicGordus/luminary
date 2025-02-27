
namespace luminary.mapping.functions;

public class IntegerOperator : OperatorValue
{
    protected int? NullableValue;

    public int? GetValue() => NullableValue;
    public IntegerOperator(int? _nullableValue, OperatorValueType _type = OperatorValueType.Integer) : base(_type)
    {
        NullableValue = _nullableValue;
    }

    protected int InheritableBooleanAnd(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanand.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot booleanand when the parameter is not an integer.");
        }

        int _paramValue = ((IntegerOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute booleanand.");

        return NullableValue.Value & _paramValue;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableBooleanAnd(parameters));
    }

    protected int InheritableBitwiseLeftShift(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwiseleftshift.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot bitwiseleftshift when the parameter is not an integer.");
        }

        return NullableValue.Value << ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Parameter not null but GetValue() unexpectedly returned null. Cannot execute bitwiseleftshift"); ;
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableBitwiseLeftShift(parameters));
    }

    protected int InheritableBitwiseMod(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisemod.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot bitwisemod when the parameter is not an integer.");
        }

        int _toMod = ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Parameter was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value % _toMod;
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableBitwiseMod(parameters));
    }

    protected int InheritableBitwiseRightShift(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwiserightshift.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot rightshift when the parameter is not an integer.");
        }

        return NullableValue >> ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Parameter was not null but the GetValue unexpectedly returned null."); ;
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableBitwiseRightShift(parameters));
    }

    protected int InheritableBitwiseXor(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisexor.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot rightshift when the parameter is not an integer.");
        }

        int _paramValue = ((IntegerOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute bitwisexor.");

        return NullableValue.Value ^ _paramValue;
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableBitwiseXor(parameters));
    }

    public override OperatorValue? Concatenate(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? parameters)
    {
        return BigIntegerOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        return DecimalOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        return DoubleOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        return IntegerOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute converttointegerunits.");
        }

        if (parameters[0] is not StringOperator)
        {
            throw new ArgumentException("Cannot converttointegerunits when the unit parameter is not a string.");
        }

        return new IntegerUnitsOperator(NullableValue, ((StringOperator)parameters[0]).GetValue());
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? parameters)
    {
        return StringOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? EndsWith(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    protected bool InheritableValueEqual(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not an integer.");
        }

        return NullableValue == ((IntegerOperator)parameters[0]).NullableValue;
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableValueEqual(parameters));
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(NullableValue != null);
    }

    protected bool InheritableGreaterOrEqual(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterorequal.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not an integer.");
        }

        int _check = ((IntegerOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterorequal.");

        return NullableValue >= _check;
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableGreaterOrEqual(parameters));
    }

    protected bool InheritableGreatherThan(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not an integer.");
        }

        int _check = ((IntegerOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterthan.");

        return NullableValue > _check;
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableGreatherThan(parameters));
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
        if (NullableValue != null)
        {
            return new IntegerOperator(NullableValue);
        }

        if (parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return parameters[0];
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

    protected bool InheritableLessOrEqual(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessorequal.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not an integer.");
        }

        int _check = ((IntegerOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessorequal.");

        return NullableValue <= _check;
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableLessOrEqual(parameters));
    }

    protected bool InheritableLessThan(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not an integer.");
        }

        int _check = ((IntegerOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessthan.");

        return NullableValue < _check;
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableLessThan(parameters));
    }

    protected int InheritableMathAdd(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not an integer.");
        }

        int _toAdd = ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value + _toAdd;
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableMathAdd(parameters));
    }

    protected int InheritableMathAverage(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaverage.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not an integer.");
        }

        int _toAverage = ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Average value was not null but the GetValue unexpectedly returned null.");

        return (NullableValue.Value + _toAverage) >> 1;
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableMathAverage(parameters));
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    protected int InheritableMathDivide(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathdivide.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not an integer.");
        }

        int _toDivide = ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Divide value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value / _toDivide;
    }

    public override OperatorValue? MathDivide(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableMathDivide(parameters));
    }

    public override OperatorValue? MathFloor(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    protected int InheritableMathMultiply(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathmultiply.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not an integer.");
        }

        int _toMultiply = ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Multiply value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value * _toMultiply;
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableMathMultiply(parameters));
    }

    protected int InheritableMathPower(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathpower.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathpower when the parameter is not an integer.");
        }

        int _toPow = ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Power value was not null but the GetValue unexpectedly returned null.");

        return (int)Math.Pow(NullableValue.Value, _toPow);
    }

    public override OperatorValue? MathPower(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableMathPower(parameters));
    }

    public override OperatorValue? MathRound(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    protected int InheritableMathSubtract(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathsubtract.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not an integer.");
        }

        int _toSubtract = ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Subtract value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value - _toSubtract;
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableMathSubtract(parameters));
    }

    protected bool InheritableNotEqual(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not an integer.");
        }

        return NullableValue != ((IntegerOperator)parameters[0]).NullableValue;
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableNotEqual(parameters));
    }

    protected int InheritableBooleanOr(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanor.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot booleanor when the parameter is not an integer.");
        }

        int _paramValue = ((IntegerOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute booleanor.");

        return NullableValue.Value | _paramValue;
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableBooleanOr(parameters));
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
        return NullableValue?.ToString() ?? "null";
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

    protected int InheritableBooleanNot(OperatorValue[]? parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute booleannot.");
        }

        return ~NullableValue.Value;
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        return new IntegerOperator(InheritableBooleanNot(parameters));
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

    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if (parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create an IntegerOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new IntegerOperator(null);
        }

        if (int.TryParse(_input, out int _value))
        {
            return new IntegerOperator(_value);
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