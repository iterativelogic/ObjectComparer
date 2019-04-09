using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    public abstract class PropertyState
    {
        public object FirstObject { get; set; }
        public object SecondObject { get; set; }

        public abstract bool AreEqual();
    }
}
