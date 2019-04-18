namespace ObjectComparer
{
    public class NullStrategy : ComparisonStrategy
    {
        public override bool AreEqual()
        {
            return FirstObject == null && SecondObject == null;
        }
    }
}
