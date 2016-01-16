using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aop.aspects
{
    public class NullGuardAspect: IAspect
    {
        readonly int _numargs;
        readonly ILogger _logger;

        public NullGuardAspect(ILogger logger, int numargs = 0)
        {
            _numargs = numargs;
            _logger = logger;
        }

        public void Before(object service, MethodInfo method, params object[] args)
        {
            _logger.Info("Executing Null guards.");

            var @params = method.GetParameters();
            for(int i=0; i<=_numargs;i++)
            {
                if(args.Length<_numargs-1)
                {
                    throw new ArgumentException(string.Format("Argument {0} does not exist.", i));
                }
                if(args[i]==null)
                {
                    throw new ArgumentNullException(@params[i].Name);
                }
            }
        }

        public void After(object service, MethodInfo method, params object[] args)
        {
            
        }

        public void Error(object service, MethodInfo method, Exception ex, params object[] args)
        {
            
        }
    }
}
