using System;


namespace luminary.functions;

public static class DateTimeHelper
{
    public const string DATE_TIME_OFFSET_FORMAT = "yyyy-MM-dd'T'HH:mm:sszzz";

    public static long? GetValueMillisecondsSinceEpoch(DateOnly? NullableValue)
    {
        if(NullableValue == null)
        {
            return null;
        }

        return GetValueMillisecondsSinceEpoch(new DateTime(NullableValue.Value, new TimeOnly()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    /// <remarks>
    /// WARNING: Glancing at the formula used in DateTime, I don't think the epoch is accurate.
    ///     There are other leap factors in play other than days in a year~
    /// </remarks
    public static long GetValueMillisecondsSinceEpoch(DateTime dateTime)
    {
        return (long)Math.Round((dateTime - DateTime.UnixEpoch).TotalMilliseconds,0);
    }
}