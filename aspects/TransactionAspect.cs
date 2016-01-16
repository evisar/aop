using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace aop.aspects
{
    public class TransactionAspect: IAspect
    {
        TransactionScope _scope;
        ILogger _logger;

        public TransactionAspect(ILogger logger)
        {
            _logger = logger;
        }

        public void Before(object service, MethodInfo method, params object[] args)
        {
            _scope = new TransactionScope();
            _logger.Info(string.Format( "Creating transaction for {0}/{1}", service.GetType().Name, method));
        }

        public void After(object service, MethodInfo method, params object[] args)
        {
            _scope.Complete();
            _logger.Info(string.Format("Commiting transaction for {0}/{1}", service.GetType().Name, method));
        }

        public void Error(object service, MethodInfo method, Exception ex, params object[] args)
        {
            _logger.Info(string.Format( "Rolling back transaction for {0}/{1}", service.GetType().Name, method));
        }
    }
}
