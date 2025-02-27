
namespace luminary.mapping.functions;

public class DoubleOperator : OperatorValue
{
    protected double? NullableValue;

    public double? GetValue() => NullableValue;

    public DoubleOperator(double? _nullableValue, OperatorValueType _type = OperatorValueType.Double) : base(_type)
    {
        NullableValue = _nullableValue;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    protected double InheritableBitwiseMod(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisemod.");
        }

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot bitwisemod when the parameter is not a double.");
        }

        double toMod = ((DoubleOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Parameter was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value % toMod;
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableBitwiseMod(_parameters));
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
        throw new InvalidOperationException();
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
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute converttointegerunits.");
        }

        if (_parameters[0] is not StringOperator)
        {
            throw new ArgumentException("Cannot converttodoubleunits when the unit parameter is not a string.");
        }

        return new DoubleUnitsOperator(NullableValue, ((StringOperator)_parameters[0]).GetValue());
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? _parameters)
    {
        return IntegerOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
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

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a double.");
        }

        return NullableValue == ((DoubleOperator)_parameters[0]).NullableValue;
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

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a double.");
        }

        double check = ((DoubleOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterorequal.");

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

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a double.");
        }

        double check = ((DoubleOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterthan.");

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
            return new DoubleOperator(NullableValue);
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

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a double.");
        }

        double check = ((DoubleOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessorequal.");

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

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a double.");
        }

        double check = ((DoubleOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessthan.");

        return NullableValue < check;
    }

    public override OperatorValue? LessThan(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableLessThan(_parameters));
    }

    protected double InheritableMathAdd(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a double.");
        }

        double toAdd = ((DoubleOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value + toAdd;
    }

    public override OperatorValue? MathAdd(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableMathAdd(_parameters));
    }

    protected double InheritableMathAverage(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaverage.");
        }

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not a double.");
        }

        double toAverage = ((DoubleOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Average value was not null but the GetValue unexpectedly returned null.");

        return (NullableValue.Value + toAverage) / 2d;
    }

    public override OperatorValue? MathAverage(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableMathAverage(_parameters));
    }

    protected double InheritableMathCeiling(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute mathceiling.");
        }
        return Math.Ceiling(NullableValue.Value);
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableMathCeiling(_parameters));
    }

    protected double InheritableMathDivide(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathdivide.");
        }

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not a double.");
        }

        double toDivide = ((DoubleOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Divide value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value / toDivide;
    }

    public override OperatorValue? MathDivide(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableMathDivide(_parameters));
    }

    protected double InheritableMathFloor(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute mathfloor.");
        }

        return Math.Floor(NullableValue.Value);
    }

    public override OperatorValue? MathFloor(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableMathFloor(_parameters));
    }

    protected double InheritableMathMultiply(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathmultiply.");
        }

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not a decimal.");
        }

        double toMultiply = ((DoubleOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Multiply value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value * toMultiply;
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableMathMultiply(_parameters));
    }

    protected double InheritableMathPower(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathpower.");
        }

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathpower when the parameter is not a decimal.");
        }

        double toPow = ((DoubleOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Power value was not null but the GetValue unexpectedly returned null.");

        return Math.Pow(NullableValue.Value, toPow);
    }

    public override OperatorValue? MathPower(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableMathPower(_parameters));
    }

    protected double InheritableMathRound(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathround.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathround when the parameter is not an integer.");
        }

        int digits = ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Round value was not null but the GetValue unexpectedly returned null.");

        return Math.Round(NullableValue.Value, digits);
    }

    public override OperatorValue? MathRound(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableMathRound(_parameters));
    }

    protected double InheritableMathSubtract(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathsubtract.");
        }

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not a double.");
        }

        double toSubtract = ((DoubleOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Subtract value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value - toSubtract;
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? _parameters)
    {
        return new DoubleOperator(InheritableMathSubtract(_parameters));
    }

    protected bool InheritableNotEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (_parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not an integer.");
        }

        return NullableValue != ((DoubleOperator)_parameters[0]).NullableValue;
    }

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableNotEqual(_parameters));
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? _parameters)
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

        return NullableValue.Value.ToString();
    }

    public override void SetValue(OperatorValue _source)
    {
        if (_source is not DoubleOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((DoubleOperator)_source).NullableValue;
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DoubleOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new DoubleOperator(null);
        }

        if (double.TryParse(_input, out double value))
        {
            return new DoubleOperator(value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to double.",
                    _input
                )
            );
        }
    }
}