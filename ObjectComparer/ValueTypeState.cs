namespace ObjectComparer
{
    public class ValueTypeState : PropertyState
    {
        public override bool AreEqual()
        {
            return FirstObject.Equals(SecondObject);
        }
    }
}
