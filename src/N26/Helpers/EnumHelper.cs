using System;

namespace N26.Helpers
{
    internal static class EnumHelper
    {
        public static TTo? Convert<TFrom, TTo>(TFrom? value)
            where TFrom : struct
            where TTo : struct
        {
            Guard.IsAssignableTo<Enum>(typeof(TFrom), nameof(TFrom));
            Guard.IsAssignableTo<Enum>(typeof(TTo), nameof(TTo));
            if (!value.HasValue) return default(TTo?);
            return (TTo)Enum.ToObject(typeof(TTo), value.Value);
        }
    }
}
