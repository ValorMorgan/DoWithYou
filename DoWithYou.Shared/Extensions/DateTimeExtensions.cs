using System;

namespace DoWithYou.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan) => 
            timeSpan == TimeSpan.Zero ? 
                dateTime : 
                dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));

        public static DateTime TruncateToMillisecond(this DateTime dateTime) =>
            Truncate(dateTime, TimeSpan.FromMilliseconds(1));

        public static DateTime TruncateToSecond(this DateTime dateTime) =>
            Truncate(dateTime, TimeSpan.FromSeconds(1));

        public static DateTime TruncateToMinute(this DateTime dateTime) =>
            Truncate(dateTime, TimeSpan.FromMinutes(1));

        public static DateTime TruncateToHour(this DateTime dateTime) =>
            Truncate(dateTime, TimeSpan.FromHours(1));

        public static DateTime TruncateToDay(this DateTime dateTime) =>
            Truncate(dateTime, TimeSpan.FromDays(1));
    }
}
