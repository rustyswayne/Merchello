namespace Merchello.Core.Logging
{
    using System;

    using Merchello.Core.DI;

    /// <summary>
    /// The multi log helper.
    /// </summary>
    public static class MultiLogHelper
    {
        /// <summary>
        /// Gets the Umbraco <see cref="ILogger"/>.
        /// </summary>
        public static ILogger Logger => MC.Container.GetInstance<ILogger>();

        /// <summary>
        /// Gets the remote logger.
        /// </summary>
        public static IRemoteLogger RemoteLogger => MC.Container.GetInstance<IRemoteLogger>();

        /// <summary>
        /// The <see cref="IMultiLogger"/>.
        /// </summary>
        internal static IMultiLogger MultiLogger => MC.Container.GetInstance<IMultiLogger>();

        /// <summary>
        /// Logs and error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <typeparam name="T">
        /// The type of the calling instance.
        /// </typeparam>
        public static void Error<T>(string message, Exception exception)
        {
            Error(typeof(T), message, exception);
        }

        /// <summary>
        /// Logs and error.
        /// </summary>
        /// <param name="callingType">
        /// The type of the calling instance.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public static void Error(Type callingType, string message, Exception exception)
        {
            MultiLogger.Logger.Error(callingType, message, exception);
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="formatItems">
        /// The format items.
        /// </param>
        /// <typeparam name="T">
        /// The type of the calling instance.
        /// </typeparam>
        public static void Warn<T>(string message, params Func<object>[] formatItems)
        {
            Warn(typeof(T), message, formatItems);
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="callingType">
        /// The type of the calling instance.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="formatItems">
        /// The format items.
        /// </param>
        public static void Warn(Type callingType, string message, params Func<object>[] formatItems)
        {
            MultiLogger.Logger.Warn(callingType, message, formatItems);
        }

        /// <summary>
        /// Logs a warning with an exception.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <param name="formatItems">
        /// The format items.
        /// </param>
        /// <typeparam name="T">
        /// The type of the calling instance.
        /// </typeparam>
        public static void WarnWithException<T>(string message, Exception e, params Func<object>[] formatItems)
        {
            WarnWithException(typeof(T), message, e, formatItems);
        }

        /// <summary>
        /// Logs a warning with an exception.
        /// </summary>
        /// <param name="callingType">
        /// The type of the calling instance.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="e">
        /// The exception.
        /// </param>
        /// <param name="formatItems">
        /// The format items.
        /// </param>
        public static void WarnWithException(Type callingType, string message, Exception e, params Func<object>[] formatItems)
        {
            MultiLogger.Logger.WarnWithException(callingType, message, e, formatItems);
        }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <typeparam name="T">
        /// The type of the calling instance.
        /// </typeparam>
        public static void Info<T>(string message)
        {
            Info(typeof(T), message);
        }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="callingType">
        /// The calling type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void Info(Type callingType, string message)
        {
            Info(callingType, () => message);
        }

        /// <summary>
        /// Logs an informative message.
        /// </summary>
        /// <param name="generateMessage">
        /// The generate message.
        /// </param>
        /// <typeparam name="T">
        /// The calling type
        /// </typeparam>
        public static void Info<T>(Func<string> generateMessage)
        {
            Info(typeof(T), generateMessage);
        }

        /// <summary>
        /// Logs an informative message.
        /// </summary>
        /// <param name="callingType">
        /// The calling type.
        /// </param>
        /// <param name="generateMessage">
        /// The generate message.
        /// </param>
        public static void Info(Type callingType, Func<string> generateMessage)
        {
            MultiLogger.Logger.Info(callingType, generateMessage);
        }

        /// <summary>
        /// Logs an informative message.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="generateMessageFormat">
        /// The generate message format.
        /// </param>
        /// <param name="formatItems">
        /// The format items.
        /// </param>
        public static void Info(Type type, string generateMessageFormat, params Func<object>[] formatItems)
        {
            MultiLogger.Logger.Info(type, generateMessageFormat, formatItems);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="generateMessage">
        /// The generate message.
        /// </param>
        /// <typeparam name="T">
        /// The calling type
        /// </typeparam>
        public static void Debug<T>(Func<string> generateMessage)
        {
            Debug(typeof(T), generateMessage);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="callingType">
        /// The calling type.
        /// </param>
        /// <param name="generateMessage">
        /// The generate message.
        /// </param>
        public static void Debug(Type callingType, Func<string> generateMessage)
        {
            MultiLogger.Logger.Debug(callingType, generateMessage);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="generateMessageFormat">
        /// The generate message format.
        /// </param>
        /// <param name="formatItems">
        /// The format items.
        /// </param>
        /// <typeparam name="T">
        /// The calling type
        /// </typeparam>
        public static void Debug<T>(string generateMessageFormat, params Func<object>[] formatItems)
        {
            Debug(typeof(T), generateMessageFormat, formatItems);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="generateMessageFormat">
        /// The generate message format.
        /// </param>
        /// <param name="formatItems">
        /// The format items.
        /// </param>
        public static void Debug(Type type, string generateMessageFormat, params Func<object>[] formatItems)
        {
            MultiLogger.Logger.Debug(type, generateMessageFormat, formatItems);
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        /// <typeparam name="T">
        /// The calling type
        /// </typeparam>
        public static void Error<T>(string message, Exception exception, IExtendedLoggerData loggerData)
        {
            Error(typeof(T), message, exception, loggerData);
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
        /// <param name="callingType">
        /// The calling Type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        public static void Error(Type callingType, string message, Exception exception, IExtendedLoggerData loggerData)
        {
            MultiLogger.Error(callingType, message, exception, loggerData);
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        /// <typeparam name="T">
        /// The calling type
        /// </typeparam>
        public static void Warn<T>(string message, IExtendedLoggerData loggerData)
        {
            Warn(typeof(T), message, loggerData);
        }

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="callingType">
        /// The calling type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        public static void Warn(Type callingType, string message, IExtendedLoggerData loggerData)
        {
            MultiLogger.Warn(callingType, message, loggerData);
        }

        /// <summary>
        /// Logs a warning with an exception.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        /// <typeparam name="T">
        /// The calling type
        /// </typeparam>
        public static void WarnWithException<T>(string message, Exception exception, IExtendedLoggerData loggerData)
        {
            WarnWithException(typeof(T), message, exception, loggerData);
        }

        /// <summary>
        /// Logs a warning with an exception.
        /// </summary>
        /// <param name="callingType">
        /// The calling type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        public static void WarnWithException(Type callingType, string message, Exception exception, IExtendedLoggerData loggerData)
        {
            MultiLogger.WarnWithException(callingType, message, exception, loggerData);
        }

        /// <summary>
        /// Logs an informative message
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        /// <typeparam name="T">
        /// The calling type
        /// </typeparam>
        public static void Info<T>(string message, IExtendedLoggerData loggerData)
        {
            Info(typeof(T), message, loggerData);
        }

        /// <summary>
        /// Logs an informative message.
        /// </summary>
        /// <param name="callingType">
        /// The calling type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        public static void Info(Type callingType, string message, IExtendedLoggerData loggerData)
        {
            MultiLogger.Info(callingType, message, loggerData);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <typeparam name="T">
        /// The calling type
        /// </typeparam>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        public static void Debug<T>(string message, IExtendedLoggerData loggerData)
        {
            Debug(typeof(T), message, loggerData);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="callingType">
        /// The calling type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="loggerData">
        /// The logger data.
        /// </param>
        public static void Debug(Type callingType, string message, IExtendedLoggerData loggerData)
        {
            MultiLogger.Debug(callingType, message, loggerData);
        }
    }
}