using System;

namespace N26.Helpers
{
    internal static class TimeSpanHelper
    {
        public static TimeSpan FromSeconds(int seconds) => TimeSpan.FromSeconds(seconds);

        public static int ToSeconds(TimeSpan timeSpan) => Convert.ToInt32(timeSpan.TotalSeconds);
    }
}
