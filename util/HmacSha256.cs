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
        long ms = _millisecondsSinceEpoch / (_secondsToAllow * 1000);
        byte[] messageBytes = BitConverter.GetBytes((ulong)ms);

        // ensure the key is KEY_SIZE bytes exactly
        if (_secretKey.Length > KEY_SIZE)
        {
            // hash the key if it is too long so that the remaining bits still apply to the resulting usage of the key
            _secretKey = ComputeSha256Hash(_secretKey)[0..KEY_SIZE];
        }
        else if (_secretKey.Length < KEY_SIZE)
        {
            // pads the right side of the array with zeroes to make sure it is KEY_SIZE bytes long

            int oldLength = _secretKey.Length;
            int paddingLength = KEY_SIZE - _secretKey.Length;

            byte[] padding = [.. Enumerable.Repeat((byte)0, paddingLength)];

            Array.Resize(ref _secretKey, KEY_SIZE);
            Buffer.BlockCopy(padding, 0, _secretKey, oldLength, paddingLength);
        }

        // calculate the xor'd arrays
        byte[] innerXorKey = XorByteArrays(_secretKey, INNER_PAD);
        byte[] outerXorKey = XorByteArrays(_secretKey, OUTER_PAD);

        // append inner xor'd key with the milliseconds since epoch
        byte[] bytesToHash = new byte[innerXorKey.Length + messageBytes.Length];
        Buffer.BlockCopy(innerXorKey, 0, bytesToHash, 0, innerXorKey.Length);
        Buffer.BlockCopy(messageBytes, 0, bytesToHash, innerXorKey.Length, messageBytes.Length);

        // perform the first hash
        byte[] hashedResult = ComputeSha256Hash(bytesToHash);

        // append the hash result with the outer xor'd key
        bytesToHash = new byte[outerXorKey.Length + hashedResult.Length];
        Buffer.BlockCopy(outerXorKey, 0, bytesToHash, 0, outerXorKey.Length);
        Buffer.BlockCopy(hashedResult, 0, bytesToHash, outerXorKey.Length, hashedResult.Length);

        // perform the second hash
        hashedResult = ComputeSha256Hash(bytesToHash);


        // now we convert the appropriate bytes to string format


        // Dynamic truncation
        int offset = hashedResult[hashedResult.Length - 1] & 0x0F;
        int binary = ((hashedResult[offset] & 0x7F) << 24) |
                    ((hashedResult[offset + 1] & 0xFF) << 16) |
                    ((hashedResult[offset + 2] & 0xFF) << 8) |
                    (hashedResult[offset + 3] & 0xFF);

        // Ensure positive number
        int totp = binary & 0x7FFFFFFF;

        // conversion to string
        int otp = totp % (int)Math.Pow(10, _digitsToReturn);

        return otp.ToString();
    }

    private static byte[] ComputeSha256Hash(byte[] _rawData)
    {
        // Compute the hash from the byte array.
        return SHA256.HashData(_rawData);
    }

    private static byte[] XorByteArrays(byte[] _array1, byte[] _array2)
    {
        if (_array1.Length != _array2.Length)
        {
            throw new ArgumentException("Both byte arrays must have the same length.");
        }

        byte[] result = new byte[_array1.Length];
        for (int i = 0; i < _array1.Length; i++)
        {
            result[i] = (byte)(_array1[i] ^ _array2[i]);
        }
        return result;
    }
}