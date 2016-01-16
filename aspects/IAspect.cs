using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aop.aspects
{
    public interface IAspect
    {
        void Before(object service, MethodInfo method, params object[] args);
        void After(object service, MethodInfo method, params object[] args);
        void Error(object service, MethodInfo method, Exception ex, params object[] args);
    }
}
