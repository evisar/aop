using aop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aop.Services
{
    public interface IEventService
    {
        void Publish<TEvent>(TEvent @event) 
            where TEvent: IEvent;
        IDisposable Subscribe<TEvent>(Action<TEvent> action) 
            where TEvent : IEvent;
    }
}
