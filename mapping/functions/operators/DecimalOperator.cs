
using System.Net.Http.Headers;

namespace luminary.mapping.functions;

public class DecimalOperator : OperatorValue
{
    protected decimal? NullableValue;

    public decimal? GetValue() => NullableValue;

    public DecimalOperator(decimal? nullableValue, OperatorValueType _type = OperatorValueType.Decimal) : base(_type)
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
        return new DecimalOperator(NullableValue);
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
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a decimal.");
        }

        return NullableValue.Value == ((DecimalOperator)parameters[0]).NullableValue;
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

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a decimal.");
        }

        decimal _check = ((DecimalOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterorequal.");

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

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a decimal.");
        }

        decimal _check = ((DecimalOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterthan.");

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
            return new DecimalOperator(NullableValue);
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

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a decimal.");
        }

        decimal _check = ((DecimalOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessorequal.");

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

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a decimal.");
        }

        decimal _check = ((DecimalOperator)parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessthan.");

        return NullableValue < _check;
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        return new BooleanOperator(InheritableLessThan(parameters));
    }

    protected decimal InheritableMathAdd(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a decimal.");
        }

        decimal _toAdd = ((DecimalOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value + _toAdd;
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        return new DecimalOperator(InheritableMathAdd(parameters));
    }

    protected decimal InheritableMathAverage(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaverage.");
        }

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not a decimal.");
        }

        decimal _toAverage = ((DecimalOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Average value was not null but the GetValue unexpectedly returned null.");

        return (NullableValue.Value + _toAverage) / 2m;
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
        return new DecimalOperator(InheritableMathAverage(parameters));
    }

    protected decimal InheritableMathCeiling(OperatorValue[]? parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute mathceiling.");
        }

        return Math.Ceiling(NullableValue.Value);
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? parameters)
    {
        return new DecimalOperator(InheritableMathCeiling(parameters));
    }

    protected decimal InheritableMathDivide(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathdivide.");
        }

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not a decimal.");
        }

        decimal _toDivide = ((DecimalOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Divide value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value / _toDivide;
    }

    public override OperatorValue? MathDivide(OperatorValue[]? parameters)
    {
        return new DecimalOperator(InheritableMathDivide(parameters));
    }

    protected decimal InheritableMathFloor(OperatorValue[]? parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute mathfloor.");
        }

        return Math.Floor(NullableValue.Value);
    }

    public override OperatorValue? MathFloor(OperatorValue[]? parameters)
    {
        return new DecimalOperator(InheritableMathFloor(parameters));
    }

    protected decimal InheritableMathMultiply(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathmultiply.");
        }

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not a decimal.");
        }

        decimal _toMultiply = ((DecimalOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Multiply value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value * _toMultiply;
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? parameters)
    {
        return new DecimalOperator(InheritableMathMultiply(parameters));
    }

    protected decimal InheritableMathPower(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathpower.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathpower when the parameter is not an integer.");
        }

        decimal _toPow = ((DecimalOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Power value was not null but the GetValue unexpectedly returned null.");

        // there are no native decimal power functions and
        //  we are not supporting numbers that precise, so
        //  we lose some precision by going to double.
        double _output = Math.Pow((double)NullableValue, (double)_toPow);

        return (decimal)_output;
    }

    public override OperatorValue? MathPower(OperatorValue[]? parameters)
    {
        return new DecimalOperator(InheritableMathPower(parameters));
    }

    protected decimal InheritableMathRound(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathround.");
        }

        if (parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot mathround when the parameter is not an integer.");
        }

        int _digits = ((IntegerOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Round value was not null but the GetValue unexpectedly returned null.");

        return Math.Round(NullableValue.Value, _digits);
    }

    public override OperatorValue? MathRound(OperatorValue[]? parameters)
    {
        return new DecimalOperator(InheritableMathRound(parameters));
    }

    protected decimal InheritableMathSubtract(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathsubtract.");
        }

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not a decimal.");
        }

        decimal _toSubtract = ((DecimalOperator)parameters[0]).GetValue() ?? throw new ArgumentException("Subtract value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value - _toSubtract;
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? parameters)
    {
        return new DecimalOperator(InheritableMathSubtract(parameters));
    }

    protected bool InheritableNotEqual(OperatorValue[]? parameters)
    {
        if (NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (parameters[0] is not DecimalOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a decimal.");
        }

        return NullableValue.Value == ((DecimalOperator)parameters[0]).NullableValue;
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
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute tostringvalue.");
        }

        return NullableValue.Value.ToString();
    }

    public override void SetValue(OperatorValue _source)
    {
        if (_source is not DecimalOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((DecimalOperator)_source).NullableValue;
    }

    public static OperatorValue BuildFromParameters(string[] parameters)
    {
        if (parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DecimalOperator, null or missing parameter.");
        }

        return BuildFromString(parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new DecimalOperator(null);
        }

        if (decimal.TryParse(_input, out decimal _value))
        {
            return new DecimalOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to decimal.",
                    _input
                )
            );
        }
    }
}