using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace ObjectComparer
{
    public class CollectionStrategy : ComparisonStrategy
    {
        public override bool AreEqual()
        {
            var equalityComparer = new ObjectEqualityComparer();

            var firstCollection = ((IEnumerable)FirstObject).Cast<object>();
            var secondCollection = ((IEnumerable)SecondObject).Cast<object>();
                       
            var collectionSet = new HashSet<object>(firstCollection, equalityComparer);

            collectionSet.UnionWith(secondCollection);
            collectionSet.ExceptWith(secondCollection);

            return collectionSet.Count == 0;
        }
    }
}
