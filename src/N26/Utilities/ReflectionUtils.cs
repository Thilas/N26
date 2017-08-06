using System;
using System.Linq.Expressions;
using System.Reflection;

namespace N26.Utilities
{
    internal static class ReflectionUtils
    {
        public static MethodInfo GetMethodInfo(Expression<Action> expression)
            => GetMethodInfo((LambdaExpression)expression);

        public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
            => GetMethodInfo((LambdaExpression)expression);

        public static MethodInfo GetMethodInfo<T, TResult>(Expression<Func<T, TResult>> expression)
            => GetMethodInfo((LambdaExpression)expression);

        private static MethodInfo GetMethodInfo(LambdaExpression expression)
        {
            Guard.IsNotNull(expression, nameof(expression));
            Guard.IsAssignableTo<MethodCallExpression>(expression.Body, nameof(expression));
            var outermostExpression = (MethodCallExpression)expression.Body;
            return outermostExpression.Method;
        }

        public static bool IsNullable(Type type)
        {
            Guard.IsNotNull(type, nameof(type));
            var typeInfo = type.GetTypeInfo();
            return !typeInfo.IsValueType || typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

    }
}
