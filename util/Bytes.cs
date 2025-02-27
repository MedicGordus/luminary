
using System.Text;

namespace luminary.util;

public static class Bytes
{
    public static byte[]? ToUtf8Bytes(this string? _input)
    {
        if (_input == null)
        {
            return null;
        }
        return Encoding.UTF8.GetBytes(_input);
    }

    public static string? FromUtf8Bytes(this byte[]? _input)
    {
        if (_input == null)
        {
            return null;
        }
        return Encoding.UTF8.GetString(_input);
    }
}