using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.Common
{
    public interface IWorkflow<TEntity, TState>
        where TEntity: IEntity
        where TState: struct
    {
        TState State { get; set; }
        IEnumerable<TState> GetStates(TState state);
    }
}
