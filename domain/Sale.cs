using aop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aop.domain
{
    public class Sale: IEntity, IWorkflow<Sale, Sale.SaleState>
    {

        readonly Workflow<Sale, Sale.SaleState> _workflow;

        public enum SaleState
        {
            New,
            Reserved,
            Normal,
            Finalized
        }

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public object Identity { get; set; }

        public IEntity Signature { get; set; }

        public SaleState State
        {
            get
            {
                return _workflow.State;
            }
            set
            {
                _workflow.State = value;
            }
        }

        public Sale()
        {
            this.Id = Guid.NewGuid();
            _workflow = new Workflow<Sale, SaleState>(this);
        }

        public bool IsValid()
        {
            return true;
        }


        public IEnumerable<Sale.SaleState> GetStates(SaleState state)
        {
            switch(state)
            {
                case SaleState.New:
                    yield return SaleState.Normal;
                    yield return SaleState.Reserved;
                    break;
                case SaleState.Reserved:
                    yield return SaleState.Normal;
                    break;
                case SaleState.Normal:
                    yield return SaleState.Finalized;
                    break;
            };
        }
    }
}
