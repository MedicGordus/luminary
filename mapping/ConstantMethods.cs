
using System.Numerics;
using luminary.mapping.functions;

namespace luminary.mapping;

public static class ConstantMethods
{
    public struct MethodNames
    {
        #region "build constant operator"
        public const string STRING = "string";
        public const string INTEGER = "integer";
        public const string BIG_INTEGER = "biginteger";
        public const string DOUBLE = "double";
        public const string DECIMAL = "decimal";
        public const string DATE = "date";
        public const string TIME = "time";
        public const string DATE_TIME_OFFSET = "datetimeoffset";
        public const string ARRAY = "array";
        public const string DURATION = "duration";
        public const string DATE_TIME_OFFSET_WITH_DURATION = "datetimeoffsetwithduration";
        public const string INTEGER_UNITS = "integerunits";
        public const string BIG_INTEGER_UNITS = "bigintegerunits";
        public const string DOUBLE_UNITS = "doubleunits";
        public const string DECIMAL_UNITS = "decimalunits";
        public const string BOOLEAN = "boolean";
        #endregion

        #region "custom methods"
        #endregion
    }

    public static Dictionary<string, Func<string[], OperatorValue>> MethodCall = new()
    {
        { MethodNames.STRING, StringOperator.BuildFromParameters },
        { MethodNames.INTEGER, IntegerOperator.BuildFromParameters },
        { MethodNames.BIG_INTEGER, BigIntegerOperator.BuildFromParameters },
        { MethodNames.DOUBLE, DoubleOperator.BuildFromParameters },
        { MethodNames.DECIMAL, DecimalOperator.BuildFromParameters },
        { MethodNames.DATE, DateOperator.BuildFromParameters },
        { MethodNames.TIME, TimeOperator.BuildFromParameters },
        { MethodNames.DATE_TIME_OFFSET, DateTimeOffsetOperator.BuildFromParameters },
        { MethodNames.ARRAY, ArrayOperator.BuildFromParameters },
        { MethodNames.DURATION, DurationOperator.BuildFromParameters },
        { MethodNames.DATE_TIME_OFFSET_WITH_DURATION, DateTimeOffsetWithDurationOperator.BuildFromParameters },
        { MethodNames.INTEGER_UNITS, IntegerUnitsOperator.BuildFromParameters },
        { MethodNames.BIG_INTEGER_UNITS, BigIntegerUnitsOperator.BuildFromParameters },
        { MethodNames.DOUBLE_UNITS, DoubleUnitsOperator.BuildFromParameters },
        { MethodNames.DECIMAL_UNITS, DecimalUnitsOperator.BuildFromParameters },
        { MethodNames.BOOLEAN, BooleanOperator.BuildFromParameters }
    };

}