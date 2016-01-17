using aop.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.aspects
{
    public class WorkflowAspect: IAspect
    {
        public void Before(object service, System.Reflection.MethodInfo method, params object[] args)
        {
            var states =
                from attr in method.GetCustomAttributes(typeof(StatesAttribute), false)
                 select (attr as StatesAttribute).State;

            Type selectedFeature = null;
            Type baseFeature = service.GetType();
            while(baseFeature!=null)
            {
                selectedFeature = baseFeature;
                baseFeature = baseFeature.GetInterfaces().FirstOrDefault();                
            }
            dynamic item =
                (from arg in args
                 where arg != null && arg.GetType() == selectedFeature.GetGenericArguments().FirstOrDefault()
                 select ((dynamic)arg).State).FirstOrDefault();

            if(!states.Contains((object)item))
            {
                throw new Exception(string.Format("Cannot execute {0}/{1} in [{2}] state.", service.GetType(), method, item));
            }
        }

        public void After(object service, System.Reflection.MethodInfo method, params object[] args)
        {
        }

        public void Error(object service, System.Reflection.MethodInfo method, Exception ex, params object[] args)
        {
        }
    }
}
