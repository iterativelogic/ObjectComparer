namespace ObjectComparer
{
    public class PreviousObjectState : PropertyState
    {
        public override bool AreEqual()
        {
            return ReferenceEquals(FirstObject, SecondObject);
        }
    }
}
