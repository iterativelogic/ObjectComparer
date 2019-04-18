namespace ObjectComparer
{
    public class ValueTypeStrategy : ComparisonStrategy
    {
        public override bool AreEqual()
        {
            return FirstObject.Equals(SecondObject);
        }
    }
}
