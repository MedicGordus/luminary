using luminary.util;


using System.Text.Json;
using System.Text.Json.Serialization;

namespace luminary.trust;

/// <summary>
/// This holds reference to a secret  keypair used by Gleam.
/// 
/// This is not sharable.
/// </summary>
public class SecretKeypairInfo : PublicKeypairInfo 
{
    
    public const string FIELD_SECRET_KEY_BASE64 = "secret-key-base64";

    /// <summary>
    /// Secret key in base 64.
    /// </summary>
    [JsonPropertyName(FIELD_SECRET_KEY_BASE64)]
    public string? SecretKeyBase64 { get; set; }

    /// <summary>
    /// Hardcode shared private keys are not sharable.
    /// </summary>
    public new static bool Shared => false;
}