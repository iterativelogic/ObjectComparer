using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    public class TypeStateFactory
    {
        public static PropertyState GetState<T>(object first, object second) where T : PropertyState, new()
        {
            var state = new T();

            state.FirstObject = first;
            state.SecondObject = second;

            return state;
        }
    }
}
