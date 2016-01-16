using aop.aspects;
using aop.aspects.castle;
using Castle.DynamicProxy;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Conventions;
using Castle.Core.Logging;
using aop.domain;

namespace aop
{
    class Program
    {
        static void Main(string[] args)
        {
            var sale = new Sale();
            var feature = Factory.Create<ISaleFeature>();

            feature.Create(sale);
            feature.Save(sale);
            
            Console.ReadKey();
        }
    }
}
