using luminary.mapping.functions;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace luminary.mapping;

public static class Helper
{
    //public const string JSON_NAME_PATTERN = @"\""([^\""]*)\""\.?";
    /// <summary>
    /// The pattern is 'a'.'aqb'.'123c'
    /// </summary>
    public const string JSON_NAME_PATTERN = @"'([^']*)'\.?";

    public const string DATE_TIME_ZONE_DURATION_SEPARATOR = "|";

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
                        Dictionary<string, OperatorValue> _parameters = new();
                        BuildPrismOperatorDictionaryFromJsonSchema(_deltaSchema.Value, _parameters);
                        _deltaOv = new PrismOperator(_parameters);
                    }
                    else if(_deltaType == OperatorValue.OperatorValueType.Array)
                    {
                        if(OperatorValue.OperatorValueTypeLookup.TryGetValue(_deltaSchema.Value.ArrayFormat ?? "", out var _arrayType))
                        {
                            _deltaOv = new ArrayOperator(_arrayType, []);
                        }
                        else
                        {
                            throw new Exception(
                                string.Format(
                                    "Unable to build array, specified type '{0}' null or unknown.",
                                    _deltaSchema.Value.ArrayFormat
                                )
                            );
                        }
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

    /// <summary>
    /// Uses the official json schema and the unofficial (defined by us) rules to assign a type.
    /// </summary>
    /// <param name="_schema">Root schema for the property we need the type for.</param>
    /// <returns>The related type or z_error if the type was designated as null.</returns>
    public static OperatorValue.OperatorValueType GetOperatorValueTypeFromSchema(SchemaJson _schema)
    {
        OperatorValue.OperatorValueType _output;

        if(_schema.Type == SchemaJson.JsonTypes.Object)
        {
            _output = OperatorValue.OperatorValueType.Prism;
        }
        else if(_schema.Type == SchemaJson.JsonTypes.Array)
        {
            _output = OperatorValue.OperatorValueType.Array;
        }
        else if(_schema.Type == SchemaJson.JsonTypes.Boolean)
        {
            _output = OperatorValue.OperatorValueType.Boolean;
        }
        else if(_schema.Type == SchemaJson.JsonTypes.Integer)
        {
            _output = OperatorValue.OperatorValueType.Integer;
        }
        else if(_schema.Type == SchemaJson.JsonTypes.String)
        {
            _output = OperatorValue.OperatorValueType.String;
        }
        else if(_schema.Type == SchemaJson.JsonTypes.Null)
        {
            // wtf is a null type lol
            _output = OperatorValue.OperatorValueType.z_error;
        }
        else
        {
            // not an object, array, boolean or null

            if(_schema.Format == null)
            {
                throw new Exception("Unable to get value type for string since the format is missing");
            }
            _output = OperatorValue.OperatorValueTypeLookup[_schema.Format];
        }

        return _output;
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
            while(_deltaJsonName != null && _remainder.Length !=0)
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

    /// <summary>
    /// Maps data to the schema from a payload that MUST fit the specified schema.
    /// </summary>
    /// <param name="_prismData">Empty prism structure to accept the data.</param>
    /// <param name="_payload">Payload of data.</param>
    public static void MapDataPerSchema(Dictionary<string, OperatorValue> _prismData, JsonElement _payload)
    {
        foreach(KeyValuePair<string, OperatorValue> _deltaKeyValue in _prismData)
        {
            JsonElement _childElement = _payload.GetProperty(_deltaKeyValue.Key);

            MapPropertyPerSchema(_deltaKeyValue.Value, _childElement);
        }
    }

    public static void MapPropertyPerSchema(OperatorValue _data, JsonElement _element)
    {
        // parse json data
        switch(_data.Type)
        {
            //// string-based parameters
            //
            case OperatorValue.OperatorValueType.String:
                _data.SetValue(
                    StringOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.BigInteger:
                _data.SetValue(
                    BigIntegerOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.Double:
                _data.SetValue(
                    DoubleOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.Decimal:
                _data.SetValue(
                    DecimalOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.DateTimeOffsetWithDuration:
                _data.SetValue(
                    DateTimeOffsetWithDurationOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.Date:
                _data.SetValue(
                    DateOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.Time:
                _data.SetValue(
                    TimeOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.Duration:
                _data.SetValue(
                    DurationOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            // unit based parameters
            case OperatorValue.OperatorValueType.IntegerUnits:
                _data.SetValue(
                    IntegerUnitsOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.BigIntegerUnits:
                _data.SetValue(
                    BigIntegerUnitsOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.DoubleUnits:
                _data.SetValue(
                    DoubleUnitsOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.DecimalUnits:
                _data.SetValue(
                    DecimalUnitsOperator.BuildFromString(
                        _element.GetString()
                    )
                );
                break;
            //
            ////

            //// native typed parameters
            //
            case OperatorValue.OperatorValueType.Integer:
                ((IntegerOperator)_data).SetNativeValue(_element.GetInt32());
                break;
            //
            case OperatorValue.OperatorValueType.DateTimeOffset:
                _data.SetValue(
                    DateTimeOffsetOperator.BuildFromString(
                        _element.GetDateTime().ToString(DateTimeHelper.DATE_TIME_OFFSET_TO_STRING_FORMAT)
                    )
                );
                break;
            //
            case OperatorValue.OperatorValueType.Boolean:
                ((BooleanOperator)_data).SetNativeValue(_element.GetBoolean());
                break;
            //
            ////

            //// special types
            //
            case OperatorValue.OperatorValueType.Array:
                List<OperatorValue> _arrayData = [];
                OperatorValue.OperatorValueType _arrayType = ((ArrayOperator)_data).ArrayType;
                foreach(var _deltaArrayElement in _element.EnumerateArray())
                {
                    // create blank ov
                    var _deltaData = OperatorValue.CreateByType(_arrayType);
                    if(_deltaData == null)
                    {
                        throw new Exception("Null operator value returned, array type must be bad.");
                    }

                    // fill with data
                    MapPropertyPerSchema(_deltaData, _deltaArrayElement);

                    // add to list
                    _arrayData.Add(_deltaData);
                }
                _data.SetValue(new ArrayOperator(_arrayType, _arrayData));

                break;
            //
            case OperatorValue.OperatorValueType.Prism:
                MapDataPerSchema(((PrismOperator)_data).GetValue(), _element);
                break;
            //
            default:
                throw new Exception(
                    string.Format(
                        "Unable to map type '{0}'",
                        _data.Type
                    )
                );
            //
            ////
        }
    }

    public static string ConvertPrismOperatorToJsonString(Dictionary<string, OperatorValue>? _keyValuePairs)
    {
        if(_keyValuePairs == null)
        {
            return "null";
        }

        StringBuilder _output = new StringBuilder();

        _output.Append('{');

        if(_keyValuePairs.Count != 0)
        {
            foreach(KeyValuePair<string, OperatorValue> _deltaKeyValuePair in _keyValuePairs)
            {
                _output.Append(
                    string.Format(
                        "\"{0}\":",
                        _deltaKeyValuePair.Key
                    )
                );

                switch(_deltaKeyValuePair.Value.Type)
                {
                    case OperatorValue.OperatorValueType.Prism:
                        _output.Append(
                            ConvertPrismOperatorToJsonString(((PrismOperator)_deltaKeyValuePair.Value).GetValue())
                        );
                        break;

                    default:
                        _output.Append(
                            _deltaKeyValuePair.Value.ToJsonStringValue()
                        );
                        break;
                }
                _output.Append(',');
            }

            // remove the last comma
            _output.Length -= 1;
        }

        _output.Append('}');

        return _output.ToString();
    }

/* this code is now in the array operator code
    public static string ConvertArrayOperatorToJsonString(OperatorValue.OperatorValueType _arrayType, List<OperatorValue>? _entries)
    {
        if(_entries == null)
        {
            return "null";
        }

        StringBuilder _output = new StringBuilder();

        _output.Append('[');

        if(_entries.Count != 0)
        {
            // this is somewhat wasteful since arrays are supposed to hold only one type but I think it is best
            foreach(OperatorValue _deltaEntry in _entries)
            {
                switch(_deltaEntry.Type)
                {
                    case OperatorValue.OperatorValueType.Array:
                        _output.Append(
                            ConvertArrayOperatorToJsonString(((ArrayOperator)_deltaEntry).ArrayType, ((ArrayOperator)_deltaEntry).GetValue())
                        );
                        break;

                    case OperatorValue.OperatorValueType.Prism:
                        _output.Append(
                            ConvertPrismOperatorToJsonString(((PrismOperator)_deltaEntry).GetValue())
                        );
                        break;

                    default:
                        _output.Append(
                            _deltaEntry.ToJsonStringValue()
                        );
                        break;
                }
                _output.Append(',');
            }

            // remove the last comma
            _output.Length -= 1;
        }

        _output.Append(']');

        return _output.ToString();
    }
*/
}