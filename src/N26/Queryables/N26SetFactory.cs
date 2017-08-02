using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using N26.Helpers;

namespace N26.Queryables
{
    internal abstract class N26SetFactory
    {
        public abstract Task<object> GetAsync(Expression expression);
    }

    internal class N26SetFactory<T> : N26SetFactory
    {
        private readonly Func<Expression, Task<IEnumerable<T>>> _factoryAsync;

        public N26SetFactory(Func<Task<IEnumerable<T>>> factoryAsync)
            : this(async expression => await factoryAsync()) { }

        protected N26SetFactory(Func<Expression, Task<IEnumerable<T>>> factoryAsync)
        {
            Guard.IsNotNull(factoryAsync, nameof(factoryAsync));
            _factoryAsync = factoryAsync;
        }

        public override async Task<object> GetAsync(Expression expression) => await _factoryAsync(expression);
    }
}
