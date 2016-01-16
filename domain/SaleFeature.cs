using aop.aspects;
using aop.Services;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace aop.domain
{
    public class SaleFeature : aop.domain.ISaleFeature
    {
        readonly ISaleRepository _repository;

        public SaleFeature(ISaleRepository repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Saves the sale or queues it for later
        /// </summary>
        /// <param name="sale"></param>        
        public virtual void Save(Sale sale)
        {
            //check if it's a scheduled sale
            if (sale.Date > DateTime.Now)
            {                
                _repository.Schedule(sale);
                sale.State = Sale.SaleState.Reserved;
            }
            else
            {
                //otherwise save
                _repository.Save(sale);
                sale.State = Sale.SaleState.Finalized;
            }
        }

        /// <summary>
        /// Creates the sale
        /// </summary>
        /// <param name="sale"></param>
        public void Create(Sale sale)
        {
            sale.State = Sale.SaleState.Normal;
        }
    }
}
