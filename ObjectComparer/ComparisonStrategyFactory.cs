using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    public class ComparisonStrategyFactory
    {
        private static readonly Type _enumerableType = typeof(IEnumerable);

        public static ComparisonStrategy GetStrategy<T>(T first, T second)
        {
            return GetStrategy(first, second, typeof(T), new HashSet<Type>());
        }

        public static ComparisonStrategy GetStrategy<T>(T first, T second, PropertyInfo property, HashSet<Type> previousTypes)
        {
            var objectType = property.PropertyType;

            var firstValue = property.GetValue(first);
            var secondValue = property.GetValue(second);

            if (firstValue == null || secondValue == null)
            {
                return new NullStrategy
                {
                    FirstObject = firstValue,
                    SecondObject = secondValue
                };
            }

            if (IsPreviousReferenceProperty(objectType, previousTypes))
            {
                return new PreviousObjectStrategy
                {
                    FirstObject = firstValue,
                    SecondObject = secondValue
                };
            }

            return GetStrategy(firstValue, secondValue, objectType, previousTypes);
        }

        public static ComparisonStrategy GetStrategy<T>(T first, T second, Type type, HashSet<Type> previousTypes)
        {
            if (type.IsValueType)
            {
                return new ValueTypeStrategy
                {
                    FirstObject = first,
                    SecondObject = second
                };
            }

            if (type.IsArray || (_enumerableType.IsAssignableFrom(type) && type != typeof(string)))
            {
                return new CollectionStrategy
                {
                    FirstObject = first,
                    SecondObject = second
                };
            }

            if (type.IsClass)
            {
                return new RefTypeStrategy(previousTypes)
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

        private static bool IsPreviousReferenceProperty(Type propertyType, HashSet<Type> previousReferences)
        {
            return previousReferences.Any(type => type == propertyType);
        }
    }
}
