
using System.Text;

namespace luminary.util;

public static class Bytes
{
    public static byte[]? ToUtf8Bytes(this string? input)
    {
        if (input == null)
        {
            return null;
        }
        return Encoding.UTF8.GetBytes(input);
    }

    public static string? FromUtf8Bytes(this byte[]? input)
    {
        if (input == null)
        {
            return null;
        }
        return Encoding.UTF8.GetString(input);
    }
}