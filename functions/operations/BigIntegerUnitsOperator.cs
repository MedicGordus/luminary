
using System.Numerics;

namespace luminary.functions;

public class BigIntegerUnitsOperator : BigIntegerOperator
{
    protected string? NullableUnit;

    public BigIntegerUnitsOperator(BigInteger? nullableValue, string? nullableUnit) : base(nullableValue, OperatorValueType.BigIntegerUnits)
    {
        NullableUnit = nullableUnit;
    }
    
    public new static OperatorValue BuildFromParameters(string[] parameters)
    {
        if(parameters == null || parameters.Length < 2)
        {
            throw new ArgumentException("Cannot create a BigIntegerUnitsOperator, null or missing parameter.");
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

#region "overloaded functions with unit checks"

    public override OperatorValue? BooleanAnd(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBooleanAnd(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot booleanand when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBitwiseMod(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot bitwisemod when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? BitwiseRightShift(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBitwiseRightShift(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot bitwiserightshift when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }

        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBitwiseXor(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot bitwisexor when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? ConvertToString(OperatorValue[]? parameters)
    {
        return StringOperator.BuildFromString(
            string.Format(
                "{0} {1}",
                NullableValue,
                NullableUnit
            )
        );
    }

    public override OperatorValue? ValueEqual(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableValueEqual(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot valueequal when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(NullableValue != null & NullableUnit != null);
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableGreaterOrEqual(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot greaterorequal when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableGreatherThan(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot greaterthan when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? LessOrEqual(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableLessOrEqual(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot lessorequal when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableLessThan(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot lessthan when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BooleanOperator(
            _parameterValue
        );
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathAdd(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathadd when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathAverage(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathaverage when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? MathDivide(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathDivide(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathdivide when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathMultiply(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathmultiply when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? MathPower(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathPower(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathpower when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableMathSubtract(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot mathsubtract when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        bool _parameterValue = base.InheritableNotEqual(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;
        
        return new BooleanOperator(_parameterValue & (_paramUnit != NullableUnit));
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBooleanOr(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot booleanor when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        // this does the nullable checks, etc. for us, on top of retrieving the value
        BigInteger _parameterValue = base.InheritableBooleanNot(parameters);

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot booleannot when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            throw new ArgumentException(
                string.Format(
                    "Units mismatch, left '{0}', right '{1}'.",
                    NullableUnit,
                    _paramUnit
                )
            );
        }
        
        return new BigIntegerUnitsOperator(
            _parameterValue,
            NullableUnit
        );
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        return base.ConvertToDecimal(parameters);
    }

    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        return base.ConvertToDouble(parameters);
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        return base.ConvertToInteger(parameters);
    }

#endregion
    

    public override OperatorValue? ConvertToDecimalUnits(OperatorValue[]? parameters)
    {
        todo("do we make a base call to decimal functions or build it out here?");
    }

    public override OperatorValue? ConvertToDoubleUnits(OperatorValue[]? parameters)
    {
        todo("do we make a base call to double functions or build it out here?");
    }

    public override OperatorValue? ConvertToIntegerUnits(OperatorValue[]? parameters)
    {
        todo("do we make a base call to integer functions or build it out here?");
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? parameters)
    {
        return new BigIntegerOperator(NullableValue);
    }

    public override OperatorValue? IfNotFilled(OperatorValue[]? parameters)
    {
        if(NullableValue != null && NullableUnit != null)
        {
            return new BigIntegerUnitsOperator(NullableValue, NullableUnit);
        }

        if(parameters == null || parameters.Length == 0)
        {
            throw new ArgumentException("parameters null or parameter missing. Cannot execute ifnotfilled.");
        }

        return parameters[0];
    }

    public new static OperatorValue BuildFromString(string? _input)
    {
        if(_input == null)
        {
            return new BigIntegerUnitsOperator((BigInteger?)null, null);
        }

        (BigInteger _bigIntegerValue, string _bigIntegerUnits) = UnitHelper.ParseStringToBigIntegerUnits(_input);
        return new BigIntegerUnitsOperator(_bigIntegerValue, _bigIntegerUnits);
    }

    public override void SetValue(OperatorValue _source)
    {
        if(_source is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Source OperatorValue wrong type, cannot set value.");
        }

        NullableValue = ((BigIntegerUnitsOperator)_source).NullableValue;
        NullableUnit = ((BigIntegerUnitsOperator)_source).NullableUnit;
    }
}