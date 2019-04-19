using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    public class ComparisonStrategyFactory
    {
        private static readonly Type _enumerableType = typeof(IEnumerable<object>);

        public static ComparisonStrategy GetStrategy<T>(T first, T second)
        {
            var objectType = typeof(T);

            if (objectType.IsValueType)
            {
                return new ValueTypeStrategy
                {
                    FirstObject = first,
                    SecondObject = second
                };
            }
            
            if (objectType.IsAssignableFrom(_enumerableType))
            {
                return new CollectionStrategy
                {
                    FirstObject = first,
                    SecondObject = second
                };
            }

            if (objectType.IsClass)
            {
                return new RefTypeStrategy
                {
                    FirstObject = first,
                    SecondObject = second
                };
            }

            return new NullStrategy
            {
                FirstObject = first,
                SecondObject = second
            };
        }
    }
}
