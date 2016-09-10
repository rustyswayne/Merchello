namespace Merchello.Core.Acquired
{
    using System;
    using System.Diagnostics;

    using Merchello.Core.Acquired.Logging;
    using Merchello.Core.Logging;

    /// <summary>
	/// Starts the timer and invokes a  callback upon disposal. Provides a simple way of timing an operation by wrapping it in a <code>using</code> (C#) statement.
	/// </summary>
	internal class DisposableTimer : DisposableObject
	{
        #region Fields

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The log type.
        /// </summary>
        private readonly LogType? _logType;

        /// <summary>
        /// The profiler.
        /// </summary>
        private readonly IProfiler _profiler;

        /// <summary>
        /// The logger type.
        /// </summary>
        private readonly Type _loggerType;

        /// <summary>
        /// The _end message.
        /// </summary>
        private readonly string _endMessage;

        /// <summary>
        /// The minimum millisecond threshold.
        /// </summary>
        private readonly int _minimumMsThreshold = 0;

        /// <summary>
        /// The profiler step.
        /// </summary>
        private readonly IDisposable _profilerStep;

        /// <summary>
        /// The stopwatch.
        /// </summary>
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        /// <summary>
        /// The callback.
        /// </summary>
        private readonly Action<long> _callback;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableTimer"/> class.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="logType">
        /// The log type.
        /// </param>
        /// <param name="profiler">
        /// The profiler.
        /// </param>
        /// <param name="loggerType">
        /// The logger type.
        /// </param>
        /// <param name="startMessage">
        /// The start message.
        /// </param>
        /// <param name="endMessage">
        /// The end message.
        /// </param>
        /// <param name="minimumMsThreshold">
        /// The minimum milliseconds threshold.
        /// </param>
        internal DisposableTimer(ILogger logger, LogType logType, IProfiler profiler, Type loggerType, string startMessage, string endMessage, int minimumMsThreshold)
	    {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (loggerType == null) throw new ArgumentNullException(nameof(loggerType));

            this._logger = logger;
            this._logType = logType;
            this._profiler = profiler;
            this._loggerType = loggerType;
            this._endMessage = endMessage;
	        this._minimumMsThreshold = minimumMsThreshold;

            //// NOTE: We aren't logging the start message with this ctor, this is output to the profiler but not the log,
            //// we just want the log to contain the result  if it's more than the minimum ms threshold

	        if (profiler != null)
            {
                this._profilerStep = profiler.Step(loggerType, startMessage);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableTimer"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="logType">
        /// The log type.
        /// </param>
        /// <param name="profiler">
        /// The profiler.
        /// </param>
        /// <param name="loggerType">
        /// The logger type.
        /// </param>
        /// <param name="startMessage">
        /// The start message.
        /// </param>
        /// <param name="endMessage">
        /// The end message.
        /// </param>
        internal DisposableTimer(ILogger logger, LogType logType, IProfiler profiler, Type loggerType, string startMessage, string endMessage)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (loggerType == null) throw new ArgumentNullException(nameof(loggerType));

            this._logger = logger;
            this._logType = logType;
            this._profiler = profiler;
            this._loggerType = loggerType;
            this._endMessage = endMessage;
            
            switch (logType)
            {
                case LogType.Debug:
                    logger.Debug(loggerType, startMessage);
                    break;
                case LogType.Info:
                    logger.Info(loggerType, startMessage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logType));
            }
            
            if (profiler != null)
            {
                this._profilerStep = profiler.Step(loggerType, startMessage);
            }
        }

        internal enum LogType
        {
            Debug, Info
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableTimer"/> class.
        /// </summary>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws if callback is null
        /// </exception>
        protected internal DisposableTimer(Action<long> callback)
	    {
	        if (callback == null) throw new ArgumentNullException(nameof(callback));
	        this._callback = callback;
	    }

        /// <summary>
        /// Gets the stopwatch.
        /// </summary>
        public Stopwatch Stopwatch
		{
			get { return this._stopwatch; }
		}

		/// <summary>
		/// Starts the timer and invokes the specified callback upon disposal.
		/// </summary>
		/// <param name="callback">The callback.</param>
		/// <returns>The disposable timer</returns>
		[Obsolete("Use either TraceDuration or DebugDuration instead of using Start")]
		public static DisposableTimer Start(Action<long> callback)
		{
			return new DisposableTimer(callback);
		}


		/// <summary>
		/// Handles the disposal of resources. Derived from abstract class <see cref="DisposableObject"/> which handles common required locking logic.
		/// </summary>
		protected override void DisposeResources()
		{
            this.Stopwatch.Stop();

            if (this._profiler != null)
            {
                this._profiler.DisposeIfDisposable();
            }

		    this._profilerStep?.Dispose();

		    if (this.Stopwatch.ElapsedMilliseconds >= this._minimumMsThreshold && this._logType.HasValue && this._endMessage.IsNullOrWhiteSpace() == false && this._loggerType != null && this._logger != null)
		    {
                switch (this._logType)
                {
                    case LogType.Debug:
                        this._logger.Debug(this._loggerType, () => this._endMessage + " (took " + this.Stopwatch.ElapsedMilliseconds + "ms)");
                        break;
                    case LogType.Info:
                        this._logger.Info(this._loggerType, () => this._endMessage + " (took " + this.Stopwatch.ElapsedMilliseconds + "ms)");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("logType");
                }
            }

		    this._callback?.Invoke(this.Stopwatch.ElapsedMilliseconds);
		}

	}
}