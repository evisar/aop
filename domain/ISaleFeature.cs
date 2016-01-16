using aop.aspects;
using aop.Common;
using System;
namespace aop.domain
{
    [Aspect(typeof(NullGuardAspect))]
    [Aspect(typeof(LoggingAspect))]
    [Aspect(typeof(TransactionAspect))]
    [Aspect(typeof(WorkflowAspect))]
    public interface ISaleFeature: IFeature<Sale>
    {
        [States(Sale.SaleState.New)]
        void Create(aop.domain.Sale sale);

        [States(Sale.SaleState.Normal)]
        [States(Sale.SaleState.Reserved)]
        void Save(aop.domain.Sale sale);
    }
}
