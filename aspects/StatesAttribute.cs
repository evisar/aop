using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.aspects
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class StatesAttribute: Attribute
    {
        public object State { get; private set; }

        public StatesAttribute(object state)
        {
            this.State = state;
        }
    }
}
