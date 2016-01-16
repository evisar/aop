using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.aspects.castle
{
    public class MethodInterceptor : IInterceptor
    {
        readonly object _service;
        readonly IAspect _aspect;

        public MethodInterceptor(object service, IAspect aspect)
        {
            _service = service;
            _aspect = aspect;
        }

        public void Intercept(IInvocation invocation)
        {
            _aspect.Before(_service, invocation.Method, invocation.Arguments);
            try
            {
                invocation.Proceed();
                _aspect.After(_service, invocation.Method, invocation.Arguments);
            }
            catch(Exception ex)
            {
                _aspect.Error(_service, invocation.Method, ex, invocation.Arguments);
                throw;
            }            
        }
    }
}
