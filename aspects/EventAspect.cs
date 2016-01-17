using aop.Common;
using aop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aop.aspects
{
    public class EventAspect: IAspect
    {
        readonly IEventService _eventService;

        public EventAspect(IEventService eventService)
        {
            _eventService = eventService;
        }

        public void Before(object service, System.Reflection.MethodInfo method, params object[] args)
        {
            
        }

        public void After(object service, System.Reflection.MethodInfo method, params object[] args)
        {
            var name = method.DeclaringType.FullName + "." + method.Name;

            Type selectedFeature = null;
            Type baseFeature = service.GetType();
            while (baseFeature != null)
            {
                selectedFeature = baseFeature;
                baseFeature = baseFeature.GetInterfaces().FirstOrDefault();
            }
            dynamic item =
                (from arg in args
                 where arg != null && arg.GetType() == selectedFeature.GetGenericArguments().FirstOrDefault()
                 select arg).FirstOrDefault();

            var entityType = item.GetType();

            var eventType = typeof(ExecuteEvent<>).MakeGenericType(entityType);

            dynamic @event = Activator.CreateInstance(eventType);
            @event.Action = name;
            @event.Entity = item;

            var mi = _eventService.GetType().GetMethod("Publish").MakeGenericMethod(eventType);
            mi.Invoke(_eventService, new[] { @event });
        }

        public void Error(object service, System.Reflection.MethodInfo method, Exception ex, params object[] args)
        {
            
        }
    }
}
