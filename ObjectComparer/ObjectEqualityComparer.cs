using System.Collections.Generic;

namespace ObjectComparer
{
    public class ObjectEqualityComparer : EqualityComparer<object>
    {
        public override bool Equals(object x, object y)
        {
            var comparer = ComparisonStrategyFactory.GetStrategy(x, y);
            var result = comparer.AreEqual();

            return result;
        }

        public override int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}
