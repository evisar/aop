using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aop.aspects
{
    public class LoggingAspect: IAspect
    {
        readonly ILogger _logger;
        public LoggingAspect(ILogger logger)
        {
            _logger = logger;
        }

        public void Before(object service, MethodInfo method, params object[] args)
        {
            var message =  string.Format("Before executing {0}/{1}", service.GetType().Name, method);
            _logger.Info(message);
        }

        public void After(object service, MethodInfo method, params object[] args)
        {
            var message = string.Format("After executing {0}/{1}", service.GetType().Name, method);
            _logger.Info(message);
        }

        public void Error(object service, MethodInfo method, Exception ex, params object[] args)
        {
            var message = string.Format("Error executing {0}/{1}", service.GetType().Name, method);
            _logger.Error(message, ex);
        }
    }
}
