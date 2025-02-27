
using System.Numerics;
using luminary.mapping.functions;

namespace luminary.mapping;

public static class ConstantMethods
{
    public struct MethodNames
    {
        #region "build constant operator"
        public const string String = "string";
        public const string Integer = "integer";
        public const string BigInteger = "biginteger";
        public const string Double = "double";
        public const string Decimal = "decimal";
        public const string Date = "date";
        public const string Time = "time";
        public const string DateTimeOffset = "datetimeoffset";
        public const string Array = "array";
        public const string Duration = "duration";
        public const string DateTimeOffsetWithDuration = "datetimeoffsetwithduration";
        public const string IntegerUnits = "integerunits";
        public const string BigIntegerUnits = "bigintegerunits";
        public const string DoubleUnits = "doubleunits";
        public const string DecimalUnits = "decimalunits";
        public const string Boolean = "boolean";
        #endregion

        #region "custom methods"
        #endregion
    }

    public static Dictionary<string, Func<string[], OperatorValue>> MethodCall = new()
    {
        { MethodNames.String, StringOperator.BuildFromParameters },
        { MethodNames.Integer, IntegerOperator.BuildFromParameters },
        { MethodNames.BigInteger, BigIntegerOperator.BuildFromParameters },
        { MethodNames.Double, DoubleOperator.BuildFromParameters },
        { MethodNames.Decimal, DecimalOperator.BuildFromParameters },
        { MethodNames.Date, DateOperator.BuildFromParameters },
        { MethodNames.Time, TimeOperator.BuildFromParameters },
        { MethodNames.DateTimeOffset, DateTimeOffsetOperator.BuildFromParameters },
        { MethodNames.Array, ArrayOperator.BuildFromParameters },
        { MethodNames.Duration, DurationOperator.BuildFromParameters },
        { MethodNames.DateTimeOffsetWithDuration, DateTimeOffsetWithDurationOperator.BuildFromParameters },
        { MethodNames.IntegerUnits, IntegerUnitsOperator.BuildFromParameters },
        { MethodNames.BigIntegerUnits, BigIntegerUnitsOperator.BuildFromParameters },
        { MethodNames.DoubleUnits, DoubleUnitsOperator.BuildFromParameters },
        { MethodNames.DecimalUnits, DecimalUnitsOperator.BuildFromParameters },
        { MethodNames.Boolean, BooleanOperator.BuildFromParameters }
    };

}