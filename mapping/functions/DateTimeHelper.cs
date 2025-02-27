using luminary.util;

using System;
using System.Text.RegularExpressions;


namespace luminary.mapping.functions;

public static class DateTimeHelper
{
    /// <summary>
    /// Used for coming FROM strings to objects.
    /// 
    /// This is ISO 8601 datetime format without fractional seconds - because
    ///     we use ParseExact, you must use regex to optionally break them 
    ///     out and deal with them separately.
    /// </summary>
    public const string DATE_TIME_OFFSET_FORMAT = @"yyyy-MM-dd'T'HH:mm:sszzz";

    /// <summary>
    /// Regex to split a ISO 8601 datetimezone string apart from the fractional seconds.
    /// 
    ///     [Group 1] + [Group 3] = DateTimeZone
    /// 
    ///     [Group 2] = Fractional seconds that need to be padded in the right with
    ///                     zeroes to 7 digits to represent ticks.
    /// </summary>
    public const string DATE_TIME_OFFSET_FRACTIONAL_SECONDS_HELPER = @"^(.+?)(?:\.(\d+))?([+-]\d{2}(?::)\d{2}|Z)?$";


    /// <summary>
    /// This is for going TO strings as it has the fractional seconds on it.
    /// </summary>
    public const string DATE_TIME_OFFSET_TO_STRING_FORMAT = @"yyyy-MM-dd'T'HH:mm:ss.fffffffzzz";

    public static long? GetValueMillisecondsSinceEpoch(DateOnly? _nullableValue)
    {
        if (_nullableValue == null)
        {
            return null;
        }

        return GetValueMillisecondsSinceEpoch(new DateTimeOffset(_nullableValue.Value, new TimeOnly(), new TimeSpan()));
    }

    public static long? GetValueMillisecondsSinceEpoch(DateTimeOffset? _nullableValue)
    {
        if (_nullableValue == null)
        {
            return null;
        }

        return Epoch.GetMillisecondsSinceUnixEpoch(_nullableValue.Value);
    }

    public static bool TrySpecialParseExactDateTimeOffset(string _input, out DateTimeOffset _output)
    {

        //// special code to separate out the fractional seconds
        //
        var match = Regex.Match(_input, DATE_TIME_OFFSET_FRACTIONAL_SECONDS_HELPER);
        if (match.Success)
        {
            // Group 1 will be everything before the fractional part or the whole string if no fractional part exists
            // Group 3 captures the timezone information
            string dateTimeZonePortion = match.Groups[1].Value + (match.Groups[3].Success ? match.Groups[3].Value : "");


            // Group 2 will be the fractional part if it exists, otherwise it's an empty string
            string fractionalSeconds = match.Groups[2].Success ? match.Groups[2].Value.PadRight(7, '0') : "";

            // DateTimeZone deals with ticks (100 nanoseconds), so we can only parse 7 digits
            if (fractionalSeconds.Length > 7)
            {
                fractionalSeconds = fractionalSeconds[..7];
            }

            if (DateTimeOffset.TryParseExact(dateTimeZonePortion, DateTimeHelper.DATE_TIME_OFFSET_FORMAT, null, System.Globalization.DateTimeStyles.None, out _output))
            {
                if (fractionalSeconds != "")
                {
                    // add the ticks, we already made sure it is 7 digits by this time to be exactly ticks
                    _output = _output.AddTicks(int.Parse(fractionalSeconds));
                }

                return true;
            }

        }
        //
        //// 

        _output = new DateTimeOffset();

        return false;
    }
}