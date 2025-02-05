
using System.Numerics;

namespace luminary.functions;

public class BigIntegerUnitsOperator : BigIntegerOperator
{
    protected string? NullableUnit;

    public BigIntegerUnitsOperator(BigInteger? nullableValue, string? nullableUnit) : base(nullableValue, OperatorValueType.BigIntegerUnits)
    {
        NullableUnit = nullableUnit;
    }

    protected BigIntegerUnitsOperator(BigIntegerOperator _op, string? nullableUnit)  : base(_op.GetValue(), OperatorValueType.BigIntegerUnits)
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
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanand.");
        }

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
            ((BigIntegerOperator?)base.BooleanAnd(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? BitwiseMod(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisemod.");
        }

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
            ((BigIntegerOperator?)base.BitwiseMod(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? BitwiseXor(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute bitwisexor.");
        }

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
            ((BigIntegerOperator?)base.BitwiseXor(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? ConvertToBigInteger(OperatorValue[]? parameters)
    {
        return new BigIntegerUnitsOperator(NullableValue, NullableUnit);
    }

    public override OperatorValue? ConvertToDecimal(OperatorValue[]? parameters)
    {
        return new BigIntegerUnitsOperator(
            ((BigIntegerOperator?)base.ConvertToDecimal(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }
    public override OperatorValue? ConvertToDouble(OperatorValue[]? parameters)
    {
        return new BigIntegerUnitsOperator(
            ((BigIntegerOperator?)base.ConvertToDouble(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? ConvertToInteger(OperatorValue[]? parameters)
    {
        return new BigIntegerUnitsOperator(
            ((BigIntegerOperator?)base.ConvertToInteger(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
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
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute valueequal.");
        }

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
        
        return new BigIntegerUnitsOperator(
            ((BigIntegerOperator?)base.ValueEqual(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? Filled(OperatorValue[]? parameters)
    {
        return new BooleanOperator(NullableValue != null & NullableUnit != null);
    }

    public override OperatorValue? GreaterOrEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterorequal.");
        }

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
        
        return new BigIntegerUnitsOperator(
            ((BigIntegerOperator?)base.GreaterOrEqual(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? GreatherThan(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute greaterthan.");
        }

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
        
        return new BigIntegerUnitsOperator(
            ((BigIntegerOperator?)base.GreatherThan(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
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

    public override OperatorValue? LessOrEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessorequal.");
        }

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
        
        return new BigIntegerUnitsOperator(
            ((BigIntegerOperator?)base.LessOrEqual(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? LessThan(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute lessthan.");
        }

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
        
        return new BigIntegerUnitsOperator(
            ((BigIntegerOperator?)base.LessThan(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? MathAdd(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathadd.");
        }

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
            ((BigIntegerOperator?)base.MathAdd(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? MathAverage(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathaverage.");
        }

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
            ((BigIntegerOperator?)base.MathAverage(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? MathDivide(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathdivide.");
        }

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
            ((BigIntegerOperator?)base.MathDivide(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? MathMultiply(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathmultiply.");
        }

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
            ((BigIntegerOperator?)base.MathMultiply(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? MathPower(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathpower.");
        }

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
            ((BigIntegerOperator?)base.MathPower(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? MathSubtract(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute mathsubtract.");
        }

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
            ((BigIntegerOperator?)base.MathSubtract(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? NotEqual(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute notequal.");
        }

        if(parameters[0] is not BigIntegerUnitsOperator)
        {
            throw new ArgumentException("Cannot notequal when the parameter is not a bigintegerunits.");
        }

        var _paramUnit = ((BigIntegerUnitsOperator)parameters[0]).NullableUnit;

        if(_paramUnit != NullableUnit)
        {
            return new BooleanOperator(false);
        }
        
        return base.NotEqual(parameters);
    }

    public override OperatorValue? BooleanOr(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleanor.");
        }

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
            ((BigIntegerOperator?)base.BooleanOr(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }

    public override OperatorValue? BooleanNot(OperatorValue[]? parameters)
    {
        if(NullableValue == null || parameters == null || parameters.Length == 0 || parameters[0] == null)
        {
            throw new ArgumentException("Value null, parameters null or parameter missing. Cannot execute booleannot.");
        }

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
            ((BigIntegerOperator?)base.BooleanNot(parameters)) ?? throw new Exception("Base operator unexpectedly returned null"),
            NullableUnit
        );
    }
#endregion
    
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