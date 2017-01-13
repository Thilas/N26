using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace N26.Queryables
{
    internal class N26Provider : IQueryProvider
    {
        private readonly N26SetFactory _setFactory;

        public N26Provider(N26SetFactory setFactory)
        {
            _setFactory = setFactory;
        }

        public IQueryable CreateQuery(Expression expression) => throw new NotSupportedException();

        public IQueryable<TResult> CreateQuery<TResult>(Expression expression) => new N26Set<TResult>(this, expression);

        public object Execute(Expression expression) => Execute(expression, false);

        public TResult Execute<TResult>(Expression expression)
        {
            var type = typeof(TResult);
            if (type.IsConstructedGenericType) type = type.GetGenericTypeDefinition();
            return (TResult)Execute(expression, type == typeof(IEnumerable<>));
        }

        private object Execute(Expression expression, bool isEnumerable)
        {
            var set = Task.Run(async () => await _setFactory.GetAsync(expression)).Result;
            //var lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            //// Send the lambda expression through the partial evaluator.
            //lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

            //// Get the place name(s) to query the Web service with.
            //var lf = new LocationFinder(lambdaExpression.Body);
            //var locations = lf.Locations;
            //if (locations.Count == 0) throw new InvalidQueryException("You must specify at least one place name in your query.");

            //// Call the Web service and get the results.
            //var places = WebServiceHelper.GetPlacesFromN26(locations);

            //// Copy the IEnumerable places to an IQueryable.
            //var places = (IEnumerable)set;
            //var queryablePlaces = places.AsQueryable();

            //// Copy the expression tree that was passed in, changing only the first
            //// argument of the innermost MethodCallExpression.
            //var treeCopier = new ExpressionTreeModifier(queryablePlaces);
            //var newExpressionTree = treeCopier.Visit(expression);

            //// This step creates an IQueryable that executes by replacing Queryable methods with Enumerable methods.
            //if (isEnumerable)
            //    return queryablePlaces.Provider.CreateQuery(newExpressionTree);
            //else
            //    return queryablePlaces.Provider.Execute(newExpressionTree);
            return set;
        }
    }
}
