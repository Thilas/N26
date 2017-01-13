using System;

namespace N26.Helpers
{
    internal static class DateTimeHelper
    {
        private static readonly DateTime JsReferenceDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromJsDate(long milliseconds) => JsReferenceDate.AddMilliseconds(milliseconds);
        public static long ToJsDate(DateTime dateTime) => Convert.ToInt64((dateTime - JsReferenceDate).TotalMilliseconds);
    }
}
