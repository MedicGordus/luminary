using System.Numerics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace luminary.mapping.functions;

public static class UnitHelper
{
    // Pattern: # unit
    //  Number followed by one or more spaces, then a string of characters (no spaces, etc)
    public const string UNIT_PATTERN = @"^(\d+)\s*(°?[a-zA-Z]+)$";

    public static (int, string) ParseStringToIntegerUnits(string? input, string unitPattern = UNIT_PATTERN)
    {
        var _match = UnitMatch(input, unitPattern);

        if (_match.Success)
        {
            if (int.TryParse(_match.Groups[1].Value, out int number))
            {
                return (number, _match.Groups[2].Value);
            }
        }

        throw new ArgumentException("Input string does not match the expected format.", nameof(input));
    }

    public static (double, string) ParseStringToDoubleUnits(string? input, string unitPattern = UNIT_PATTERN)
    {
        var _match = UnitMatch(input, unitPattern);

        if (_match.Success)
        {
            if (double.TryParse(_match.Groups[1].Value, out double number))
            {
                return (number, _match.Groups[2].Value);
            }
        }

        throw new ArgumentException("Input string does not match the expected format.", nameof(input));
    }

    public static (decimal, string) ParseStringToDecimalUnits(string? input, string unitPattern = UNIT_PATTERN)
    {
        var _match = UnitMatch(input, unitPattern);

        if (_match.Success)
        {
            if (decimal.TryParse(_match.Groups[1].Value, out decimal number))
            {
                return (number, _match.Groups[2].Value);
            }
        }

        throw new ArgumentException("Input string does not match the expected format.", nameof(input));
    }

    public static (BigInteger, string) ParseStringToBigIntegerUnits(string? input, string unitPattern = UNIT_PATTERN)
    {
        var _match = UnitMatch(input, unitPattern);

        if (_match.Success)
        {
            if (BigInteger.TryParse(_match.Groups[1].Value, out BigInteger number))
            {
                return (number, _match.Groups[2].Value);
            }
        }

        throw new ArgumentException("Input string does not match the expected format.", nameof(input));
    }

    private static Match UnitMatch(string? input, string unitPattern)
    {
        if(input == null)
        {
            throw new ArgumentNullException("Cannot parse null to value with units.");
        }


        // Regular expression to match one or more digits followed by (optional) spaces, then any non-digit characters
        return Regex.Match(input, unitPattern);
    }
}