
using System.Numerics;
using luminary.functions;

namespace luminary.mapping;

public static class ConstantMethods
{
    public struct MethodNames
    {
        public static string String = "string";
        public static string Integer = "integer";
        public static string BigInteger = "biginteger";
        public static string Double = "double";
        public static string Decimal = "decimal";
        public static string Date = "date";
        public static string Time = "time";
        public static string DateTimeZone = "datetimezone";
        public static string Array = "array";
        public static string Duration = "duration";
        public static string DateTimeZoneWithDuration = "datetimezonewithduration";
        public static string IntegerUnits = "integerunits";
        public static string BigIntegerUnits = "bigintegerunits";
        public static string DoubleUnits = "doubleunits";
        public static string DecimalUnits = "decimalunits";
        public static string Boolean = "boolean";
    }

    public static Dictionary<string, Func<string[], OperatorValue>> MethodCall = new()
    {
        { MethodNames.String, ConstantMethods.StringConstant },
        { MethodNames.Integer, ConstantMethods.IntegerConstant },
        { MethodNames.BigInteger, ConstantMethods.BigIntegerConstant },
        { MethodNames.Double, ConstantMethods.DoubleConstant },
        { MethodNames.Decimal, ConstantMethods.DecimalConstant },
        { MethodNames.Date, ConstantMethods.DateConstant },
        { MethodNames.Time, ConstantMethods.TimeConstant },
        { MethodNames.DateTimeZone, ConstantMethods.DateTimeZoneConstant },
        { MethodNames.Array, ConstantMethods.ArrayConstant },
        { MethodNames.Duration, ConstantMethods.DurationConstant },
        { MethodNames.DateTimeZoneWithDuration, ConstantMethods.DateTimeZoneWithDurationConstant },
        { MethodNames.IntegerUnits, ConstantMethods.IntegerUnitsConstant },
        { MethodNames.BigIntegerUnits, ConstantMethods.BigIntegerUnitsConstant },
        { MethodNames.DoubleUnits, ConstantMethods.DoubleUnitsConstant },
        { MethodNames.DecimalUnits, ConstantMethods.DecimalUnitsConstant },
        { MethodNames.Boolean, ConstantMethods.BooleanConstant }
    };

    public static OperatorValue StringConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a StringConstant, null or missing parameter.");
        }

        return new StringOperator(parameters[0]);
    }

    public static OperatorValue IntegerConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create an IntegerConstant, null or missing parameter.");
        }
        
        if(int.TryParse(parameters[0], out int _value))
        {
            return new IntegerOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to int.",
                    parameters[0]
                )
            );
        }
    }
    
    public static OperatorValue BigIntegerConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a BigIntegerConstant, null or missing parameter.");
        }
        
        if(BigInteger.TryParse(parameters[0], out BigInteger _value))
        {
            return new BigIntegerOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to BigInteger.",
                    parameters[0]
                )
            );
        }
    }
    
    public static OperatorValue DoubleConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DoubleConstant, null or missing parameter.");
        }
        
        if(double.TryParse(parameters[0], out double _value))
        {
            return new DoubleOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to double.",
                    parameters[0]
                )
            );
        }
    }
    
    public static OperatorValue DecimalConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DecimalConstant, null or missing parameter.");
        }
        
        if(decimal.TryParse(parameters[0], out decimal _value))
        {
            return new DecimalOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to decimal.",
                    parameters[0]
                )
            );
        }
    }
    
    public static OperatorValue DateConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DateConstant, null or missing parameter.");
        }
        
        if(DateOnly.TryParse(parameters[0], out DateOnly _value))
        {
            return new DateOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to DateOnly.",
                    parameters[0]
                )
            );
        }
    }
    
    public static OperatorValue TimeConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a TimeConstant, null or missing parameter.");
        }
        
        if(TimeOnly.TryParse(parameters[0], out TimeOnly _value))
        {
            return new TimeOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to TimeOnly.",
                    parameters[0]
                )
            );
        }
    }
    
    public static OperatorValue DateTimeZoneConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DateTimeZoneConstant, null or missing parameter.");
        }
        
        if(DateTimeOffset.TryParse(parameters[0], out DateTimeOffset _value))
        {
            return new DateTimeZoneOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to DateTimeZoneOperator.",
                    parameters[0]
                )
            );
        }
    }
    
    public static OperatorValue ArrayConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a ArrayConstant, null or missing parameter.");
        }
        
        if(Array.TryParse(parameters[0], out Array _value))
        {
            return new ArrayOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to ArrayOperator.",
                    parameters[0]
                )
            );
        }
    }
    
    public static OperatorValue DurationConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a DurationConstant, null or missing parameter.");
        }
        
        if(TimeSpan.TryParse(parameters[0], out TimeSpan _value))
        {
            return new DurationOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to DurationOperator.",
                    parameters[0]
                )
            );
        }
    }
    
    public static OperatorValue DateTimeZoneWithDurationConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a DateTimeZoneWithDurationConstant, null or missing parameter.");
        }
        
        if(
                DateTimeOffset.TryParse(parameters[0], out DateTimeOffset _value)
            &&
                TimeSpan.TryParse(parameters[1], out TimeSpan _durationValue)
        )
        {
            return new DateTimeZoneWithDurationOperator(_value, _durationValue);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to DateTimeZoneWithDurationOperator.",
                    parameters[0],
                    parameters[1]
                )
            );
        }
    }
    
    public static OperatorValue IntegerUnitsConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a IntegerUnitsConstant, null or missing parameter.");
        }
        
        if(int.TryParse(parameters[0], out int _value))
        {
            return new IntegerUnitsOperator(_value, parameters[1]);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to IntegerUnitsOperator.",
                    parameters[0],
                    parameters[1]
                )
            );
        }
    }
    
    public static OperatorValue BigIntegerUnitsConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a BigIntegerUnitsConstant, null or missing parameter.");
        }
        
        if(BigInteger.TryParse(parameters[0], out BigInteger _value))
        {
            return new BigIntegerUnitsOperator(_value, parameters[1]);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to BigIntegerUnitsOperator.",
                    parameters[0],
                    parameters[1]
                )
            );
        }
    }
    
    public static OperatorValue DoubleUnitsConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a DoubleUnitsConstant, null or missing parameter.");
        }
        
        if(double.TryParse(parameters[0], out double _value))
        {
            return new DoubleUnitsOperator(_value, parameters[1]);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to DoubleUnitsOperator.",
                    parameters[0],
                    parameters[1]
                )
            );
        }
    }
    
    public static OperatorValue DecimalUnitsConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a DecimalUnitsConstant, null or missing parameter.");
        }
        
        if(decimal.TryParse(parameters[0], out decimal _value))
        {
            return new DecimalUnitsOperator(_value, parameters[1]);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse inputs '{0}', '{1}' to DecimalUnitsOperator.",
                    parameters[0],
                    parameters[1]
                )
            );
        }
    }
    
    public static OperatorValue BooleanConstant(string[] parameters)
    {
        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("Cannot create a BooleanConstant, null or missing parameter.");
        }
        
        if(bool.TryParse(parameters[0], out bool _value))
        {
            return new BooleanOperator(_value);
        }
        else
        {
            throw new ArgumentException(
                string.Format(
                    "Cannot parse input '{0}' to BooleanOperator.",
                    parameters[0]
                )
            );
        }
    }

}