using luminary.util;


using System.Text.Json;
using System.Text.Json.Serialization;

namespace luminary.trust;

/// <summary>
/// This holds reference to info about a public keypair used by Gleam.
/// 
/// This is sharable.
/// </summary>
public class PublicKeypairInfo : KeypairInfo
{
    public const string FIELD_LABEL = "label";
    /// <summary>
    /// Arbitrary label applied to this keypair.
    /// </summary>
    [JsonPropertyName(FIELD_LABEL)]
    public string? Label { get; set; }


    public const string FIELD_PUBLIC_KEY_BASE64 = "public-key-base64";
    /// <summary>
    /// Public key in base 64.
    /// </summary>
    [JsonPropertyName(FIELD_PUBLIC_KEY_BASE64)]
    public string? PublicKeyBase64 { get; set; }


    public const string FIELD_GROUPS = "groups";
    /// <summary>
    /// What groups this keypair belongs to.
    /// </summary>
    [JsonPropertyName(FIELD_GROUPS)]
    public List<string>? Groups { get; set; }


    public const string FIELD_EFFECTIVE_START_DATE = "effective-start-date";
    /// <summary>
    /// When this keypair is effective (null is not a valid value).
    /// </summary>
    [JsonPropertyName(FIELD_EFFECTIVE_START_DATE)]
    public DateTime? EffectiveStartDate { get; set; }


    public const string FIELD_EFFECTIVE_END_DATE = "effective-end-date";
    /// <summary>
    /// When this keypair is no longer effective (or null if no end date is specified).
    /// </summary>
    [JsonPropertyName(FIELD_EFFECTIVE_END_DATE)]
    public DateTime? EffectiveEndDate { get; set; }


    public const string FIELD_SHARED = "shared";
    /// <summary>
    /// Flag to indicate this keypair should be published on the Gleam endpoint.
    /// </summary>
    [JsonPropertyName(FIELD_SHARED)]
    public bool? Shared { get; set; }


    public const string FIELD_KEYPAIR_TYPE = "keypair-type";
    /// <summary>
    /// Type of keypair. See <seealso cref="KeypairInfo.KeyPairType">KeypairInfo.KeyPairType</seealso>.
    /// </summary>
    [JsonPropertyName(FIELD_KEYPAIR_TYPE)]
    public string? KeypairType { get; set; }
}