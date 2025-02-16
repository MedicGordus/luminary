using System.Security.Cryptography;


namespace luminary.util;

public static class HmacSha256
{

    private const int KEY_SIZE = 64;

    private const int DEFAULT_TOTP_DIGITS_TO_RETURN = 6;

    private const int DEFAULT_TOTP_SECONDS_TO_ALLOW = 60;

    private static readonly byte[] INNER_PAD = [.. Enumerable.Repeat((byte)0x36, KEY_SIZE)];
    private static readonly byte[] OUTER_PAD = [.. Enumerable.Repeat((byte)0x5C, KEY_SIZE)];

    public static string ComputeTotp(byte[] _secretKey, long _millisecondsSinceEpoch, int _digitsToReturn = DEFAULT_TOTP_DIGITS_TO_RETURN, int _secondsToAllow = DEFAULT_TOTP_SECONDS_TO_ALLOW)
    {
        long _ms = _millisecondsSinceEpoch / (_secondsToAllow * 1000);
        byte[] _messageBytes = BitConverter.GetBytes((ulong)_ms);

        // ensure the key is KEY_SIZE bytes exactly
        if(_secretKey.Length > KEY_SIZE)
        {
            // hash the key if it is too long so that the remaining bits still apply to the resulting usage of the key
            _secretKey = ComputeSha256Hash(_secretKey)[0..KEY_SIZE];
        }
        else if(_secretKey.Length < KEY_SIZE)
        {
            // pads the right side of the array with zeroes to make sure it is KEY_SIZE bytes long

            int _oldLength = _secretKey.Length;
            int _paddingLength = KEY_SIZE - _secretKey.Length;

            byte[] _padding = [.. Enumerable.Repeat((byte)0, _paddingLength)];

            Array.Resize(ref _secretKey, KEY_SIZE);
            Buffer.BlockCopy(_padding, 0, _secretKey, _oldLength, _paddingLength);
        }

        // calculate the xor'd arrays
        byte[] _innerXorKey = XorByteArrays(_secretKey, INNER_PAD);
        byte[] _outerXorKey = XorByteArrays(_secretKey, OUTER_PAD);

        // append inner xor'd key with the milliseconds since epoch
        byte[] _bytesToHash = new byte[_innerXorKey.Length + _messageBytes.Length];
        Buffer.BlockCopy(_innerXorKey, 0, _bytesToHash, 0, _innerXorKey.Length);
        Buffer.BlockCopy(_messageBytes, 0, _bytesToHash, _innerXorKey.Length, _messageBytes.Length);

        // perform the first hash
        byte[] _hashedResult = ComputeSha256Hash(_bytesToHash);
        
        // append the hash result with the outer xor'd key
        _bytesToHash = new byte[_outerXorKey.Length + _hashedResult.Length];
        Buffer.BlockCopy(_outerXorKey, 0, _bytesToHash, 0, _outerXorKey.Length);
        Buffer.BlockCopy(_hashedResult, 0, _bytesToHash, _outerXorKey.Length, _hashedResult.Length);
        
        // perform the second hash
        _hashedResult = ComputeSha256Hash(_bytesToHash);


        // now we convert the appropriate bytes to string format


        // Dynamic truncation
        int _offset = _hashedResult[_hashedResult.Length - 1] & 0x0F;
        int _binary = ((_hashedResult[_offset] & 0x7F) << 24) |
                    ((_hashedResult[_offset + 1] & 0xFF) << 16) |
                    ((_hashedResult[_offset + 2] & 0xFF) << 8) |
                    (_hashedResult[_offset + 3] & 0xFF);

        // Ensure positive number
        int _totp = _binary & 0x7FFFFFFF;

        // conversion to string
        int _otp = _totp % (int)Math.Pow(10, _digitsToReturn);

        return _otp.ToString();
    }

    private static byte[] ComputeSha256Hash(byte[] rawData)
    {
        // Compute the hash from the byte array.
        return SHA256.HashData(rawData);
    }

    private static byte[] XorByteArrays(byte[] array1, byte[] array2)
    {
        if (array1.Length != array2.Length)
        {
            throw new ArgumentException("Both byte arrays must have the same length.");
        }

        byte[] result = new byte[array1.Length];
        for (int i = 0; i < array1.Length; i++)
        {
            result[i] = (byte)(array1[i] ^ array2[i]);
        }
        return result;
    }
}