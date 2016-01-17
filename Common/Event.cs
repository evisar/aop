using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.Common
{
    public abstract class Event<T>: IEvent
        where T: IEntity
    {
        public string Action { get; set; }
        public T Entity { get; set; }
    }
}
