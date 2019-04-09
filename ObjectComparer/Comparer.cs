using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    public class Comparer
    {
        public static bool AreSimilar<T>(T first, T second )
        {
            var propertyState = new RefTypeState
            {
                FirstObject = first,
                SecondObject = second
            };

            var result = propertyState.AreEqual();

            return result;
        }
        
    }
}
