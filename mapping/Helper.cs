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

    public static void BuildPrismOperatorDictionaryFromJsonSchema(SchemaJson _schema, IDictionary<string, OperatorValue> _outputDictionary)
    {
        if (_outputDictionary == null)
        {
            throw new ArgumentNullException("Unable to build a prism operator when the dictionary is null.");
        }

        if (_schema.Properties != null)
        {
            foreach (KeyValuePair<string, SchemaJson> deltaSchema in _schema.Properties)
            {
                var deltaType = GetOperatorValueTypeFromSchema(deltaSchema.Value);
                OperatorValue? deltaOv = null;

                // setup further structure for objects (prisms) and arrays
                if (deltaSchema.Value.Properties != null && deltaSchema.Value.Properties.Count != 0)
                {
                    if (deltaType == OperatorValue.OperatorValueType.Prism)
                    {
                        Dictionary<string, OperatorValue> parameters = new();
                        BuildPrismOperatorDictionaryFromJsonSchema(deltaSchema.Value, parameters);
                        deltaOv = new PrismOperator(parameters);
                    }
                    else if (deltaType == OperatorValue.OperatorValueType.Array)
                    {
                        if (OperatorValue.OperatorValueTypeLookup.TryGetValue(deltaSchema.Value.ArrayFormat ?? "", out var arrayType))
                        {
                            deltaOv = new ArrayOperator(arrayType, []);
                        }
                        else
                        {
                            throw new Exception(
                                string.Format(
                                    "Unable to build array, specified type '{0}' null or unknown.",
                                    deltaSchema.Value.ArrayFormat
                                )
                            );
                        }
                    }
                }
                else
                {
                    deltaOv = OperatorValue.CreateByType(deltaType);
                }

                if (deltaOv == null)
                {
                    throw new Exception("Parsing of property failed, properties were not null or empty but the parent was not a prism or an array.");
                }

                _outputDictionary.Add(deltaSchema.Key, deltaOv);
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
        OperatorValue.OperatorValueType output;

        if (_schema.Type == SchemaJson.JsonTypes.OBJECT)
        {
            output = OperatorValue.OperatorValueType.Prism;
        }
        else if (_schema.Type == SchemaJson.JsonTypes.ARRAY)
        {
            output = OperatorValue.OperatorValueType.Array;
        }
        else if (_schema.Type == SchemaJson.JsonTypes.BOOLEAN)
        {
            output = OperatorValue.OperatorValueType.Boolean;
        }
        else if (_schema.Type == SchemaJson.JsonTypes.INTEGER)
        {
            output = OperatorValue.OperatorValueType.Integer;
        }
        else if (_schema.Type == SchemaJson.JsonTypes.STRING)
        {
            output = OperatorValue.OperatorValueType.String;
        }
        else if (_schema.Type == SchemaJson.JsonTypes.NULL)
        {
            // wtf is a null type lol
            output = OperatorValue.OperatorValueType.z_error;
        }
        else
        {
            // not an object, array, boolean or null

            if (_schema.Format == null)
            {
                throw new Exception("Unable to get value type for string since the format is missing");
            }
            output = OperatorValue.OperatorValueTypeLookup[_schema.Format];
        }

        return output;
    }

    public static OperatorValue? GetTarget(string _targetIdentifier, PrismOperator _prismContainingTarget)
    {
        (var deltaJsonName, var remainder) = RetrieveNextJsonName(_targetIdentifier);
        if (deltaJsonName == null)
        {
            throw new ArgumentException(
                string.Format(
                    "Could not get target by name '{0}', null was the first name returned.",
                    _targetIdentifier
                )
            );
        }
        OperatorValue? deltaOperator = _prismContainingTarget.GetOperatorByName(deltaJsonName);
        if (deltaOperator != null)
        {
            while (deltaJsonName != null && remainder.Length != 0)
            {
                if (deltaOperator is not PrismOperator)
                {
                    throw new ArgumentException(
                        string.Format(
                            "Could not get target by name '{0}', encountered a non-object in the object chain.",
                            _targetIdentifier
                        )
                    );
                }
                deltaOperator = ((PrismOperator)deltaOperator).GetOperatorByName(deltaJsonName);
                (deltaJsonName, remainder) = RetrieveNextJsonName(remainder);
            }
        }

        return deltaOperator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_input">Json parameter name, such as "object"."string"</param>
    /// <returns>
    /// First tuple: The first matching json name or null if there are none.
    /// Second tuple: The remainder of the string, or empty if there is nothing else left.
    /// </returns>
    public static (string?, string) RetrieveNextJsonName(string _input)
    {
        MatchCollection matches = Regex.Matches(_input, JSON_NAME_PATTERN);

        foreach (Match match in matches)
        {
            if (match.Success)
            {
                return (match.Groups[1].Value, _input.Substring(match.Groups[0].Value.Length));
            }
        }

        return (null, _input);
    }

    /// <summary>
    /// Maps data to the schema from a payload that MUST fit the specified schema.
    /// </summary>
    /// <param name="_prismData">Empty prism structure to accept the data.</param>
    /// <param name="_payload">Payload of data.</param>
    public static void MapDataPerSchema(Dictionary<string, OperatorValue> _prismData, JsonElement _payload)
    {
        foreach (KeyValuePair<string, OperatorValue> deltaKeyValue in _prismData)
        {
            JsonElement childElement = _payload.GetProperty(deltaKeyValue.Key);

            MapPropertyPerSchema(deltaKeyValue.Value, childElement);
        }
    }

    public static void MapPropertyPerSchema(OperatorValue _data, JsonElement _element)
    {
        // parse json data
        switch (_data.Type)
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
                List<OperatorValue> arrayData = [];
                OperatorValue.OperatorValueType arrayType = ((ArrayOperator)_data).ArrayType;
                foreach (var deltaArrayElement in _element.EnumerateArray())
                {
                    // create blank ov
                    var deltaData = OperatorValue.CreateByType(arrayType) ?? throw new Exception("Null operator value returned, array type must be bad.");

                    // fill with data
                    MapPropertyPerSchema(deltaData, deltaArrayElement);

                    // add to list
                    arrayData.Add(deltaData);
                }
                _data.SetValue(new ArrayOperator(arrayType, arrayData));

                break;
            //
            case OperatorValue.OperatorValueType.Prism:
                MapDataPerSchema(((PrismOperator)_data).GetValue() ?? throw new Exception("Null operator value returned, prism must be corrupt."), _element);
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
        if (_keyValuePairs == null)
        {
            return "null";
        }

        StringBuilder output = new StringBuilder();

        output.Append('{');

        if (_keyValuePairs.Count != 0)
        {
            foreach (KeyValuePair<string, OperatorValue> deltaKeyValuePair in _keyValuePairs)
            {
                output.Append(
                    string.Format(
                        "\"{0}\":",
                        deltaKeyValuePair.Key
                    )
                );

                switch (deltaKeyValuePair.Value.Type)
                {
                    case OperatorValue.OperatorValueType.Prism:
                        output.Append(
                            ConvertPrismOperatorToJsonString(((PrismOperator)deltaKeyValuePair.Value).GetValue())
                        );
                        break;

                    default:
                        output.Append(
                            deltaKeyValuePair.Value.ToJsonStringValue()
                        );
                        break;
                }
                output.Append(',');
            }

            // remove the last comma
            output.Length -= 1;
        }

        output.Append('}');

        return output.ToString();
    }

    public static PrismOperator BuildPrismFromJsonDocument(JsonDocument _inputPayload, JsonDocument _expectedInputPrismSchema)
    {
        SchemaJson schema = JsonSerializer.Deserialize<SchemaJson>(_expectedInputPrismSchema) ?? throw new ArgumentException("Could not parse json element into json schema.");

        return BuildPrismFromJsonDocument(_inputPayload, _expectedInputPrismSchema);        
    }

    public static PrismOperator BuildPrismFromJsonDocument(JsonDocument _inputPayload, SchemaJson _expectedInputPrismSchema)
    {
        //// build empty prism operator from expected input
        //
        var inputPayloadPrismDictionary = new Dictionary<string, OperatorValue>();
        Helper.BuildPrismOperatorDictionaryFromJsonSchema(_expectedInputPrismSchema, inputPayloadPrismDictionary);
        //
        ////

        //// map data from the payload (this way the entire input isn't "wastefully" mapped, only what is defined in the schema)
        //
        Helper.MapDataPerSchema(inputPayloadPrismDictionary, _inputPayload.RootElement);
        //
        ////

        // at this point, an empty prism dictionary was structured, and then input data was parsed across from the payload

        // return a new operator from the prism dictionary
        return new PrismOperator(inputPayloadPrismDictionary);
    }
}