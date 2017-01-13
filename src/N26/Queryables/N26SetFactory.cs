using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace N26.Queryables
{
    public abstract class N26SetFactory
    {
        internal abstract Task<object> GetAsync(Expression expression);
    }

    public class N26SetFactory<T> : N26SetFactory
    {
        private readonly Func<Expression, Task<IEnumerable<T>>> _factoryAsync;

        public N26SetFactory(Func<Task<IEnumerable<T>>> factoryAsync)
            : this(async expression => await factoryAsync()) { }

        protected N26SetFactory(Func<Expression, Task<IEnumerable<T>>> factoryAsync)
        {
            _factoryAsync = factoryAsync ?? throw new ArgumentNullException(nameof(factoryAsync));
        }

        internal override async Task<object> GetAsync(Expression expression) => await _factoryAsync(expression);
    }
}
