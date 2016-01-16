using aop.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.Services
{
    public interface ISaleRepository: IRepository<Sale>
    {
        void Schedule(Sale item);
    }
}
