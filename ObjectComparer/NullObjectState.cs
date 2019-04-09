namespace ObjectComparer
{
    public class NullObjectState : PropertyState
    {
        public override bool AreEqual()
        {
            return FirstObject == null && SecondObject == null;
        }
    }
}
