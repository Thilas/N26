using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using N26.Helpers;

namespace N26.Queryables
{
    internal class TransactionVisitor : ExpressionVisitor
    {
        public string Keyword { get; }
        public DateTime? DateFrom { get; }
        public DateTime? DateTo { get; }
        public int? Category { get; }
        public int? Limit { get; }
        public int? LastId { get; }

        private static readonly Lazy<MethodCallVisitor> _countVisitor = new Lazy<MethodCallVisitor>(() => new CountVisitor());

        private static readonly Lazy<MethodCallVisitor> _predicateVisitor = new Lazy<MethodCallVisitor>(() => new PredicateVisitor(expression => expression.Arguments[0],
                                                                                                                                   expression => expression.Arguments[1]));

        private static readonly Lazy<Dictionary<MethodInfo, Lazy<MethodCallVisitor>>> _methodVisitors = new Lazy<Dictionary<MethodInfo, Lazy<MethodCallVisitor>>>(()
            => new Dictionary<MethodInfo, Lazy<MethodCallVisitor>>()
            {
                { TypeHelper.GetMethodInfo(() => Queryable.All(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.Any(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.Count(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.ElementAt(default(IQueryable<object>), 0)).GetGenericMethodDefinition(), _countVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.ElementAtOrDefault(default(IQueryable<object>), 0)).GetGenericMethodDefinition(), _countVisitor },
                //{ TypeHelper.GetMethodInfo(() => Queryable.First(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                //{ TypeHelper.GetMethodInfo(() => Queryable.FirstOrDefault(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.Last(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.LastOrDefault(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.LongCount(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                //{ TypeHelper.GetMethodInfo(() => Queryable.Single(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                //{ TypeHelper.GetMethodInfo(() => Queryable.SingleOrDefault(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.Take(default(IQueryable<object>), 0)).GetGenericMethodDefinition(), _countVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.TakeWhile(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.TakeWhile(null, default(Expression<Func<object, int, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.Where(null, default(Expression<Func<object, bool>>))).GetGenericMethodDefinition(), _predicateVisitor },
                { TypeHelper.GetMethodInfo(() => Queryable.Where(null, default(Expression<Func<object, int, bool>>))).GetGenericMethodDefinition(), _predicateVisitor }
            });

        public TransactionVisitor(Expression expression)
        {
            Visit(expression);
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            var method = expression.Method;
            if (method.IsGenericMethod) method = method.GetGenericMethodDefinition();
            if (_methodVisitors.Value.TryGetValue(method, out var visitor)) visitor.Value.Visit(this, expression);
            return expression;
        }

        private abstract class MethodCallVisitor : ExpressionVisitor
        {
            public void Visit(TransactionVisitor visitor, MethodCallExpression expression)
            {
                VisitArguments(expression);
                visitor.Visit(GetNextExpression(expression));
            }

            protected abstract Expression GetNextExpression(MethodCallExpression expression);
            protected abstract void VisitArguments(MethodCallExpression expression);
        }

        private class CountVisitor : MethodCallVisitor
        {
            protected override Expression GetNextExpression(MethodCallExpression expression) => throw new NotImplementedException();
            protected override void VisitArguments(MethodCallExpression expression) => throw new NotImplementedException();
        }

        private class PredicateVisitor : MethodCallVisitor
        {
            private readonly Func<MethodCallExpression, Expression> _getNextExpression;
            private readonly Func<MethodCallExpression, Expression> _getPredicateExpression;

            public PredicateVisitor(Func<MethodCallExpression, Expression> getNextExpression,
                                    Func<MethodCallExpression, Expression> getPredicateExpression)
            {
                _getNextExpression = getNextExpression ?? throw new ArgumentNullException(nameof(getNextExpression));
                _getPredicateExpression = getPredicateExpression ?? throw new ArgumentNullException(nameof(getPredicateExpression));
            }

            protected override Expression GetNextExpression(MethodCallExpression expression) => _getNextExpression(expression);
            protected override void VisitArguments(MethodCallExpression expression) => Visit(_getPredicateExpression(expression));

            protected override Expression VisitBinary(BinaryExpression expression)
            {
                //if (expression.NodeType == ExpressionType.Equal)
                //{
                //    if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(expression, typeof(Place), "Name"))
                //    {
                //        _locations.Add(ExpressionTreeHelpers.GetValueFromEqualsExpression(expression, typeof(Place), "Name"));
                //        return expression;
                //    }
                //    else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(expression, typeof(Place), "State"))
                //    {
                //        _locations.Add(ExpressionTreeHelpers.GetValueFromEqualsExpression(expression, typeof(Place), "State"));
                //        return expression;
                //    }
                //    else
                //        return base.VisitBinary(expression);
                //}
                //else
                //    return base.VisitBinary(expression);
                return base.VisitBinary(expression);
            }

            //protected override Expression VisitMethodCall(MethodCallExpression expression)
            //{
            //    if (expression.Method.DeclaringType == typeof(string) && expression.Method.Name == nameof(string.StartsWith))
            //    {
            //        if (ExpressionTreeHelpers.IsSpecificMemberExpression(expression.Object, typeof(Place), "Name") ||
            //        ExpressionTreeHelpers.IsSpecificMemberExpression(expression.Object, typeof(Place), "State"))
            //        {
            //            _locations.Add(ExpressionTreeHelpers.GetValueFromExpression(expression.Arguments[0]));
            //            return expression;
            //        }

            //    }
            //    else if (expression.Method.Name == nameof(Enumerable.Contains))
            //    {
            //        var valuesExpression = default(Expression);

            //        if (expression.Method.DeclaringType == typeof(Enumerable))
            //        {
            //            if (ExpressionTreeHelpers.IsSpecificMemberExpression(expression.Arguments[1], typeof(Place), "Name") ||
            //            ExpressionTreeHelpers.IsSpecificMemberExpression(expression.Arguments[1], typeof(Place), "State"))
            //            {
            //                valuesExpression = expression.Arguments[0];
            //            }
            //        }
            //        else if (expression.Method.DeclaringType == typeof(List<string>))
            //        {
            //            if (ExpressionTreeHelpers.IsSpecificMemberExpression(expression.Arguments[0], typeof(Place), "Name") ||
            //            ExpressionTreeHelpers.IsSpecificMemberExpression(expression.Arguments[0], typeof(Place), "State"))
            //            {
            //                valuesExpression = expression.Object;
            //            }
            //        }

            //        if (valuesExpression == null || valuesExpression.NodeType != ExpressionType.Constant)
            //            throw new Exception("Could not find the location values.");

            //        var ce = (ConstantExpression)valuesExpression;

            //        var placeStrings = (IEnumerable<string>)ce.Value;
            //        // Add each string in the collection to the list of locations to obtain data about. 
            //        _locations.AddRange(placeStrings);

            //        return expression;
            //    }

            //    return base.VisitMethodCall(expression);
            //}
        }
    }
}
