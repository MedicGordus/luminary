
using System.Numerics;

namespace luminary.mapping.functions;

public class BigIntegerOperator : OperatorValue
{
    protected BigInteger? NullableValue;

    public BigInteger? GetValue() => NullableValue;

    public BigIntegerOperator(BigInteger? _nullableValue, OperatorValueType _type = OperatorValueType.BigInteger) : base(_type)
    {
        NullableValue = _nullableValue;
    }

    protected BigInteger InheritableBooleanAnd(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanand.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot booleanand when the parameter is not a biginteger.");
        }

        (var left, var right) = GetArraysForDeepCalculations(
            NullableValue.Value,
            ((BigIntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute booleanand.")
        );

        return AndArrays(left, right);
    }

    public override OperatorValue? BooleanAnd(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableBooleanAnd(_parameters));
    }

    protected BigInteger InheritableBitwiseLeftShift(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwiseleftshift.");
        }

        if (_parameters[0] is not IntegerOperator)
        {
            throw new ArgumentException("Cannot bitwiseleftshift when the parameter is not an integer.");
        }

        return NullableValue << ((IntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Parameter not null but GetValue() unexpectedly returned null. Cannot execute bitwiseleftshift"); ;
    }

    public override OperatorValue? BitwiseLeftShift(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableBitwiseLeftShift(_parameters));
    }

    protected BigInteger InheritableBitwiseMod(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisemod.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot bitwisemod when the parameter is not a biginteger.");
        }

        BigInteger toMod = ((BigIntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Parameter was not null but the GetValue unexpectedly returned null.");

        return BigInteger.ModPow(NullableValue.Value, BigInteger.One, toMod);
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableBitwiseMod(_parameters));
    }

    protected BigInteger InheritableBitwiseRightShift(OperatorValue[]? _parameters)
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
        return new BigIntegerOperator(InheritableBitwiseRightShift(_parameters));
    }

    protected BigInteger InheritableBitwiseXor(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisexor.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot rightshift when the parameter is not a biginteger.");
        }

        (var left, var right) = GetArraysForDeepCalculations(
            NullableValue.Value,
            ((BigIntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute bitwisexor.")
        );

        return XorArrays(left, right);
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableBitwiseXor(_parameters));
    }

    public override OperatorValue? Concatenate(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(NullableValue);
    }

    public override OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute converttobigintegerunits.");
        }

        if (_parameters[0] is not StringOperator)
        {
            throw new ArgumentException("Cannot converttobigintegerunits when the unit parameter is not a string.");
        }

        return new BigIntegerUnitsOperator(NullableValue, ((StringOperator)_parameters[0]).GetValue());
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

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a biginteger.");
        }

        return NullableValue.Value == ((BigIntegerOperator)_parameters[0]).NullableValue;
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

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a biginteger.");
        }

        BigInteger check = ((BigIntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterorequal.");

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

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a biginteger.");
        }

        BigInteger check = ((BigIntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute greaterthan.");

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
            return new BigIntegerOperator(NullableValue);
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

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a biginteger.");
        }

        BigInteger check = ((BigIntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessorequal.");

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

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a biginteger.");
        }

        BigInteger check = ((BigIntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute lessthan.");

        return NullableValue < check;
    }

    public override OperatorValue? LessThan(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableLessThan(_parameters));
    }

    protected BigInteger InheritableMathAdd(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a biginteger.");
        }

        BigInteger toAdd = ((BigIntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Add value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value + toAdd;
    }

    public override OperatorValue? MathAdd(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableMathAdd(_parameters));
    }

    protected BigInteger InheritableMathAverage(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaverage.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not a biginteger.");
        }

        BigInteger toAverage = ((BigIntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Average value was not null but the GetValue unexpectedly returned null.");

        return (NullableValue.Value + toAverage) >> 1;
    }

    public override OperatorValue? MathAverage(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableMathAverage(_parameters));
    }

    public override OperatorValue? MathCeiling(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    protected BigInteger InheritableMathDivide(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathdivide.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not a biginteger.");
        }

        BigInteger toDivide = ((BigIntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Divide value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value / toDivide;
    }

    public override OperatorValue? MathDivide(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableMathDivide(_parameters));
    }

    public override OperatorValue? MathFloor(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    protected BigInteger InheritableMathMultiply(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathmultiply.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not a biginteger.");
        }

        BigInteger toMultiply = ((BigIntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Multiply value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value * toMultiply;
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableMathMultiply(_parameters));
    }

    protected BigInteger InheritableMathPower(OperatorValue[]? _parameters)
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

        return BigInteger.Pow(NullableValue.Value, toPow);
    }

    public override OperatorValue? MathPower(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableMathPower(_parameters));
    }

    public override OperatorValue? MathRound(OperatorValue[]? _parameters)
    {
        throw new InvalidOperationException();
    }

    protected BigInteger InheritableMathSubtract(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathsubtract.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not a biginteger.");
        }

        BigInteger toSubtract = ((BigIntegerOperator)_parameters[0]).GetValue() ?? throw new ArgumentException("Subtract value was not null but the GetValue unexpectedly returned null.");

        return NullableValue.Value - toSubtract;
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableMathSubtract(_parameters));
    }

    protected bool InheritableNotEqual(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a biginteger.");
        }

        return NullableValue.Value != ((BigIntegerOperator)_parameters[0]).NullableValue;
    }

    public override OperatorValue? NotEqual(OperatorValue[]? _parameters)
    {
        return new BooleanOperator(InheritableNotEqual(_parameters));
    }

    protected BigInteger InheritableBooleanOr(OperatorValue[]? _parameters)
    {
        if (NullableValue == null || _parameters == null || _parameters.Length == 0 || _parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanor.");
        }

        if (_parameters[0] is not BigIntegerOperator)
        {
            throw new ArgumentException("Cannot booleanor when the parameter is not a biginteger.");
        }

        (var left, var right) = GetArraysForDeepCalculations(
            NullableValue.Value,
            ((BigIntegerOperator)_parameters[0]).NullableValue ?? throw new ArgumentException("Parameter null. Cannot execute booleanor.")
        );

        return OrArrays(left, right);
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableBooleanOr(_parameters));
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

    protected BigInteger InheritableBooleanNot(OperatorValue[]? _parameters)
    {
        if (NullableValue == null)
        {
            throw new ArgumentException("Value null. Cannot execute booleannot.");
        }

        byte[] bytes = NullableValue.Value.ToByteArray();

        return new BigInteger(
            bytes.Select(_deltaByte => (byte)~_deltaByte).ToArray()
        );
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? _parameters)
    {
        return new BigIntegerOperator(InheritableBooleanNot(_parameters));
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
        if (_source is not BigIntegerOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((BigIntegerOperator)_source).NullableValue;
    }

    public static OperatorValue BuildFromParameters(string[] _parameters)
    {
        if (_parameters == null || _parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a BigIntegerOperator, null or missing parameter.");
        }

        return BuildFromString(_parameters[0]);
    }

    public static OperatorValue BuildFromString(string? _input)
    {
        if (_input == null)
        {
            return new BigIntegerOperator(null);
        }

        if (BigInteger.TryParse(_input, out BigInteger value))
        {
            return new BigIntegerOperator(value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to BigInteger.",
                    _input
                )
            );
        }
    }

    protected static (byte[], byte[]) GetArraysForDeepCalculations(BigInteger _left, BigInteger _right)
    {
        return (_left.ToByteArray(), _right.ToByteArray());
    }

    /// <summary>
    /// Performs an xor on two arrays, after resizing them.
    /// 
    /// WARNING: No idea if the conversions to/from big integer are little or big endian - UNTESTED.
    /// </summary>
    /// <param name="_left">The left array to xor.</param>
    /// <param name="_right">The right array to xor.</param>
    /// <returns>A new BigInteger created from the two input arrays xor'd.</returns>
    protected static BigInteger XorArrays(byte[] _left, byte[] _right)
    {
        int maxLength = Math.Max(_left.Length, _right.Length);
        Array.Resize(ref _left, maxLength);
        Array.Resize(ref _right, maxLength);

        // Perform XOR on each byte
        byte[] resultBytes = new byte[maxLength];
        for (int i = 0; i < maxLength; i++)
        {
            resultBytes[i] = (byte)(_left[i] ^ _right[i]);
        }

        // Convert back to BigInteger
        return new BigInteger(resultBytes);
    }

    /// <summary>
    /// Performs an or on two arrays, after resizing them.
    /// 
    /// WARNING: No idea if the conversions to/from big integer are little or big endian - UNTESTED.
    /// </summary>
    /// <param name="_left">The left array to or.</param>
    /// <param name="_right">The right array to or.</param>
    /// <returns>A new BigInteger created from the two input arrays or'd.</returns>
    protected static BigInteger OrArrays(byte[] _left, byte[] _right)
    {
        int maxLength = Math.Max(_left.Length, _right.Length);
        Array.Resize(ref _left, maxLength);
        Array.Resize(ref _right, maxLength);

        // Perform XOR on each byte
        byte[] resultBytes = new byte[maxLength];
        for (int i = 0; i < maxLength; i++)
        {
            resultBytes[i] = (byte)(_left[i] | _right[i]);
        }

        // Convert back to BigInteger
        return new BigInteger(resultBytes);
    }

    /// <summary>
    /// Performs an and on two arrays, after resizing them.
    /// 
    /// WARNING: No idea if the conversions to/from big integer are little or big endian - UNTESTED.
    /// </summary>
    /// <param name="_left">The left array to and.</param>
    /// <param name="_right">The right array to and.</param>
    /// <returns>A new BigInteger created from the two input arrays and'd.</returns>
    protected static BigInteger AndArrays(byte[] _left, byte[] _right)
    {
        int maxLength = Math.Max(_left.Length, _right.Length);
        Array.Resize(ref _left, maxLength);
        Array.Resize(ref _right, maxLength);

        // Perform XOR on each byte
        byte[] resultBytes = new byte[maxLength];
        for (int i = 0; i < maxLength; i++)
        {
            resultBytes[i] = (byte)(_left[i] & _right[i]);
        }

        // Convert back to BigInteger
        return new BigInteger(resultBytes);
    }
}