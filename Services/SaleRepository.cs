using aop.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.Services
{
    public class SaleRepository: ISaleRepository
    {
        public void Schedule(Sale item)
        {
            Console.WriteLine("Sale scheduled.");
        }

        public void Save(Sale item)
        {
            Console.WriteLine("Sale saved.");
        }
    }
}
