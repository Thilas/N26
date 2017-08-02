using System;
using System.Linq.Expressions;
using System.Reflection;

namespace N26.Helpers
{
    internal static class TypeHelper
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
            var outermostExpression = expression.Body as MethodCallExpression;
            Guard.IsValid(outermostExpression, nameof(expression), v => v != null, "Parameter must consist in a method call only.");
            return outermostExpression.Method;
        }
    }
}
