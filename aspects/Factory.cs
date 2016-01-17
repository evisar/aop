using aop.aspects.castle;
using aop.domain;
using aop.Services;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.aspects
{
    public class Factory
    {
        static readonly ProxyGenerator _proxy;
        static IKernel _kernel;

        public static IKernel Kernel
        {
            get { return Factory._kernel; }
            set { Factory._kernel = value; }
        }

        static Factory()
        {
            _proxy = new ProxyGenerator();
            _kernel = new StandardKernel();

            BootstrapDependencies(_kernel);                       
        }

        private static void BootstrapDependencies(IKernel kernel)
        {
            kernel.Bind<IEventService>().ToConstant(new EventService());
            kernel.Bind<ILogger>().To<ConsoleLogger>();
            kernel.Bind<ISaleRepository>().To<SaleRepository>();
            kernel.Bind<ISaleFeature>().To<SaleFeature>();
        }

        private static IInterceptor[] WireAspects<T>(IKernel kernel, T target)
            where T: class
        {
            return
                (from aa in typeof(T).GetCustomAttributes(typeof(AspectAttribute), false)
                let aspect = _kernel.Get((aa as AspectAttribute).Type) as IAspect
                select new MethodInterceptor(target, aspect)).Reverse().ToArray();
        }

        public static T Create<T>()
            where T: class
        {
            var feature = _kernel.Get<T>();
            var aspects = WireAspects<T>(_kernel, feature);
            return _proxy.CreateInterfaceProxyWithTarget<T>(feature, aspects);
        }
    }
}
