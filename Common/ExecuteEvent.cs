using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.Common
{
    public class ExecuteEvent<T>: Event<T>
        where T: IEntity
    {
    }
}
