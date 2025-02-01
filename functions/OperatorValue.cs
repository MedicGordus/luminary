using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

namespace luminary.functions;

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
        DateTimeZone = 8,
        Array = 9,
        Duration = 10,
        DateTimeZoneWithDuration = 11,
        IntegerUnits = 12,
        BigIntegerUnits = 13,
        DoubleUnits = 14,
        DecimalUnits = 15,
        Boolean = 16,
        Prism = 17
    }

    public readonly struct OperatorValueTypeFormat
    {
        public static readonly string String = "string";
        public static readonly string Integer = "integer";
        public static readonly string BigInteger = "biginteger";
        public static readonly string Double = "double";
        public static readonly string Decimal = "decimal";
        public static readonly string Date = "date";
        public static readonly string Time = "time";
        public static readonly string DateTimeZone = "date-time";
        public static readonly string Duration = "duration";
        public static readonly string DateTimeZoneWithDuration = "date-time-and-duration";
        public static readonly string IntegerUnits = "integer-and-units";
        public static readonly string BigIntegerUnits = "biginteger-and-units";
        public static readonly string DoubleUnits = "double-and-units";
        public static readonly string DecimalUnits = "decimal-and-units";

        // these are not strings so they don't need a format:
        //
        // public static readonly string Array = "array";
        // public static readonly string Boolean = "boolean";
        // public static readonly string Prism = "prism";
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
        { OperatorValueTypeFormat.DateTimeZone, OperatorValueType.DateTimeZone },
        { OperatorValueTypeFormat.Duration, OperatorValueType.Duration },
        { OperatorValueTypeFormat.DateTimeZoneWithDuration, OperatorValueType.DateTimeZoneWithDuration },
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
            MethodNames.Includes
        ];

    public readonly struct MethodNames
    {
        public static readonly string Filled = "filled";
        public static readonly string BooleanNot = "not";
        public static readonly string BooleanAnd = "and";
        public static readonly string BooleanOr = "or";
        public static readonly string ValueEqual = "equal";
        public static readonly string GreatherThan = "greaterthan";
        public static readonly string LessThan = "lessthan";
        public static readonly string NotEqual = "notequal";
        public static readonly string GreaterOrEqual = "greaterorequal";
        public static readonly string LessOrEqual = "lessorequal";
        public static readonly string IfNotFilled = "ifnotfilled";
        public static readonly string Trim = "trim";
        public static readonly string Substring = "substring";
        public static readonly string Length = "length";
        public static readonly string ToUpper = "toupper";
        public static readonly string ToLower = "tolower";
        public static readonly string Concatenate = "concatenate";
        public static readonly string Join = "join";
        public static readonly string Split = "split";
        public static readonly string EqualsIgnoreCase = "equalsignorecase";
        public static readonly string ConvertToString = "tostring";
        public static readonly string ConvertToInteger = "tointeger";
        public static readonly string ConvertToBigInteger = "tobiginteger";
        public static readonly string ConvertToDouble = "todouble";
        public static readonly string ConvertToDecimal = "todecimal";
        public static readonly string ConvertToIntegerUnits = "tointegerunits";
        public static readonly string ConvertToBigIntegerUnits = "tobigintegerunits";
        public static readonly string ConvertToDoubleUnits = "todoubleunits";
        public static readonly string ConvertToDecimalUnits = "todecimalunits";
        public static readonly string MathCeiling = "ceiling";
        public static readonly string MathFloor = "floor";
        public static readonly string MathRound = "round";
        public static readonly string MathAverage = "average";
        public static readonly string MathPower = "power";
        public static readonly string MathAdd = "add";
        public static readonly string MathSubtract = "subtract";
        public static readonly string MathMultiply = "multiply";
        public static readonly string MathDivide = "divide";
        public static readonly string BitwiseLeftShift = "leftshift";
        public static readonly string BitwiseRightShift = "rightshift";
        public static readonly string BitwiseXor = "xor";
        public static readonly string BitwiseMod = "mod";
        public static readonly string IndexOf = "indexof";
        public static readonly string Replace = "replace";
        public static readonly string StartsWith = "startswith";
        public static readonly string EndsWith = "endswith";
        public static readonly string Includes = "includes";
    }

    private Dictionary<string, Func<OperatorValue[]?, OperatorValue?>> StringMethods;

    public OperatorValueType Type;

    protected OperatorValue(OperatorValueType type)
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
        Type = type;
    }

    public OperatorValue? ExecuteMethod(string methodName, OperatorValue[]? parameters)
    {
        if (StringMethods.TryGetValue(methodName.ToLower(), out var _method))
        {
            return _method(parameters);
        }

        throw new ArgumentException("Method not found", nameof(methodName));
    }

    public abstract OperatorValue? Filled(OperatorValue[]? parameters);

    public abstract OperatorValue? BooleanNot(OperatorValue[]? parameters);

    public abstract OperatorValue? BooleanAnd(OperatorValue[]? parameters);

    public abstract OperatorValue? BooleanOr(OperatorValue[]? parameters);

    public abstract OperatorValue? ValueEqual(OperatorValue[]? parameters);

    public abstract OperatorValue? GreatherThan(OperatorValue[]? parameters);

    public abstract OperatorValue? LessThan(OperatorValue[]? parameters);

    public abstract OperatorValue? NotEqual(OperatorValue[]? parameters);

    public abstract OperatorValue? GreaterOrEqual(OperatorValue[]? parameters);

    public abstract OperatorValue? LessOrEqual(OperatorValue[]? parameters);

    public abstract OperatorValue? IfNotFilled(OperatorValue[]? parameters);

    public abstract OperatorValue? Trim(OperatorValue[]? parameters);

    public abstract OperatorValue? Substring(OperatorValue[]? parameters);

    public abstract OperatorValue? Length(OperatorValue[]? parameters);

    public abstract OperatorValue? ToUpper(OperatorValue[]? parameters);

    public abstract OperatorValue? ToLower(OperatorValue[]? parameters);

    public abstract OperatorValue? Concatenate(OperatorValue[]? parameters);

    public abstract OperatorValue? Join(OperatorValue[]? parameters);

    public abstract OperatorValue? Split(OperatorValue[]? parameters);

    public abstract OperatorValue? EqualsIgnoreCase(OperatorValue[]? parameters);

    public abstract OperatorValue? ConvertToString(OperatorValue[]? parameters);

    public abstract OperatorValue? ConvertToInteger(OperatorValue[]? parameters);

    public abstract OperatorValue? ConvertToBigInteger(OperatorValue[]? parameters);

    public abstract OperatorValue? ConvertToDouble(OperatorValue[]? parameters);

    public abstract OperatorValue? ConvertToDecimal(OperatorValue[]? parameters);

    public abstract OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters);

    public abstract OperatorValue? ConvertToBigIntegerUnits(OperatorValue[]? parameters);

    public abstract OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters);

    public abstract OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters);

    public abstract OperatorValue? MathCeiling(OperatorValue[]? parameters);

    public abstract OperatorValue? MathFloor(OperatorValue[]? parameters);

    public abstract OperatorValue? MathRound(OperatorValue[]? parameters);

    public abstract OperatorValue? MathAverage(OperatorValue[]? parameters);

    public abstract OperatorValue? MathPower(OperatorValue[]? parameters);

    public abstract OperatorValue? MathAdd(OperatorValue[]? parameters);

    public abstract OperatorValue? MathSubtract(OperatorValue[]? parameters);

    public abstract OperatorValue? MathMultiply(OperatorValue[]? parameters);

    public abstract OperatorValue? MathDivide(OperatorValue[]? parameters);

    public abstract OperatorValue? BitwiseLeftShift(OperatorValue[]? parameters);

    public abstract OperatorValue? BitwiseRightShift(OperatorValue[]? parameters);

    public abstract OperatorValue? BitwiseXor(OperatorValue[]? parameters);

    public abstract OperatorValue? BitwiseMod(OperatorValue[]? parameters);

    public abstract OperatorValue? IndexOf(OperatorValue[]? parameters);

    public abstract OperatorValue? Replace(OperatorValue[]? parameters);

    public abstract OperatorValue? StartsWith(OperatorValue[]? parameters);

    public abstract OperatorValue? EndsWith(OperatorValue[]? parameters);

    public abstract OperatorValue? Includes(OperatorValue[]? parameters);

    public abstract string ToStringValue();
    public abstract string ToJsonStringValue();
    public abstract void SetValue(OperatorValue value);


    public static OperatorValue? CreateByType(OperatorValueType type)
    {
        return type switch {
            OperatorValueType.String => new StringOperator(default),
            OperatorValueType.Integer => new IntegerOperator(default),
            OperatorValueType.BigInteger => new BigIntegerOperator(default),
            OperatorValueType.Double => new DoubleOperator(default),
            OperatorValueType.Decimal => new DecimalOperator(default),
            OperatorValueType.Date => new DateOperator(default),
            OperatorValueType.Time => new TimeOperator(default),
            OperatorValueType.DateTimeZone => new DateTimeZoneOperator(default),
            OperatorValueType.Array => new ArrayOperator([]),
            OperatorValueType.Duration => new DurationOperator(default),
            OperatorValueType.DateTimeZoneWithDuration => new DateTimeZoneWithDurationOperator(default, default),
            OperatorValueType.IntegerUnits => new IntegerUnitsOperator(default, ""),
            OperatorValueType.BigIntegerUnits => new BigIntegerUnitsOperator(default, ""),
            OperatorValueType.DoubleUnits => new DoubleUnitsOperator(default, ""),
            OperatorValueType.DecimalUnits => new DecimalUnitsOperator(default, ""),
            OperatorValueType.Boolean => new BooleanOperator(default),
            OperatorValueType.Prism => new PrismOperator([]),
            _ => null
        };
    }
}