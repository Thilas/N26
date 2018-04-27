using System;
using JetBrains.Annotations;

namespace N26.Utilities
{
    internal static class DateTimeUtils
    {
        [NotNull]
        public static readonly DateTime N26ReferenceDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromN26DateTime(long milliseconds)
        {
            var utcDateTime = N26ReferenceDateTime.AddMilliseconds(milliseconds);
            return utcDateTime;
        }

        public static long ToN26DateTime(DateTime dateTime)
        {
            var utcDateTime = dateTime.ToUniversalTime();
            var milliseconds = (long)(utcDateTime - N26ReferenceDateTime).TotalMilliseconds;
            return milliseconds;
        }
    }
}
