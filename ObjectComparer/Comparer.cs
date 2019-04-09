using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    public class Comparer
    {
        public static bool AreSimilar<T>(T first, T second )
        {
            var objectType = typeof(T);

            if (objectType.IsValueType)
            {
                var valueTypeMatcher = TypeStateFactory.GetState<ValueTypeState>(first, second);
                return valueTypeMatcher.AreEqual();
            }

            var enumerableType = typeof(IEnumerable<object>);

            if (objectType.IsAssignableFrom(enumerableType))
            {
                var collectionTypeMatcher = TypeStateFactory.GetState<CollectionState>(first, second);
                return collectionTypeMatcher.AreEqual();
            }

            if (objectType.IsClass)
            {
                var referenceTypeMatcher = TypeStateFactory.GetState<RefTypeState>(first, second);
                return referenceTypeMatcher.AreEqual();
            }

            var propertyState = new RefTypeState
            {
                FirstObject = first,
                SecondObject = second
            };

            var result = propertyState.AreEqual();

            return result;
        }
        
    }
}
