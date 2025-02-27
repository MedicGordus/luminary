using System.Text.Json.Serialization;


namespace luminary.util;

public class PublicKeyJson
{
    /// <summary>
    /// Keys/sizes allowed:
    ///     secp256r1
    ///     secp384r1
    ///     secp521r1
    ///     rsa2048
    ///     rsa3072
    ///     rsa4096
    ///     rsa7680
    ///     rsa15360
    ///     
    ///     ed25519
    ///     secp256k1
    /// </summary>
    [JsonPropertyName("key-type")]
    public string? KeyType { get; set; }


    [JsonPropertyName("public-key-base64")]
    public string? PublicKeyBase64 { get; set; }
}