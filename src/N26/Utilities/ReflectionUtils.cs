using System;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace N26.Utilities
{
    internal static class ReflectionUtils
    {
        [NotNull]
        public static MethodInfo GetMethodInfo([NotNull] Expression<Action> expression)
            => GetMethodInfo((LambdaExpression)expression);

        [NotNull]
        public static MethodInfo GetMethodInfo<T>([NotNull] Expression<Action<T>> expression)
            => GetMethodInfo((LambdaExpression)expression);

        [NotNull]
        public static MethodInfo GetMethodInfo<T, TResult>([NotNull] Expression<Func<T, TResult>> expression)
            => GetMethodInfo((LambdaExpression)expression);

        private static MethodInfo GetMethodInfo(LambdaExpression expression)
        {
            Guard.IsNotNull(expression, nameof(expression));
            Guard.IsAssignableTo<MethodCallExpression>(expression.Body, nameof(expression));
            var outermostExpression = (MethodCallExpression)expression.Body;
            return outermostExpression.Method;
        }
    }
}
