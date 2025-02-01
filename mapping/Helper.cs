using luminary.functions;

using System.Text.Json;
using System.Text.RegularExpressions;

namespace luminary.mapping;

public static class Helper
{
    public const string JSON_NAME_PATTERN = @"\""([^\""]*)\""\.?";

    public static void BuildPrismOperatorDictionaryFromJsonSchema(SchemaJson _schema, IDictionary<string, OperatorValue> outputDictionary)
    {
        if(outputDictionary == null)
        {
            throw new ArgumentNullException("Unable to build a prism operator when the dictionary is null.");
        }

        if(_schema.Properties != null)
        {
            foreach(KeyValuePair<string, SchemaJson> _deltaSchema in _schema.Properties)
            {
                var _deltaType = GetOperatorValueTypeFromSchema(_deltaSchema.Value);
                OperatorValue? _deltaOv = null;

                // setup further structure for objects (prisms) and arrays
                if(_deltaSchema.Value.Properties != null && _deltaSchema.Value.Properties.Count != 0)
                {
                    if(_deltaType == OperatorValue.OperatorValueType.Prism)
                    {
                        Dictionary<string, OperatorValue> _parameters = todo();
                        _deltaOv = new PrismOperator(_parameters);
                    }
                    else if(_deltaType == OperatorValue.OperatorValueType.Array)
                    {
                        _deltaOv = todo();
                    }
                }
                else
                {
                    _deltaOv = OperatorValue.CreateByType(_deltaType);
                }

                if(_deltaOv == null)
                {
                    throw new Exception("Parsing of property failed, properties were not null or empty but the parent was not a prism or an array.");
                }

                outputDictionary.Add(_deltaSchema.Key, _deltaOv);
            }
        }
    }

    public static OperatorValue? GetTarget(string targetIdentifier, PrismOperator prismContainingTarget)
    {
        (var _deltaJsonName, var _remainder) = RetrieveNextJsonName(targetIdentifier);
        if(_deltaJsonName == null)
        {
            throw new ArgumentException(
                string.Format(
                    "Could not get target by name '{0}', null was the first name returned.",
                    targetIdentifier
                )
            );
        }
        OperatorValue? deltaOperator = prismContainingTarget.GetOperatorByName(_deltaJsonName);
        if(deltaOperator != null)
        {
            while(_deltaJsonName != null)
            {
                if(deltaOperator is not PrismOperator)
                {
                    throw new ArgumentException(
                        string.Format(
                            "Could not get target by name '{0}', encountered a non-object in the object chain.",
                            targetIdentifier
                        )
                    );
                }
                deltaOperator = ((PrismOperator)deltaOperator).GetOperatorByName(_deltaJsonName);
                (_deltaJsonName, _remainder) = RetrieveNextJsonName(_remainder);
            }
        }

        return deltaOperator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input">Json parameter name, such as "object"."string"</param>
    /// <returns>
    /// First tuple: The first matching json name or null if there are none.
    /// Second tuple: The remainder of the string, or empty if there is nothing else left.
    /// </returns>
    public static (string?, string) RetrieveNextJsonName(string input)
    {
        MatchCollection matches = Regex.Matches(input, JSON_NAME_PATTERN);

        foreach (Match match in matches)
        {
            if (match.Success)
            {
                return (match.Groups[1].Value, input.Substring(match.Groups[0].Value.Length));
            }
        }

        return (null,input);
    }
}