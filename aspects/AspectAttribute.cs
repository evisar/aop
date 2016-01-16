using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.aspects
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface|AttributeTargets.Method, AllowMultiple=true)]
    public class AspectAttribute: Attribute
    {
        public Type Type { get; private set; }

        public AspectAttribute(Type type)
        {
            this.Type = type;
        }
    }
}
