using System.Numerics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace luminary.mapping.functions;

public static class UnitHelper
{
    // Pattern: # unit
    //  Number followed by one or more spaces, then a string of characters (no spaces, etc)
    public const string UNIT_PATTERN = @"^(\d+)\s*(°?[a-zA-Z]+)$";

    public static (int, string) ParseStringToIntegerUnits(string? _input, string _unitPattern = UNIT_PATTERN)
    {
        var match = UnitMatch(_input, _unitPattern);

        if (match.Success)
        {
            if (int.TryParse(match.Groups[1].Value, out int number))
            {
                return (number, match.Groups[2].Value);
            }
        }

        throw new ArgumentException("Input string does not match the expected format.", nameof(_input));
    }

    public static (double, string) ParseStringToDoubleUnits(string? _input, string _unitPattern = UNIT_PATTERN)
    {
        var match = UnitMatch(_input, _unitPattern);

        if (match.Success)
        {
            if (double.TryParse(match.Groups[1].Value, out double number))
            {
                return (number, match.Groups[2].Value);
            }
        }

        throw new ArgumentException("Input string does not match the expected format.", nameof(_input));
    }

    public static (decimal, string) ParseStringToDecimalUnits(string? _input, string _unitPattern = UNIT_PATTERN)
    {
        var match = UnitMatch(_input, _unitPattern);

        if (match.Success)
        {
            if (decimal.TryParse(match.Groups[1].Value, out decimal number))
            {
                return (number, match.Groups[2].Value);
            }
        }

        throw new ArgumentException("Input string does not match the expected format.", nameof(_input));
    }

    public static (BigInteger, string) ParseStringToBigIntegerUnits(string? _input, string _unitPattern = UNIT_PATTERN)
    {
        var match = UnitMatch(_input, _unitPattern);

        if (match.Success)
        {
            if (BigInteger.TryParse(match.Groups[1].Value, out BigInteger number))
            {
                return (number, match.Groups[2].Value);
            }
        }

        throw new ArgumentException("Input string does not match the expected format.", nameof(_input));
    }

    private static Match UnitMatch(string? _input, string _unitPattern)
    {
        if (_input == null)
        {
            throw new ArgumentNullException("Cannot parse null to value with units.");
        }


        // Regular expression to match one or more digits followed by (optional) spaces, then any non-digit characters
        return Regex.Match(_input, _unitPattern);
    }
}