using System;

namespace N26.Utilities
{
    public static class TimeSpanUtils
    {
        public static TimeSpan FromN26TimeSpan(long seconds)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return timeSpan;
        }

        public static long ToN26TimeSpan(TimeSpan timeSpan)
        {
            var seconds = (long)timeSpan.TotalSeconds;
            return seconds;
        }
    }
}
