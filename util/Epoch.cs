namespace luminary.util;

public static class Epoch
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    /// <remarks>
    /// WARNING: Glancing at the formula used in DateTime, I don't think the epoch is accurate.
    ///     There are other leap factors in play other than days in a year~
    /// </remarks
    public static long GetMillisecondsSinceUnixEpoch(DateTimeOffset dateTime)
    {
        return (long)Math.Round((dateTime - DateTimeOffset.UnixEpoch).TotalMilliseconds,0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// WARNING: Glancing at the formula used in DateTime, I don't think the epoch is accurate.
    ///     There are other leap factors in play other than days in a year~
    /// </remarks
    public static long GetMillisecondsSinceUnixEpoch()
    {
        return GetMillisecondsSinceUnixEpoch(DateTime.Now);
    }
}