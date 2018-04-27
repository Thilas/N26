using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core.Activators.Reflection;
using JetBrains.Annotations;
using N26.Utilities;

namespace N26.IoC
{
    internal sealed class IoCConstructorFinder : IConstructorFinder
    {
        [NotNull, ItemNotNull]
        public ConstructorInfo[] FindConstructors([NotNull] Type type)
        {
            var result = type.DeclaredConstructors()
                .Where(c => c.IsPublic || c.GetCustomAttribute<IoCConstructorAttribute>() != null);
            return result.ToArray();
        }
    }
}
