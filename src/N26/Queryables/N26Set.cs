using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace N26.Queryables
{
    public class N26Set<TEntity> : IQueryable<TEntity>
    {
        public N26Set(N26SetFactory setFactory)
        {
            Provider = new N26Provider(setFactory);
            Expression = Expression.Constant(this);
        }

        internal N26Set(N26Provider provider, Expression expression)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            if (!typeof(IQueryable<TEntity>).IsAssignableFrom(expression.Type)) throw new ArgumentOutOfRangeException(nameof(expression));
        }

        public IQueryProvider Provider { get; }
        public Expression Expression { get; }

        public Type ElementType => typeof(TEntity);

        public IEnumerator<TEntity> GetEnumerator() => (Provider.Execute<IEnumerable<TEntity>>(Expression)).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
    }

#if false
    internal class ExpressionTreeModifier : ExpressionVisitor
    {
        private readonly IQueryable<Place> _queryablePlaces;

        public ExpressionTreeModifier(IQueryable<Place> places)
        {
            _queryablePlaces = places;
        }

        protected override Expression VisitConstant(ConstantExpression ce)
        {
            // Replace the constant QueryableN26Data arg with the queryable Place collection.
            if (ce.Type == typeof(QueryableN26Data<Place>))
                return Expression.Constant(_queryablePlaces);
            else
                return ce;
        }
    }

    internal static class Evaluator
    {
        /// <summary> 
        /// Performs evaluation & replacement of independent sub-trees 
        /// </summary> 
        /// <param name="expression">The root of the expression tree.</param>
        /// <param name="canBeEvaluated">A function that decides whether a given expression node can be part of the local function.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns> 
        public static Expression PartialEval(Expression expression, Func<Expression, bool> canBeEvaluated)
            => new SubtreeEvaluator(new Nominator(canBeEvaluated).Nominate(expression)).Eval(expression);

        /// <summary> 
        /// Performs evaluation & replacement of independent sub-trees 
        /// </summary> 
        /// <param name="expression">The root of the expression tree.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns> 
        public static Expression PartialEval(Expression expression)
            => PartialEval(expression, CanBeEvaluatedLocally);

        private static bool CanBeEvaluatedLocally(Expression expression) => expression.NodeType != ExpressionType.Parameter;

        /// <summary> 
        /// Evaluates & replaces sub-trees when first candidate is reached (top-down) 
        /// </summary> 
        class SubtreeEvaluator : ExpressionVisitor
        {
            private readonly HashSet<Expression> _candidates;

            public SubtreeEvaluator(HashSet<Expression> candidates)
            {
                _candidates = candidates;
            }

            public Expression Eval(Expression exp) => Visit(exp);

            public override Expression Visit(Expression expression)
            {
                if (expression == null) return null;
                if (_candidates.Contains(expression)) return Evaluate(expression);
                return base.Visit(expression);
            }

            private Expression Evaluate(Expression expression)
            {
                if (expression.NodeType == ExpressionType.Constant) return expression;
                var lambda = Expression.Lambda(expression);
                var function = lambda.Compile();
                return Expression.Constant(function.DynamicInvoke(null), expression.Type);
            }
        }

        /// <summary>
        /// Performs bottom-up analysis to determine which nodes can possibly
        /// be part of an evaluated sub-tree.
        /// </summary>
        class Nominator : ExpressionVisitor
        {
            private readonly Func<Expression, bool> _canBeEvaluated;
            private HashSet<Expression> _candidates;
            private bool _cannotBeEvaluated;

            public Nominator(Func<Expression, bool> canBeEvaluated)
            {
                _canBeEvaluated = canBeEvaluated;
            }

            public HashSet<Expression> Nominate(Expression expression)
            {
                _candidates = new HashSet<Expression>();
                Visit(expression);
                return _candidates;
            }

            public override Expression Visit(Expression expression)
            {
                if (expression != null)
                {
                    var saveCannotBeEvaluated = _cannotBeEvaluated;
                    _cannotBeEvaluated = false;
                    base.Visit(expression);
                    if (!_cannotBeEvaluated)
                    {
                        if (_canBeEvaluated(expression))
                            _candidates.Add(expression);
                        else
                            _cannotBeEvaluated = true;
                    }
                    _cannotBeEvaluated |= saveCannotBeEvaluated;
                }
                return expression;
            }
        }
    }

    internal static class ExpressionTreeHelpers
    {
        public static bool IsMemberEqualsValueExpression(Expression expression, Type declaringType, string memberName)
        {
            if (expression.NodeType != ExpressionType.Equal) return false;

            var be = (BinaryExpression)expression;

            // Assert.
            if (IsSpecificMemberExpression(be.Left, declaringType, memberName) && IsSpecificMemberExpression(be.Right, declaringType, memberName))
                throw new Exception("Cannot have 'member' == 'member' in an expression!");

            return IsSpecificMemberExpression(be.Left, declaringType, memberName) || IsSpecificMemberExpression(be.Right, declaringType, memberName);
        }

        public static bool IsSpecificMemberExpression(Expression exp, Type declaringType, string memberName)
        {
            return (exp is MemberExpression) &&
                (((MemberExpression)exp).Member.DeclaringType == declaringType) &&
                (((MemberExpression)exp).Member.Name == memberName);
        }

        public static string GetValueFromEqualsExpression(BinaryExpression be, Type memberDeclaringType, string memberName)
        {
            if (be.NodeType != ExpressionType.Equal) throw new Exception("There is a bug in this program.");
            if (be.Left.NodeType == ExpressionType.MemberAccess)
            {
                var me = (MemberExpression)be.Left;
                if (me.Member.DeclaringType == memberDeclaringType && me.Member.Name == memberName) return GetValueFromExpression(be.Right);
            }
            else if (be.Right.NodeType == ExpressionType.MemberAccess)
            {
                var me = (MemberExpression)be.Right;
                if (me.Member.DeclaringType == memberDeclaringType && me.Member.Name == memberName) return GetValueFromExpression(be.Left);
            }
            // We should have returned by now.
            throw new Exception("There is a bug in this program.");
        }

        public static string GetValueFromExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
                return (string)(((ConstantExpression)expression).Value);
            else
                throw new InvalidQueryException($"The expression type {expression.NodeType} is not supported to obtain a value.");
        }
    }

    internal class InvalidQueryException : Exception
    {
        public InvalidQueryException(string message) : base($"The client query is invalid: {message}.") { }
    }
#endif
}
