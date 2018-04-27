using System;

namespace N26.IoC
{
    [AttributeUsage(AttributeTargets.Constructor, Inherited = false, AllowMultiple = false)]
    internal sealed class IoCConstructorAttribute : Attribute
    {
    }
}
