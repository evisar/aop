using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.Common
{
    public class Workflow<TEntity, TState>
        where TEntity: IEntity, IWorkflow<TEntity, TState>
        where TState: struct
    {
        readonly TEntity _entity;

        public Workflow(TEntity entity)
        {
            _entity = entity;
        }

        TState _state;
        public TState State
        {
            get
            {
                return _state;
            }
            set
            {
                var allowedStated = _entity.GetStates(_state);
                if (allowedStated.Contains(value))
                {
                    _state = value;
                }
                else
                {
                    throw new Exception(
                        string.Format("State change from {0} to {1} is not allowed.", _state, value));
                }
            }
        }
    }
}
