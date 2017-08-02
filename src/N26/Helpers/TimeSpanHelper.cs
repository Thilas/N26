using System;

namespace N26.Helpers
{
    internal static class TimeSpanHelper
    {
        public static TimeSpan? FromSeconds(int? seconds)
        {
            if (!seconds.HasValue) return null;
            return TimeSpan.FromSeconds(seconds.Value);
        }

        public static int? ToSeconds(TimeSpan? timeSpan)
        {
            if (!timeSpan.HasValue) return null;
            return Convert.ToInt32(timeSpan.Value.TotalSeconds);
        }
    }
}
