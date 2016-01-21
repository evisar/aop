using aop.domain;
using aop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.domain
{
    public interface ISaleRepository: IRepository<Sale>
    {
        void Schedule(Sale item);
    }
}
