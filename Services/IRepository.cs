using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aop.Services
{
    public interface IRepository<T>
    {        
        void Save(T item);        
    }
}
