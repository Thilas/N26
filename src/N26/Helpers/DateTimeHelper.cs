using System;

namespace N26.Helpers
{
    internal static class DateTimeHelper
    {
        public static readonly DateTime JsReferenceDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime? FromJsDate(long? milliseconds)
        {
            if (!milliseconds.HasValue) return null;
            return JsReferenceDate.AddMilliseconds(milliseconds.Value);
        }

        public static long? ToJsDate(DateTime? dateTime)
        {
            if (!dateTime.HasValue) return null;
            return Convert.ToInt64((dateTime.Value - JsReferenceDate).TotalMilliseconds);
        }
    }
}
