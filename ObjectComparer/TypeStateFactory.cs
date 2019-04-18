using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    public class ComparisonStrategyFactory
    {
        public static ComparisonStrategy GetStrategy<T>(object first, object second) where T : ComparisonStrategy, new()
        {
            var state = new T();

            state.FirstObject = first;
            state.SecondObject = second;

            return state;
        }
    }
}
