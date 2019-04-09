using System.Collections.Generic;

namespace ObjectComparer
{
    public class CollectionState : PropertyState
    {
        public override bool AreEqual()
        {
            var firstCollection = (IEnumerable<object>)FirstObject;
            var secondCollection = (IEnumerable<object>)SecondObject;

            var collectionSet = new HashSet<object>(firstCollection, EqualityComparer<object>.Default);

            collectionSet.UnionWith(secondCollection);
            collectionSet.ExceptWith(secondCollection);

            return collectionSet.Count == 0;
        }
    }
}
