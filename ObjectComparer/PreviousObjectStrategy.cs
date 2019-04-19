namespace ObjectComparer
{
    public class PreviousObjectStrategy : ComparisonStrategy
    {
        public override bool AreEqual()
        {
            return ReferenceEquals(FirstObject, SecondObject) ? true : new RefTypeStrategy
            {
                FirstObject = FirstObject,
                SecondObject = SecondObject
            }.AreEqual();
        }
    }
}
