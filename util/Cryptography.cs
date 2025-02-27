namespace luminary.util;


using System;
using System.Security.Cryptography;

public static class Cryptography
{
    /// <summary>
    /// Verify signature using public key.
    /// </summary>
    /// <param name="_publicKey"></param>
    /// <param name="_data"></param>
    /// <param name="_signatureBase64"></param>
    public static bool VerifySignature(PublicKeyJson _publicKey, byte[] _data, string _signatureBase64)
    {
        if (_publicKey?.KeyType == null || _publicKey.PublicKeyBase64 == null)
            throw new ArgumentNullException(nameof(_publicKey));

        byte[] publicKeyBytes = Convert.FromBase64String(_publicKey.PublicKeyBase64);
        byte[] signatureBytes = Convert.FromBase64String(_signatureBase64);

        switch (_publicKey.KeyType.ToLower())
        {
            case "secp256r1":
            case "secp384r1":
            case "secp521r1":
                using (var ecdsa = ECDsa.Create())
                {
                    ecdsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
                    return ecdsa.VerifyData(_data, signatureBytes, HashAlgorithmName.SHA256);
                }

            case "rsa2048":
            case "rsa3072":
            case "rsa4096":
            case "rsa7680":
            case "rsa15360":
                using (var rsa = RSA.Create())
                {
                    rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
                    return rsa.VerifyData(_data, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }

            case "ed25519":
                throw new NotSupportedException("Ed25519 is not natively supported in .NET yet");

            case "secp256k1":
                throw new NotSupportedException("secp256k1 requires additional libraries");

            default:
                throw new ArgumentException($"Unsupported key type: {_publicKey.KeyType}");
        }
    }

    /// <summary>
    /// Sign data using private key bytes.
    /// </summary>
    /// <param name="_privateKeyBytes"></param>
    /// <param name="_message"></param>
    /// <param name="_keyType"></param>
    public static byte[] SignData(byte[] _privateKeyBytes, byte[] _message, string _keyType)
    {
        switch (_keyType.ToLower())
        {
            case "secp256r1":
            case "secp384r1":
            case "secp521r1":
                using (var ecdsa = ECDsa.Create())
                {
                    ecdsa.ImportPkcs8PrivateKey(_privateKeyBytes, out _);
                    return ecdsa.SignData(_message, HashAlgorithmName.SHA256);
                }

            case "rsa2048":
            case "rsa3072":
            case "rsa4096":
            case "rsa7680":
            case "rsa15360":
                using (var rsa = RSA.Create())
                {
                    rsa.ImportPkcs8PrivateKey(_privateKeyBytes, out _);
                    return rsa.SignData(_message, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }

            case "ed25519":
                throw new NotSupportedException("Ed25519 is not natively supported in .NET yet");

            case "secp256k1":
                throw new NotSupportedException("secp256k1 requires additional libraries");

            default:
                throw new ArgumentException($"Unsupported key type: {_keyType}");
        }
    }

    /// <summary>
    /// Generate key pair.
    /// </summary>
    /// <param name="_keyType"></param>
    public static (byte[] _privateKey, PublicKeyJson _publicKey) GenerateKeyPair(string _keyType)
    {
        switch (_keyType.ToLower())
        {
            case "secp256r1":
                using (var ecdsa256 = ECDsa.Create(ECCurve.NamedCurves.nistP256))
                {
                    var privateKey = ecdsa256.ExportPkcs8PrivateKey();
                    var publicKey = ecdsa256.ExportSubjectPublicKeyInfo();
                    return (privateKey, new PublicKeyJson
                    {
                        KeyType = "secp256r1",
                        PublicKeyBase64 = Convert.ToBase64String(publicKey)
                    });
                }

            case "secp384r1":
                using (var ecdsa384 = ECDsa.Create(ECCurve.NamedCurves.nistP384))
                {
                    var privateKey = ecdsa384.ExportPkcs8PrivateKey();
                    var publicKey = ecdsa384.ExportSubjectPublicKeyInfo();
                    return (privateKey, new PublicKeyJson
                    {
                        KeyType = "secp384r1",
                        PublicKeyBase64 = Convert.ToBase64String(publicKey)
                    });
                }

            case "secp521r1":
                using (var ecdsa521 = ECDsa.Create(ECCurve.NamedCurves.nistP521))
                {
                    var privateKey = ecdsa521.ExportPkcs8PrivateKey();
                    var publicKey = ecdsa521.ExportSubjectPublicKeyInfo();
                    return (privateKey, new PublicKeyJson
                    {
                        KeyType = "secp521r1",
                        PublicKeyBase64 = Convert.ToBase64String(publicKey)
                    });
                }

            case "rsa2048":
            case "rsa3072":
            case "rsa4096":
            case "rsa7680":
            case "rsa15360":
                int keySize = int.Parse(_keyType.Substring(3));
                using (var rsa = RSA.Create(keySize))
                {
                    var privateKey = rsa.ExportPkcs8PrivateKey();
                    var publicKey = rsa.ExportSubjectPublicKeyInfo();
                    return (privateKey, new PublicKeyJson
                    {
                        KeyType = _keyType,
                        PublicKeyBase64 = Convert.ToBase64String(publicKey)
                    });
                }

            case "ed25519":
                throw new NotSupportedException("Ed25519 is not natively supported in .NET yet");

            case "secp256k1":
                throw new NotSupportedException("secp256k1 requires additional libraries");

            default:
                throw new ArgumentException($"Unsupported key type: {_keyType}");
        }
    }
}