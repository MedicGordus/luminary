

using System.Text;
using System.Text.RegularExpressions;

namespace luminary.util;

/// <summary>
/// This is a helper class to assist with ISO 8601 durations since dotnet doesn't have a native library for it.
/// 
/// Note that because of leap minutes, etc. certain functionality is intentionally not included.
/// </summary>
public class Duration
{

    /// <summary>
    /// Used to indicate what pieces this duration contains.
    /// 
    /// Examples:
    ///     Days = Date portion
    ///     Seconds = Time portion
    /// </summary>
    [Flags]
    public enum DurationParts : int
    {
        Null = 0,
        ContainsDatePortion = 1,
        ContainstTimePortion = 2
    }

    /// <summary>
    /// Regex helper for parsing durations.
    /// </summary>
    public const string ISO_8601_DURATION_REGEX = @"^P((?<years>\d+(\.\d+)?)Y)?((?<months>\d+(\.\d+)?)M)?((?<weeks>\d+(\.\d+)?)W)?((?<days>\d+(\.\d+)?)D)?(T((?<hours>\d+(\.\d+)?)H)?((?<minutes>\d+(\.\d+)?)M)?((?<seconds>\d+(\.\d+)?)S)?)?$";



    /// <summary>
    /// How many nanoseconds are in a second lol.
    /// </summary>
    public const long NANOSECONDS_PER_SECOND = TimeSpan.TicksPerSecond * TimeSpan.NanosecondsPerTick;

    public double? Years { get; private set; }
    public double? Months { get; private set; }
    public double? Weeks { get; private set; }
    public double? Days { get; private set; }
    public double? Hours { get; private set; }
    public double? Minutes { get; private set; }
    public long? Seconds { get; private set; }
    public long? Nanoseconds { get; private set; }

    public Duration(double? _years = null, double? _months = null, double? _weeks = null, double? _days = null, double? _hours = null, double? _minutes = null, long? _seconds = null, long? _nanoseconds = null)
    {
        Years = _years;
        Months = _months;
        Weeks = _weeks;
        Days = _days;
        Hours = _hours;
        Minutes = _minutes;
        Seconds = _seconds;
        Nanoseconds = _nanoseconds;
    }

    // Add two durations
    public static Duration operator +(Duration _left, Duration _right)
    {
        var output = _left.Clone();

        output.AddYears(_right.Years);
        output.AddMonths(_right.Months);
        output.AddWeeks(_right.Weeks);
        output.AddDays(_right.Days);
        output.AddHours(_right.Hours);
        output.AddMinutes(_right.Minutes);
        output.AddSeconds(_right.Seconds);
        output.AddNanoseconds(_right.Nanoseconds);

        return output;
    }

    // Subtract one duration from another
    public static Duration operator -(Duration _left, Duration _right)
    {
        var output = _left.Clone();

        output.AddYears(_right.Years == null ? null : -_right.Years);
        output.AddMonths(_right.Months == null ? null : -_right.Months);
        output.AddWeeks(_right.Weeks == null ? null : -_right.Weeks);
        output.AddDays(_right.Days == null ? null : -_right.Days);
        output.AddHours(_right.Hours == null ? null : -_right.Hours);
        output.AddMinutes(_right.Minutes == null ? null : -_right.Minutes);
        output.AddSeconds(_right.Seconds == null ? null : -_right.Seconds);
        output.AddNanoseconds(_right.Nanoseconds == null ? null : -_right.Nanoseconds);

        return output;
    }

    /// <summary>
    /// Deep copies this instance into a new instance and returns the new instance.
    /// </summary>
    /// <returns>A deep copied instance of this one.</returns>
    public Duration Clone()
    {
        return new Duration(Years, Months, Weeks, Days, Hours, Minutes, Seconds, Nanoseconds);
    }

    #region "base additions(reusable for subtractions)"

    public void AddYears(double? _years)
    {
        if (_years != null)
        {
            Years = (Years ?? 0) + _years;
        }
    }

    public void AddMonths(double? _months)
    {
        if (_months != null)
        {
            Months = (Months ?? 0) + _months;
        }
    }

    public void AddWeeks(double? _weeks)
    {
        if (_weeks != null)
        {
            Weeks = (Weeks ?? 0) + _weeks;
        }
    }

    public void AddDays(double? _days)
    {
        if (_days != null)
        {
            Days = (Days ?? 0) + _days;
        }
    }

    public void AddHours(double? _hours)
    {
        if (_hours != null)
        {
            Hours = (Hours ?? 0) + _hours;
        }
    }

    public void AddMinutes(double? _minutes)
    {
        if (_minutes != null)
        {
            Minutes = (Minutes ?? 0) + _minutes;
        }
    }

    public void AddSeconds(long? _seconds)
    {
        if (_seconds != null)
        {
            Seconds = (Seconds ?? 0) + _seconds;
        }
    }

    public void AddNanoseconds(long? _nanoseconds)
    {
        if (_nanoseconds != null)
        {
            Nanoseconds = (Nanoseconds ?? 0) + _nanoseconds;
        }
    }

    #endregion

    #region "operators"
    protected static bool AllValuesEqual(Duration _left, Duration _right)
    {
        bool yearsMatch = _left.Years == _right.Years;
        bool monthsMatch = _left.Months == _right.Months;
        bool weeksMatch = _left.Weeks == _right.Weeks;
        bool daysMatch = _left.Days == _right.Days;
        bool hoursMatch = _left.Hours == _right.Hours;
        bool minutesMatch = _left.Minutes == _right.Minutes;
        bool secondsMatch = _left.Seconds == _right.Seconds;
        bool nanosecondsMatch = _left.Nanoseconds == _right.Nanoseconds;

        return yearsMatch & monthsMatch & weeksMatch & daysMatch & hoursMatch & minutesMatch & secondsMatch & nanosecondsMatch;
    }

    public static bool operator ==(Duration? _left, Duration? _right)
    {
        if (_left == null)
        {
            if (_right == null)
            {
                return true;
            }

            return false;
        }
        else if (_right == null)
        {
            return false;
        }

        // performs a check if both right and left only have one parameter set
        if (DurationsSingleValueComparable(_left, _right, out ComparableResult difference))
        {
            return difference == ComparableResult.LeftEqualToRight;
        }

        if (!DurationsComparable(_left, _right))
        {
            throw new ArgumentException("Cannot compare the two durations with their designated field values since leap years and leap seconds exist.");
        }

        // if by random chance, all fields are equal, we can return that they are equal
        if (AllValuesEqual(_left, _right))
        {
            return true;
        }

        // at this point, the left and right have:
        //  Seconds
        //  Seconds and Nanoseconds
        //  Nanoseconds
        //  Nothing
        (var leftNanoseconds, var rightNanoseconds) = RetrieveSecondsAndNanosecondsAsNanoseconds(_left, _right);

        return leftNanoseconds == rightNanoseconds;
    }

    public static bool operator !=(Duration? _left, Duration? _right)
    {
        if (_left == null)
        {
            if (_right == null)
            {
                return false;
            }

            return true;
        }
        else if (_right == null)
        {
            return true;
        }

        // performs a check if both right and left only have one parameter set
        if (DurationsSingleValueComparable(_left, _right, out ComparableResult difference))
        {
            return difference != ComparableResult.LeftEqualToRight;
        }

        if (!DurationsComparable(_left, _right))
        {
            throw new ArgumentException("Cannot compare the two durations with their designated field values since leap years and leap seconds exist.");
        }

        // at this point, the left and right have:
        //  Seconds
        //  Seconds and Nanoseconds
        //  Nanoseconds
        //  Nothing
        (var leftNanoseconds, var rightNanoseconds) = RetrieveSecondsAndNanosecondsAsNanoseconds(_left, _right);

        return leftNanoseconds != rightNanoseconds;
    }

    protected enum ComparableResult : int
    {
        z_error = 0,
        LeftLessThanRight = 1,
        LeftEqualToRight = 2,
        LeftGreaterThanRight = 3,
    }

    /// <summary>
    /// This is a specialized function that checks if _left and _right both
    ///     onlyhave one parameter, then returns the difference via a
    ///     ComparableResult.
    /// </summary>
    /// <param name="_left">The left parameter to check.</param>
    /// <param name="_right">The right parameter to check.</param>
    /// <param name="_difference">See ComparableResult enumerator.</param>
    /// <returns>True of there is only one value to compare on _left and _right, otherwise false.</returns>
    protected static bool DurationsSingleValueComparable(Duration _left, Duration _right, out ComparableResult _difference)
    {
        // first ensure that the left nulls are the same as the right nulls
        if (
            !(
                YearsComparible(_left, _right)
            &
                MonthsComparible(_left, _right)
            &
                WeeksComparible(_left, _right)
            &
                DaysComparible(_left, _right)
            &
                HoursComparible(_left, _right)
            &
                MinutesComparible(_left, _right)
            &
                SecondsComparible(_left, _right)
            &
                NanosecondsComparible(_left, _right)
            )
        )
        {
            // one of the parameters was null on left or right and not on the other
            _difference = ComparableResult.z_error;
            return false;
        }

        //// at this point we know left and right null matches, so count and check the ones on the left
        //
        bool yearsNull = _left.Years == null;
        bool monthsNull = _left.Months == null;
        bool weeksNull = _left.Weeks == null;
        bool daysNull = _left.Days == null;
        bool hoursNull = _left.Hours == null;
        bool minutesNull = _left.Minutes == null;
        bool secondsNull = _left.Seconds == null;
        bool nanosecondsNull = _left.Nanoseconds == null;
        //
        int nullCount =
                (yearsNull ? 1 : 0)
            +
                (monthsNull ? 1 : 0)
            +
                (weeksNull ? 1 : 0)
            +
                (daysNull ? 1 : 0)
            +
                (hoursNull ? 1 : 0)
            +
                (minutesNull ? 1 : 0)
            +
                (secondsNull ? 1 : 0)
            +
                (nanosecondsNull ? 1 : 0)
        ;
        //
        if (nullCount != 7)
        {
            // more than one parameter is not null, so we cannot compare a single one of them
            _difference = ComparableResult.z_error;
            return false;
        }
        //
        ////

        // now we return the comparible result based on which one is not null
        if (yearsNull == false)
        {
            _difference = _left.Years < _right.Years ? ComparableResult.LeftLessThanRight : (_left.Years == _right.Years ? ComparableResult.LeftEqualToRight : ComparableResult.LeftGreaterThanRight);
        }
        else if (monthsNull == false)
        {
            _difference = _left.Months < _right.Months ? ComparableResult.LeftLessThanRight : (_left.Years == _right.Years ? ComparableResult.LeftEqualToRight : ComparableResult.LeftGreaterThanRight);
        }
        else if (weeksNull == false)
        {
            _difference = _left.Weeks < _right.Weeks ? ComparableResult.LeftLessThanRight : (_left.Weeks == _right.Weeks ? ComparableResult.LeftEqualToRight : ComparableResult.LeftGreaterThanRight);
        }
        else if (daysNull == false)
        {
            _difference = _left.Days < _right.Days ? ComparableResult.LeftLessThanRight : (_left.Days == _right.Days ? ComparableResult.LeftEqualToRight : ComparableResult.LeftGreaterThanRight);
        }
        else if (hoursNull == false)
        {
            _difference = _left.Hours < _right.Hours ? ComparableResult.LeftLessThanRight : (_left.Hours == _right.Hours ? ComparableResult.LeftEqualToRight : ComparableResult.LeftGreaterThanRight);
        }
        else if (minutesNull == false)
        {
            _difference = _left.Minutes < _right.Minutes ? ComparableResult.LeftLessThanRight : (_left.Minutes == _right.Minutes ? ComparableResult.LeftEqualToRight : ComparableResult.LeftGreaterThanRight);
        }
        else if (secondsNull == false)
        {
            _difference = _left.Seconds < _right.Seconds ? ComparableResult.LeftLessThanRight : (_left.Seconds == _right.Seconds ? ComparableResult.LeftEqualToRight : ComparableResult.LeftGreaterThanRight);
        }
        else if (nanosecondsNull == false)
        {
            _difference = _left.Nanoseconds < _right.Nanoseconds ? ComparableResult.LeftLessThanRight : (_left.Nanoseconds == _right.Nanoseconds ? ComparableResult.LeftEqualToRight : ComparableResult.LeftGreaterThanRight);
        }
        else
        {
            throw new Exception("It is logically impossible to get to this error.");
        }

        return true;
    }

    public static bool operator >(Duration? _left, Duration? _right)
    {
        if (_left == null || _right == null)
        {
            throw new ArgumentException("Cannot compare the two durations when one or both are null.");
        }

        // performs a check if both right and left only have one parameter set
        if (DurationsSingleValueComparable(_left, _right, out ComparableResult difference))
        {
            return difference == ComparableResult.LeftGreaterThanRight;
        }

        if (!DurationsComparable(_left, _right))
        {
            throw new ArgumentException("Cannot compare the two durations with their designated field values since leap years and leap seconds exist.");
        }

        // at this point, the left and right have:
        //  Seconds
        //  Seconds and Nanoseconds
        //  Nanoseconds
        //  Nothing
        (var leftNanoseconds, var rightNanoseconds) = RetrieveSecondsAndNanosecondsAsNanoseconds(_left, _right);

        return leftNanoseconds > rightNanoseconds;
    }

    public static bool operator >=(Duration? _left, Duration? _right)
    {
        if (_left == null || _right == null)
        {
            throw new ArgumentException("Cannot compare the two durations when one or both are null.");
        }

        // performs a check if both right and left only have one parameter set
        if (DurationsSingleValueComparable(_left, _right, out ComparableResult difference))
        {
            return (difference == ComparableResult.LeftGreaterThanRight) || (difference == ComparableResult.LeftEqualToRight);
        }

        if (!DurationsComparable(_left, _right))
        {
            throw new ArgumentException("Cannot compare the two durations with their designated field values since leap years and leap seconds exist.");
        }

        // at this point, the left and right have:
        //  Seconds
        //  Seconds and Nanoseconds
        //  Nanoseconds
        //  Nothing
        (var leftNanoseconds, var rightNanoseconds) = RetrieveSecondsAndNanosecondsAsNanoseconds(_left, _right);

        return leftNanoseconds >= rightNanoseconds;
    }

    public static bool operator <(Duration? _left, Duration? _right)
    {
        if (_left == null || _right == null)
        {
            throw new ArgumentException("Cannot compare the two durations when one or both are null.");
        }

        // performs a check if both right and left only have one parameter set
        if (DurationsSingleValueComparable(_left, _right, out ComparableResult difference))
        {
            return difference == ComparableResult.LeftLessThanRight;
        }

        if (!DurationsComparable(_left, _right))
        {
            throw new ArgumentException("Cannot compare the two durations with their designated field values since leap years and leap seconds exist.");
        }

        // at this point, the left and right have:
        //  Seconds
        //  Seconds and Nanoseconds
        //  Nanoseconds
        //  Nothing
        (var leftNanoseconds, var rightNanoseconds) = RetrieveSecondsAndNanosecondsAsNanoseconds(_left, _right);

        return leftNanoseconds < rightNanoseconds;
    }

    public static bool operator <=(Duration? _left, Duration? _right)
    {
        if (_left == null || _right == null)
        {
            throw new ArgumentException("Cannot compare the two durations when one or both are null.");
        }

        // performs a check if both right and left only have one parameter set
        if (DurationsSingleValueComparable(_left, _right, out ComparableResult difference))
        {
            return (difference == ComparableResult.LeftLessThanRight) || (difference == ComparableResult.LeftEqualToRight);
        }

        if (!DurationsComparable(_left, _right))
        {
            throw new ArgumentException("Cannot compare the two durations with their designated field values since leap years and leap seconds exist.");
        }

        // at this point, the left and right have:
        //  Seconds
        //  Seconds and Nanoseconds
        //  Nanoseconds
        //  Nothing
        (var leftNanoseconds, var rightNanoseconds) = RetrieveSecondsAndNanosecondsAsNanoseconds(_left, _right);

        return leftNanoseconds <= rightNanoseconds;
    }

    protected static (long, long) RetrieveSecondsAndNanosecondsAsNanoseconds(Duration _left, Duration _right)
    {
        long leftNanoseconds =
                (_left.Seconds == null ? 0 : _left.Seconds.Value * NANOSECONDS_PER_SECOND)
            +
                (_left.Nanoseconds == null ? 0 : _left.Nanoseconds.Value);


        long rightNanoseconds =
                (_right.Seconds == null ? 0 : _right.Seconds.Value * NANOSECONDS_PER_SECOND)
            +
                (_right.Nanoseconds == null ? 0 : _right.Nanoseconds.Value);

        return (leftNanoseconds, rightNanoseconds);
    }
    #endregion

    #region "comparible checks"

    public static bool DurationsComparable(Duration _left, Duration _right)
    {
        // make sure both sides have all units the other does
        if (
                YearsComparible(_left, _right)
            &
                MonthsComparible(_left, _right)
            &
                WeeksComparible(_left, _right)
            &
                DaysComparible(_left, _right)
            &
                HoursComparible(_left, _right)
            &
                MinutesComparible(_left, _right)
            &
                SecondsComparible(_left, _right)
            &
                NanosecondsComparible(_left, _right)
        )
        {
            // do checks on the conversions between units now
            //
            //  Minutes are not always 60 seconds, so all units above this can vary in size
            //  
            return _left.Years == null & _left.Months == null & _left.Weeks == null & _left.Days == null & _left.Hours == null & _left.Minutes == null;
        }
        return false;
    }

    public static bool YearsComparible(Duration _left, Duration _right)
    {
        return (_left.Years == null & _right.Years == null) || (_left.Years != null & _right.Years != null);
    }

    public static bool MonthsComparible(Duration _left, Duration _right)
    {
        return (_left.Months == null & _right.Months == null) || (_left.Months != null & _right.Months != null);
    }

    public static bool WeeksComparible(Duration _left, Duration _right)
    {
        return (_left.Weeks == null & _right.Weeks == null) || (_left.Weeks != null & _right.Weeks != null);
    }

    public static bool DaysComparible(Duration _left, Duration _right)
    {
        return (_left.Days == null & _right.Days == null) || (_left.Days != null & _right.Days != null);
    }

    public static bool HoursComparible(Duration _left, Duration _right)
    {
        return (_left.Hours == null & _right.Hours == null) || (_left.Hours != null & _right.Hours != null);
    }

    public static bool MinutesComparible(Duration _left, Duration _right)
    {
        return (_left.Minutes == null & _right.Minutes == null) || (_left.Minutes != null & _right.Minutes != null);
    }

    public static bool SecondsComparible(Duration _left, Duration _right)
    {
        return (_left.Seconds == null & _right.Seconds == null) || (_left.Seconds != null & _right.Seconds != null);
    }

    public static bool NanosecondsComparible(Duration _left, Duration _right)
    {
        return (_left.Nanoseconds == null & _right.Nanoseconds == null) || (_left.Nanoseconds != null & _right.Nanoseconds != null);
    }

    #endregion

    /// <summary>
    /// This uses ISO 8601 to parse a duration.
    /// </summary>
    /// <param name="_input"></param>
    /// <returns></returns>
    public static Duration Parse(string? _input)
    {
        if (_input == null)
        {
            return new Duration();
        }

        var match = Regex.Match(_input, ISO_8601_DURATION_REGEX);
        if (!match.Success)
        {
            throw new Exception(
                string.Format(
                    "Parse of duration, '{0}' failed to match regex '{1}'.",
                    _input,
                    ISO_8601_DURATION_REGEX
                )
            );
        }

        double? years = match.Groups["years"].Success ? double.Parse(match.Groups["years"].Value) : null;
        double? months = match.Groups["months"].Success ? double.Parse(match.Groups["months"].Value) : null;
        double? weeks = match.Groups["weeks"].Success ? double.Parse(match.Groups["weeks"].Value) : null;
        double? days = match.Groups["days"].Success ? double.Parse(match.Groups["days"].Value) : null;
        double? hours = match.Groups["hours"].Success ? double.Parse(match.Groups["hours"].Value) : null;
        double? minutes = match.Groups["minutes"].Success ? double.Parse(match.Groups["minutes"].Value) : null;
        string? secondsString = match.Groups["seconds"].Success ? match.Groups["seconds"].Value : null;

        long? seconds = null;
        long? nanoSeconds = null;

        if (secondsString != null && secondsString != "")
        {
            // check if there are fractional seconds
            var decimalPosition = secondsString.IndexOf('.');
            if (decimalPosition != -1)
            {

                // capture the whole seconds portion
                seconds = long.Parse(secondsString[0..decimalPosition]);

                //// capture the fractional seconds portion
                //
                // make sure we only capture nanoseconds (there could be less or more chars, but we fix it to nanoseconds)
                var nanoSecondsString = secondsString[(decimalPosition + 1)..].PadRight(9, '0');
                if (nanoSecondsString.Length > 9)
                {
                    nanoSecondsString = nanoSecondsString[..9];
                }
                //
                nanoSeconds = long.Parse(nanoSecondsString);
                //
                ////

            }
            else
            {
                // no fractional seconds

                seconds = long.Parse(secondsString);
            }
        }

        return new Duration(years, months, weeks, days, hours, minutes, seconds, nanoSeconds);
    }

    public static bool TryParse(string? _input, out Duration? _output)
    {
        try
        {
            _output = Parse(_input);

            return true;
        }
        catch
        {
            _output = null;
            return false;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append('P');
        if (Years.HasValue && Years.Value != 0d)
        {
            sb.Append(Years.Value);
            sb.Append('Y');
        }
        if (Months.HasValue && Months.Value != 0d)
        {
            sb.Append(Months.Value);
            sb.Append('M');
        }
        if (Weeks.HasValue && Weeks.Value != 0d)
        {
            sb.Append(Weeks.Value);
            sb.Append('W');
        }
        if (Days.HasValue && Days.Value != 0d)
        {
            sb.Append(Days.Value);
            sb.Append('D');
        }

        if (Hours.HasValue | Minutes.HasValue | Seconds.HasValue | Nanoseconds.HasValue)
        {
            sb.Append('T');
            if (Hours.HasValue && Hours.Value != 0d)
            {
                sb.Append(Hours.Value);
                sb.Append('H');
            }
            if (Minutes.HasValue && Minutes.Value != 0d)
            {
                sb.Append(Minutes.Value);
                sb.Append('M');
            }

            // if we have whole or partial seconds we need to add the "S" part
            if ((Seconds.HasValue && Seconds.Value != 0) | (Nanoseconds.HasValue && Nanoseconds.Value != 0))
            {
                if (Seconds.HasValue)
                {
                    sb.Append(Seconds.Value);
                }
                else
                {
                    // seconds is null: to get here nanoseconds has to not be null, so we add '0'
                    sb.Append('0');
                }

                if (Nanoseconds.HasValue && Nanoseconds.Value != 0)
                {
                    sb.Append('.');
                    var nanoSecondsString = Nanoseconds.Value.ToString().PadLeft(9, '0');
                    sb.Append(nanoSecondsString);
                }
                sb.Append('S');
            }
        }

        return sb.ToString();
    }

    #region "these are literally here purely to suppress compiler errors lol"

    public override bool Equals(object? _obj)
    {
        if (ReferenceEquals(this, _obj))
        {
            return true;
        }

        if (ReferenceEquals(_obj, null))
        {
            return false;
        }

        return base.Equals(_obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion

    /// <summary>
    /// This returns if Date and/or Time have active fields in this Duration object.
    /// </summary>
    /// <returns>
    /// One ore more flags of DurationParts, indicating if Date and/or Time parts are in this Duration object.
    /// </returns>
    public DurationParts GetParts()
    {
        bool containsDateParts = true;
        if ((Years == null) && (Months == null) && (Weeks == null) && (Days == null))
        {
            containsDateParts = false;
        }

        bool containsTimeParts = true;
        if ((Hours == null) && (Minutes == null) && (Seconds == null) && (Nanoseconds == null))
        {
            containsTimeParts = false;
        }

        return
                (containsDateParts ? DurationParts.ContainsDatePortion : DurationParts.Null)
            |
                (containsTimeParts ? DurationParts.ContainsDatePortion : DurationParts.Null)
            ;
    }

    public DateTimeOffset AddToDateTimeOffset(DateTimeOffset _dto)
    {
        return ApplyAdditionOrSubtractionFromDateTimeOffset(_dto, 1);
    }

    public DateTimeOffset SubtractFromDateTimeOffset(DateTimeOffset _dto)
    {
        return ApplyAdditionOrSubtractionFromDateTimeOffset(_dto, -1);
    }

    protected DateTimeOffset ApplyAdditionOrSubtractionFromDateTimeOffset(DateTimeOffset _dto, int _multiplier)
    {
        var output = new DateTimeOffset(
            _dto.Year,
            _dto.Month,
            _dto.Day,
            _dto.Hour,
            _dto.Minute,
            _dto.Second,
            _dto.Offset
        );

        if (Years != null)
        {
            output.AddYears(_multiplier * (int)Years);
        }
        if (Months != null)
        {
            output.AddMonths(_multiplier * (int)Months);
        }
        if (Weeks != null)
        {
            output.AddDays(_multiplier * (int)(Weeks * 7d));
        }
        if (Days != null)
        {
            output.AddDays(_multiplier * (int)Days);
        }
        if (Hours != null)
        {
            output.AddHours(_multiplier * (int)Hours);
        }
        if (Minutes != null)
        {
            output.AddMinutes(_multiplier * (int)Minutes);
        }
        if (Seconds != null)
        {
            output.AddSeconds(_multiplier * (int)Seconds);
        }
        if (Nanoseconds != null)
        {
            output.AddTicks(_multiplier * ((long)(Nanoseconds / NANOSECONDS_PER_SECOND)));
        }

        return output;
    }

    public TimeOnly AddToTimeOnly(TimeOnly _to)
    {
        return ApplyAdditionOrSubtractionFromTimeOnly(_to, 1);
    }

    public TimeOnly SubtractFromTimeOnly(TimeOnly _to)
    {
        return ApplyAdditionOrSubtractionFromTimeOnly(_to, -1);
    }

    protected TimeOnly ApplyAdditionOrSubtractionFromTimeOnly(TimeOnly _to, int _multiplier)
    {
        /* inefficient string code
        if(Nanoseconds != null)
        {
            if(Nanoseconds > 999999999 || Nanoseconds < 0)
            {
                throw new InvalidOperationException("Nanoseconds was so high that it was greater than one second, or it was negative. Unable to execute the requested command.");
            }
        }

        string? _nanosecondsString = Nanoseconds?.ToString();

        string? _microsecondsCropped = _nanosecondsString == null ? null : _nanosecondsString.PadLeft(9, '0')[0..6];

        int _milliseconds = _microsecondsCropped == null ? 0 : int.Parse(_microsecondsCropped[0..3]);
        int _microseconds = _microsecondsCropped == null ? 0 : int.Parse(_microsecondsCropped[3..6]);
        */

        // replacement by Grok:
        int milliseconds = 0;
        int microseconds = 0;
        if (Nanoseconds != null)
        {
            if (Nanoseconds > 999999999 || Nanoseconds < 0)
            {
                throw new InvalidOperationException("Nanoseconds was so high that it was greater than one second, or it was negative. Unable to execute the requested command.");
            }

            long nanosecondsValue = Nanoseconds.Value; // Use long to avoid overflow during calculations
            long microsecondsCalc = nanosecondsValue / 1000; // Convert to microseconds (1,000 nanoseconds = 1 microsecond)
            milliseconds = (int)(microsecondsCalc / 1000); // Convert to milliseconds (1,000 microseconds = 1 millisecond)
            microseconds = (int)(microsecondsCalc % 1000); // Remaining microseconds
        }

        return new TimeOnly(
            _to.Hour + (Hours == null ? 0 : (_multiplier * (int)Hours)),
            _to.Minute + (Minutes == null ? 0 : (_multiplier * (int)Minutes)),
            _to.Second + (Seconds == null ? 0 : (_multiplier * (int)Seconds)),
            _to.Millisecond + (_multiplier * milliseconds),
            _to.Microsecond + (_multiplier * microseconds)
        );

    }

    public DateOnly AddToDateOnly(DateOnly _do)
    {
        return ApplyAdditionOrSubtractionFromDateOnly(_do, 1);
    }

    public DateOnly SubtractFromDateOnly(DateOnly _do)
    {
        return ApplyAdditionOrSubtractionFromDateOnly(_do, -1);
    }

    protected DateOnly ApplyAdditionOrSubtractionFromDateOnly(DateOnly _do, int _multiplier)
    {
        return new DateOnly(
            _do.Year,
            _do.Month,
            _do.Day
        )
            .AddYears(Years == null ? 0 : (_multiplier * (int)Years))
            .AddMonths(Months == null ? 0 : (_multiplier * (int)Months))
            .AddDays(Weeks == null ? 0 : (7 * _multiplier * (int)Weeks))
            .AddDays(Days == null ? 0 : (_multiplier * (int)Days))
        ;
    }

    /// <summary>
    /// Converts a time-based duration to a timespan.
    /// 
    /// Very useful for converting to DateTimeOffset Offset portion.
    /// </summary>
    /// <returns>Timespan representation of the duration.</returns>
    public TimeSpan ConvertToTimeSpan()
    {
        if (GetParts().HasFlag(DurationParts.ContainsDatePortion))
        {
            throw new InvalidOperationException("Cannot convert duration to timespan when it contains date portions.");
        }

        int milliseconds = 0;
        int microseconds = 0;
        if (Nanoseconds != null)
        {
            if (Nanoseconds > 999999999 || Nanoseconds < 0)
            {
                throw new InvalidOperationException("Nanoseconds was so high that it was greater than one second, or it was negative. Unable to execute the requested command.");
            }

            long nanosecondsValue = Nanoseconds.Value; // Use long to avoid overflow during calculations
            long microsecondsCalc = nanosecondsValue / 1000; // Convert to microseconds (1,000 nanoseconds = 1 microsecond)
            milliseconds = (int)(microsecondsCalc / 1000); // Convert to milliseconds (1,000 microseconds = 1 millisecond)
            microseconds = (int)(microsecondsCalc % 1000); // Remaining microseconds
        }

        return new TimeSpan(
            0,
            (int)(Hours ?? 0d),
            (int)(Minutes ?? 0d),
            (int)(Seconds ?? 0d),
            milliseconds,
            microseconds
        );
    }
}