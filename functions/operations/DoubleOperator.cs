
namespace luminary.functions;

public class DoubleOperator : OperatorValue
{
    protected double? NullableValue;

    public double? GetValue() => NullableValue;

    public DoubleOperator(double? nullableValue, OperatorValueType _type = OperatorValueType.Double) : base(_type)
    {
        NullableValue = nullableValue;
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    protected double InheritableBitwiseMod(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisemod.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot bitwisemod when the parameter is not a double.");
        }

        double _toMod = ((DoubleOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Parameter was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value % _toMod;
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableBitwiseMod(parameters));
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
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
        throw new InvalidOperationException();
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
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute converttointegerunits.");
        }

        if(parameters[0] is not StringOperator)
        {
            throw new ArgumentException("Cannot converttodoubleunits when the unit parameter is not a string.");
        }

        return new DoubleUnitsOperator(NullableValue, ((StringOperator)parameters[0]).GetValue());
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        return IntegerOperator.BuildFromString(NullableValue?.ToString());
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        throw new InvalidOperationException();
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
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a double.");
        }

        return NullableValue == ((DoubleOperator)parameters[0]).NullableValue;
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
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterorequal.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a double.");
        }

        double _check = ((DoubleOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterorequal.");

        return NullableValue >= _check;
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableGreaterOrEqual(parameters));
    }

    protected bool InheritableGreatherThan(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a double.");
        }

        double _check = ((DoubleOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterthan.");

        return NullableValue > _check;
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableGreatherThan(parameters));
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
        if(NullableValue != null)
        {
            return new DoubleOperator(NullableValue);
        }

        if(parameters == null || parameters.Length == 0)
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
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessorequal.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a double.");
        }

        double _check = ((DoubleOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessorequal.");

        return NullableValue <= _check;
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableLessOrEqual(parameters));
    }

    protected bool InheritableLessThan(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a double.");
        }

        double _check = ((DoubleOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessthan.");

        return NullableValue < _check;
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableLessThan(parameters));
    }

    protected double InheritableMathAdd(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a double.");
        }

        double _toAdd = ((DoubleOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value + _toAdd;
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableMathAdd(parameters));
    }

    protected double InheritableMathAverage(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaverage.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not a double.");
        }

        double _toAverage = ((DoubleOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Average value was not null but the GetValue unexpectedly returned null.");

        return (NullableValue.Value + _toAverage) / 2d;
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableMathAverage(parameters));
    }

    protected double InheritableMathCeiling(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute mathceiling.");
        }
        return Math.Ceiling(NullableValue.Value);
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableMathCeiling(parameters));
    }

    protected double InheritableMathDivide(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathdivide.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not a double.");
        }

        double _toDivide = ((DoubleOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Divide value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value / _toDivide;
    }

    public override OperatorValue? MathDivide(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableMathDivide(parameters));
    }

    protected double InheritableMathFloor(OperatorValue[]? parameters)
    {
        if(NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute mathfloor.");
        }

        return Math.Floor(NullableValue.Value);
    }

    public override OperatorValue? MathFloor(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableMathFloor(parameters));
    }

    protected double InheritableMathMultiply(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathmultiply.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not a decimal.");
        }

        double _toMultiply = ((DoubleOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Multiply value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value * _toMultiply;
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableMathMultiply(parameters));
    }

    protected double InheritableMathPower(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathpower.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathpower when the parameter is not a decimal.");
        }

        double _toPow = ((DoubleOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Power value was not null but the GetValue unexpectedly returned null.");

        return Math.Pow(NullableValue.Value, _toPow);
    }

    public override OperatorValue? MathPower(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableMathPower(parameters));
    }

    protected double InheritableMathRound(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathround.");
        }

        if(parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathround when the parameter is not an integer.");
        }

        int _digits = ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Round value was not null but the GetValue unexpectedly returned null.");

        return Math.Round(NullableValue.Value, _digits);
    }

    public override OperatorValue? MathRound(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableMathRound(parameters));
    }

    protected double InheritableMathSubtract(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathsubtract.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not a double.");
        }

        double _toSubtract = ((DoubleOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Subtract value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value - _toSubtract;
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? parameters)
    {
        return new DoubleOperator(InheritableMathSubtract(parameters));
    }

    protected bool InheritableNotEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if(parameters[0] is not DoubleOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not an integer.");
        }

        return NullableValue != ((DoubleOperator)parameters[0]).NullableValue;
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableNotEqual(parameters));
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
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

        return NullableValue.Value.ToString();
    }

    public override void SetValue(OperatorValue _source)
    {
        if(_source is not DoubleOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((DoubleOperator)_source).NullableValue;
    }
    
    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DoubleOperator, null or missing parameter.");
        }
        
        return BuildFromString(parameters[0]);
    }
    
    public static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new DoubleOperator(null);
        }

        if(double.TryParse(_input, out double _value))
        {
            return new DoubleOperator(_value);
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