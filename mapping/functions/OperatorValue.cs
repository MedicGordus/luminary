using luminary.flow;


namespace luminary.mapping.functions;

public abstract class OperatorValue
{
    public enum OperatorValueType : int
    {
        z_error = 0,
        String = 1,
        Integer = 2,
        BigInteger = 3,
        Double = 4,
        Decimal = 5,
        Date = 6,
        Time = 7,
        DateTimeOffset = 8,
        Array = 9,
        Duration = 10,

        /// <summary>
        /// This is a date and duration, separated by |
        /// </summary>
        DateTimeOffsetWithDuration = 11,
        IntegerUnits = 12,
        BigIntegerUnits = 13,
        DoubleUnits = 14,
        DecimalUnits = 15,
        Boolean = 16,
        Prism = 17
    }

    public readonly struct OperatorValueTypeFormat
    {
        public const string String = "string";
        public const string Integer = "integer";
        public const string BigInteger = "biginteger";
        public const string Double = "double";
        public const string Decimal = "decimal";
        public const string Date = "date";
        public const string Time = "time";
        public const string DateTimeOffset = "date-time";
        public const string Duration = "duration";
        public const string DateTimeOffsetWithDuration = "date-time-and-duration";
        public const string IntegerUnits = "integer-and-units";
        public const string BigIntegerUnits = "biginteger-and-units";
        public const string DoubleUnits = "double-and-units";
        public const string DecimalUnits = "decimal-and-units";

        // these are not strings so they don't need a format:
        //
        // public const string Array = "array";
        // public const string Boolean = "boolean";
        // public const string Prism = "prism";
    }

    public static readonly Dictionary<string, OperatorValueType> OperatorValueTypeLookup = new()
    {
        { OperatorValueTypeFormat.String, OperatorValueType.String },
        { OperatorValueTypeFormat.Integer, OperatorValueType.Integer },
        { OperatorValueTypeFormat.BigInteger, OperatorValueType.BigInteger },
        { OperatorValueTypeFormat.Double, OperatorValueType.Double },
        { OperatorValueTypeFormat.Decimal, OperatorValueType.Decimal },
        { OperatorValueTypeFormat.Date, OperatorValueType.Date },
        { OperatorValueTypeFormat.Time, OperatorValueType.Time },
        { OperatorValueTypeFormat.DateTimeOffset, OperatorValueType.DateTimeOffset },
        { OperatorValueTypeFormat.Duration, OperatorValueType.Duration },
        { OperatorValueTypeFormat.DateTimeOffsetWithDuration, OperatorValueType.DateTimeOffsetWithDuration },
        { OperatorValueTypeFormat.IntegerUnits, OperatorValueType.IntegerUnits },
        { OperatorValueTypeFormat.BigIntegerUnits, OperatorValueType.BigIntegerUnits },
        { OperatorValueTypeFormat.DoubleUnits, OperatorValueType.DoubleUnits },
        { OperatorValueTypeFormat.DecimalUnits, OperatorValueType.DecimalUnits },

        
        // these are not strings so they don't need a format:
        //
        //{ OperatorValueTypeFormat.Array, OperatorValueType.Array },
        //{ OperatorValueTypeFormat.Boolean, OperatorValueType.Boolean },
        //{ OperatorValueTypeFormat.Prism, OperatorValueType.Prism }
    };

    public static readonly List<string> MethodNameList =
    [
        // these are functions
        MethodNames.Filled,
        MethodNames.BooleanNot,
        MethodNames.BooleanAnd,
        MethodNames.BooleanOr,
        MethodNames.ValueEqual,
        MethodNames.GreatherThan,
        MethodNames.LessThan,
        MethodNames.NotEqual,
        MethodNames.GreaterOrEqual,
        MethodNames.LessOrEqual,
        MethodNames.IfNotFilled,
        MethodNames.Trim,
        MethodNames.Substring,
        MethodNames.Length,
        MethodNames.ToUpper,
        MethodNames.ToLower,
        MethodNames.Concatenate,
        MethodNames.Join,
        MethodNames.Split,
        MethodNames.EqualsIgnoreCase,
        MethodNames.ConvertToString,
        MethodNames.ConvertToInteger,
        MethodNames.ConvertToBigInteger,
        MethodNames.ConvertToDouble,
        MethodNames.ConvertToDecimal,
        MethodNames.ConvertToIntegerUnits,
        MethodNames.ConvertToBigIntegerUnits,
        MethodNames.ConvertToDoubleUnits,
        MethodNames.ConvertToDecimalUnits,
        MethodNames.MathCeiling,
        MethodNames.MathFloor,
        MethodNames.MathRound,
        MethodNames.MathAverage,
        MethodNames.MathPower,
        MethodNames.MathAdd,
        MethodNames.MathSubtract,
        MethodNames.MathMultiply,
        MethodNames.MathDivide,
        MethodNames.BitwiseLeftShift,
        MethodNames.BitwiseRightShift,
        MethodNames.BitwiseXor,
        MethodNames.BitwiseMod,
        MethodNames.IndexOf,
        MethodNames.Replace,
        MethodNames.StartsWith,
        MethodNames.EndsWith,
        MethodNames.Includes,


        // these are flows
        FlowType.GOTO,
        FlowType.IF,
        FlowType.WHILE,
        FlowType.FOR,
        FlowType.FOR_EACH,
    ];

    public static readonly List<string> FlowNameList =
    [
        FlowType.GOTO,
        FlowType.IF,
        FlowType.WHILE,
        FlowType.FOR,
        FlowType.FOR_EACH
    ];

    public readonly struct MethodNames
    {
        public const string Filled = "filled";
        public const string BooleanNot = "not";
        public const string BooleanAnd = "and";
        public const string BooleanOr = "or";
        public const string ValueEqual = "equal";
        public const string GreatherThan = "greaterthan";
        public const string LessThan = "lessthan";
        public const string NotEqual = "notequal";
        public const string GreaterOrEqual = "greaterorequal";
        public const string LessOrEqual = "lessorequal";
        public const string IfNotFilled = "ifnotfilled";
        public const string Trim = "trim";
        public const string Substring = "substring";
        public const string Length = "length";
        public const string ToUpper = "toupper";
        public const string ToLower = "tolower";
        public const string Concatenate = "concatenate";
        public const string Join = "join";
        public const string Split = "split";
        public const string EqualsIgnoreCase = "equalsignorecase";
        public const string ConvertToString = "tostring";
        public const string ConvertToInteger = "tointeger";
        public const string ConvertToBigInteger = "tobiginteger";
        public const string ConvertToDouble = "todouble";
        public const string ConvertToDecimal = "todecimal";
        public const string ConvertToIntegerUnits = "tointegerunits";
        public const string ConvertToBigIntegerUnits = "tobigintegerunits";
        public const string ConvertToDoubleUnits = "todoubleunits";
        public const string ConvertToDecimalUnits = "todecimalunits";
        public const string MathCeiling = "ceiling";
        public const string MathFloor = "floor";
        public const string MathRound = "round";
        public const string MathAverage = "average";
        public const string MathPower = "power";
        public const string MathAdd = "add";
        public const string MathSubtract = "subtract";
        public const string MathMultiply = "multiply";
        public const string MathDivide = "divide";
        public const string BitwiseLeftShift = "leftshift";
        public const string BitwiseRightShift = "rightshift";
        public const string BitwiseXor = "xor";
        public const string BitwiseMod = "mod";
        public const string IndexOf = "indexof";
        public const string Replace = "replace";
        public const string StartsWith = "startswith";
        public const string EndsWith = "endswith";
        public const string Includes = "includes";
    }


    private Dictionary<string, Func<OperatorValue[]?, OperatorValue?>> StringMethods;

    public OperatorValueType Type;

    protected OperatorValue(OperatorValueType _type)
    {
        StringMethods = new()
        {
            { MethodNames.Filled, Filled },
            { MethodNames.BooleanNot, BooleanNot },
            { MethodNames.BooleanAnd, BooleanAnd },
            { MethodNames.BooleanOr, BooleanOr },
            { MethodNames.ValueEqual, ValueEqual },
            { MethodNames.GreatherThan, GreatherThan },
            { MethodNames.LessThan, LessThan },
            { MethodNames.NotEqual, NotEqual },
            { MethodNames.GreaterOrEqual, GreaterOrEqual },
            { MethodNames.LessOrEqual, LessOrEqual },
            { MethodNames.IfNotFilled, IfNotFilled },
            { MethodNames.Trim, Trim },
            { MethodNames.Substring, Substring },
            { MethodNames.Length, Length },
            { MethodNames.ToUpper, ToUpper },
            { MethodNames.ToLower, ToLower },
            { MethodNames.Concatenate, Concatenate },
            { MethodNames.Join, Join },
            { MethodNames.Split, Split },
            { MethodNames.EqualsIgnoreCase, EqualsIgnoreCase },
            { MethodNames.ConvertToString, ConvertToString },
            { MethodNames.ConvertToInteger, ConvertToInteger },
            { MethodNames.ConvertToBigInteger, ConvertToBigInteger },
            { MethodNames.ConvertToDouble, ConvertToDouble },
            { MethodNames.ConvertToDecimal, ConvertToDecimal },
            { MethodNames.ConvertToIntegerUnits, ConvertToIntegerUnits },
            { MethodNames.ConvertToBigIntegerUnits, ConvertToBigIntegerUnits },
            { MethodNames.ConvertToDoubleUnits, ConvertToDoubleUnits },
            { MethodNames.ConvertToDecimalUnits, ConvertToDecimalUnits },
            { MethodNames.MathCeiling, MathCeiling },
            { MethodNames.MathFloor, MathFloor },
            { MethodNames.MathRound, MathRound },
            { MethodNames.MathAverage, MathAverage },
            { MethodNames.MathPower, MathPower },
            { MethodNames.MathAdd, MathAdd },
            { MethodNames.MathSubtract, MathSubtract },
            { MethodNames.MathMultiply, MathMultiply },
            { MethodNames.MathDivide, MathDivide },
            { MethodNames.BitwiseLeftShift, BitwiseLeftShift },
            { MethodNames.BitwiseRightShift, BitwiseRightShift },
            { MethodNames.BitwiseXor, BitwiseXor },
            { MethodNames.BitwiseMod, BitwiseMod },
            { MethodNames.IndexOf, IndexOf },
            { MethodNames.Replace, Replace },
            { MethodNames.StartsWith, StartsWith },
            { MethodNames.EndsWith, EndsWith },
            { MethodNames.Includes, Includes }
        };
        Type = _type;
    }

    public OperatorValue? ExecuteMethod(string _methodName, OperatorValue[]? _parameters)
    {
        if (StringMethods.TryGetValue(_methodName.ToLower(), out var method))
        {
            return method(_parameters);
        }

        throw new ArgumentException("Method not found", nameof(_methodName));
    }

    public abstract OperatorValue? Filled(OperatorValue[]? _parameters);

    public abstract OperatorValue? BooleanNot(OperatorValue[]? _parameters);

    public abstract OperatorValue? BooleanAnd(OperatorValue[]? _parameters);

    public abstract OperatorValue? BooleanOr(OperatorValue[]? _parameters);

    public abstract OperatorValue? ValueEqual(OperatorValue[]? _parameters);

    public abstract OperatorValue? GreatherThan(OperatorValue[]? _parameters);

    public abstract OperatorValue? LessThan(OperatorValue[]? _parameters);

    public abstract OperatorValue? NotEqual(OperatorValue[]? _parameters);

    public abstract OperatorValue? GreaterOrEqual(OperatorValue[]? _parameters);

    public abstract OperatorValue? LessOrEqual(OperatorValue[]? _parameters);

    public abstract OperatorValue? IfNotFilled(OperatorValue[]? _parameters);

    public abstract OperatorValue? Trim(OperatorValue[]? _parameters);

    public abstract OperatorValue? Substring(OperatorValue[]? _parameters);

    public abstract OperatorValue? Length(OperatorValue[]? _parameters);

    public abstract OperatorValue? ToUpper(OperatorValue[]? _parameters);

    public abstract OperatorValue? ToLower(OperatorValue[]? _parameters);

    public abstract OperatorValue? Concatenate(OperatorValue[]? _parameters);

    public abstract OperatorValue? Join(OperatorValue[]? _parameters);

    public abstract OperatorValue? Split(OperatorValue[]? _parameters);

    public abstract OperatorValue? EqualsIgnoreCase(OperatorValue[]? _parameters);

    public abstract OperatorValue? ConvertToString(OperatorValue[]? _parameters);

    public abstract OperatorValue? ConvertToInteger(OperatorValue[]? _parameters);

    public abstract OperatorValue? ConvertToBigInteger(OperatorValue[]? _parameters);

    public abstract OperatorValue? ConvertToDouble(OperatorValue[]? _parameters);

    public abstract OperatorValue? ConvertToDecimal(OperatorValue[]? _parameters);

    public abstract OperatorValue? ConvertToIntegerUnits(OperatorValue[]? _parameters);

    public abstract OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? _parameters);

    public abstract OperatorValue? ConvertToDoubleUnits(OperatorValue[]? _parameters);

    public abstract OperatorValue? ConvertToDecimalUnits(OperatorValue[]? _parameters);

    public abstract OperatorValue? MathCeiling(OperatorValue[]? _parameters);

    public abstract OperatorValue? MathFloor(OperatorValue[]? _parameters);

    public abstract OperatorValue? MathRound(OperatorValue[]? _parameters);

    public abstract OperatorValue? MathAverage(OperatorValue[]? _parameters);

    public abstract OperatorValue? MathPower(OperatorValue[]? _parameters);

    public abstract OperatorValue? MathAdd(OperatorValue[]? _parameters);

    public abstract OperatorValue? MathSubtract(OperatorValue[]? _parameters);

    public abstract OperatorValue? MathMultiply(OperatorValue[]? _parameters);

    public abstract OperatorValue? MathDivide(OperatorValue[]? _parameters);

    public abstract OperatorValue? BitwiseLeftShift(OperatorValue[]? _parameters);

    public abstract OperatorValue? BitwiseRightShift(OperatorValue[]? _parameters);

    public abstract OperatorValue? BitwiseXor(OperatorValue[]? _parameters);

    public abstract OperatorValue? BitwiseMod(OperatorValue[]? _parameters);

    public abstract OperatorValue? IndexOf(OperatorValue[]? _parameters);

    public abstract OperatorValue? Replace(OperatorValue[]? _parameters);

    public abstract OperatorValue? StartsWith(OperatorValue[]? _parameters);

    public abstract OperatorValue? EndsWith(OperatorValue[]? _parameters);

    public abstract OperatorValue? Includes(OperatorValue[]? _parameters);

    public abstract string ToStringValue();
    public abstract string ToJsonStringValue();
    public abstract void SetValue(OperatorValue _source);

    /// <summary>
    /// Caller must handle array and prism uniquely and not expect this to handle those types.
    /// </summary>
    public static OperatorValue? CreateByType(OperatorValueType _type)
    {
        return _type switch
        {
            OperatorValueType.String => new StringOperator(default),
            OperatorValueType.Integer => new IntegerOperator(default),
            OperatorValueType.BigInteger => new BigIntegerOperator(default),
            OperatorValueType.Double => new DoubleOperator(default),
            OperatorValueType.Decimal => new DecimalOperator(default),
            OperatorValueType.Date => new DateOperator(default),
            OperatorValueType.Time => new TimeOperator(default),
            OperatorValueType.DateTimeOffset => new DateTimeOffsetOperator(default),
            OperatorValueType.Duration => new DurationOperator(default),
            OperatorValueType.DateTimeOffsetWithDuration => new DateTimeOffsetWithDurationOperator(default, default),
            OperatorValueType.IntegerUnits => new IntegerUnitsOperator(default, ""),
            OperatorValueType.BigIntegerUnits => new BigIntegerUnitsOperator(default, ""),
            OperatorValueType.DoubleUnits => new DoubleUnitsOperator(default, ""),
            OperatorValueType.DecimalUnits => new DecimalUnitsOperator(default, ""),
            OperatorValueType.Boolean => new BooleanOperator(default),

            // these two shouldn't be called - the caller must handle them appropriately (see helper.cs ~line34 or so)
            //OperatorValueType.Array => new ArrayOperator(OperatorValueType.z_error, []),
            //OperatorValueType.Prism => new PrismOperator([]),
            //

            _ => null
        };
    }
}