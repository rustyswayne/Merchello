namespace Merchello.Core.Observation
{
    using System;
    using System.Reflection;

    using Merchello.Core.Logging;

    /// <summary>
    /// Defines a Base Monitor
    /// </summary>
    /// <typeparam name="T">
    /// The type of the monitor Model
    /// </typeparam>
    public abstract class MonitorBase<T> : IObserver<T>, IMonitor
    {
        /// <summary>
        /// Gets the type observable used by the monitor.
        /// </summary>
        public Type ObservesType => typeof(T);

        /// <summary>
        /// Performs the action
        /// </summary>
        /// <param name="value">The model used in the monitor</param>
        public abstract void OnNext(T value);

        /// <summary>
        /// Handles an error.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        public virtual void OnError(Exception error)
        {
            var logData = MultiLogger.GetBaseLoggingData();
            logData.AddCategory("Monitors");
            MultiLogHelper.Error<MonitorBase<T>>("Monitor error: ", error, logData);
        }

        /// <summary>
        /// Handles completed.
        /// </summary>
        public virtual void OnCompleted()
        {
            var logData = MultiLogger.GetBaseLoggingData();
            logData.AddCategory("Monitors");
            MultiLogHelper.Debug<MonitorBase<T>>($"Completed monitoring {this.GetType()}", logData);
        }

        /// <summary>
        /// Subscribes itself to a <see cref="ITrigger"/>
        /// </summary>
        /// <param name="register">
        /// The <see cref="ITriggerRegister"/> to find the trigger this monitor subscribes to
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/> monitor
        /// </returns>
        public IDisposable Subscribe(ITriggerRegister register)
        {
            var att = GetType().GetCustomAttribute<MonitorForAttribute>(false);
            if (att != null)
            {
                var trigger = (IObservable<T>)register.GetTrigger(att.ObservableTrigger);
                if (trigger != null)
                {
                    MultiLogHelper.Info<MonitorBase<T>>($"{this.GetType().Name} subscribing to {trigger.GetType().Name}");
                    return trigger.Subscribe(this);                    
                }
            }

            MultiLogHelper.Debug<MonitorBase<T>>("Subscribe failed for type" + GetType());
            return null;
        }
    }
}