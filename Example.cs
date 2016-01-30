using Castle.Core.Logging;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace aop
{

    /// <summary>
    /// This example uses Castle.DynamicProxy
    /// </summary>
    public class Example
    {
        public static void Main()
        {
            //infrastructure and dependencies
            var proxyGen = new ProxyGenerator();
            var logger = new ConsoleLogger();

            //light object
            var light = new LightBulb();

            //light object proxy factory with aspects
            var logAspect = new LoggingAspectInterceptor(logger);
            var tranAspect = new TransactionAspectInterceptor();
            var stateAspect = new StateAspectInterceptor<LightBulb.LightBulbState>();
            var lightProxy = proxyGen.CreateInterfaceProxyWithTarget<ILightBulb>(light, logAspect, tranAspect, stateAspect);

            //call turn on feature, when light is off, should be ok
            lightProxy.TurnOn();

            //call turn on feature when light is on, should thrown invalid operation
            lightProxy.TurnOn();
        }
    }

    /// <summary>
    /// Manages state transitions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IState<T>
    {
        T State { get; }
    }

    /// <summary>
    /// Our object contract 
    /// </summary>
    public interface ILightBulb
    {
        //light bulv can be turned off, only when is in state on
        [AllowedState(LightBulb.LightBulbState.On)]
        void TurnOff();
        //light bulb can be turned off, only when is in state off
        [AllowedState(LightBulb.LightBulbState.Off)]
        void TurnOn();
    }

    /// <summary>
    /// Out object implementation
    /// </summary>
    public class LightBulb : aop.ILightBulb, aop.IState<LightBulb.LightBulbState>
    {
        /// <summary>
        /// Out object states
        /// </summary>
        public enum LightBulbState
        {            
            Off,
            On,
            BlownUp
        }

        /// <summary>
        /// Current state of the object
        /// </summary>
        public LightBulbState State { get; private set; }


        /// <summary>
        /// Functionality torn on the light
        /// </summary>
        public virtual void TurnOn()
        {
            State = LightBulbState.On;
        }

        /// <summary>
        /// Functionality turn off the light
        /// </summary>
        public virtual void TurnOff()
        {
            State = LightBulbState.Off;
        }
    }

    

    /// <summary>
    /// Method logger aspect
    /// </summary>
    public class LoggingAspectInterceptor: IInterceptor
    {

        readonly ILogger _logger;

        public LoggingAspectInterceptor(ILogger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {

            var method = invocation.Method.Name;

            try
            {
                _logger.Info("Entering method: " + method);


                invocation.Proceed();
            }
            catch (Exception ex)
            {
                _logger.Error(method + " execution failed.", ex);
                throw;
            }
            finally
            {
                _logger.Info("Finishing method: " + method);
            }
        }
    }

    /// <summary>
    /// Transaction logger aspect
    /// </summary>
    public class TransactionAspectInterceptor: IInterceptor
    {

        public void Intercept(IInvocation invocation)
        {
            //create new transaction
            using (var tran = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    tran.Complete(); //commit
                }
                catch
                {
                    throw;
                }
            }
        }
    }

    /// <summary>
    /// Attribute that defines allowed states on methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowedStateAttribute : Attribute
    {
        public object State { get; private set; }

        public AllowedStateAttribute(object state)
        {
            this.State = state;
        }
    }

    /// <summary>
    /// Managed state aspect
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StateAspectInterceptor<T>: IInterceptor
    {

        public void Intercept(IInvocation invocation)
        {

            //get all defiend states
            var allowedStatesAttribute = invocation.Method.GetCustomAttributes(typeof(AllowedStateAttribute), false).OfType<AllowedStateAttribute>().FirstOrDefault();
            
            if (allowedStatesAttribute != null)
            {
                //convert enum flags and state to uint
                var allowedStates = Convert.ToUInt64(allowedStatesAttribute.State);
                var target = invocation.InvocationTarget as IState<T>;
                var state = Convert.ToUInt64(target.State);

                //check if state is within the allowed states 
                if ((allowedStates & state) != state)
                {
                    throw new InvalidOperationException();
                }
            }
            
            invocation.Proceed();
        }
    }
}
