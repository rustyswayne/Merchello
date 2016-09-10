namespace Merchello.Core.Acquired
{
    using System;

    /// <summary>
    /// Represents the result of an operation attempt.
    /// </summary>
    /// <typeparam name="T">The type of the attempted operation result.</typeparam>
    /// <seealso cref="https://github.com/umbraco/Umbraco-CMS/blob/dev-v7/src/Umbraco.Core/Attempt%7BT%7D.cs"/>
    /// UMBRACO_SRC Direct port of Umbraco internal interface to get rid of hard dependency
    [Serializable]
	public struct Attempt<T>
	{
        /// <summary>
        /// optimize, use a singleton failed attempt
        /// </summary>
        private static readonly Attempt<T> Failed = new Attempt<T>(false, default(T), null);

        /// <summary>
        /// Initializes a new instance of the <see cref="Attempt{T}"/> struct.
        /// </summary>
        /// <param name="success">
        /// The success.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <remarks>
        /// Use Succeed() or Fail() methods to create attempts
        /// </remarks>
        private Attempt(bool success, T result, Exception exception)
        {
            this.Success = success;
            this.Result = result;
            this.Exception = exception;
        }


        /// <summary>
        /// Gets a value indicating whether this <see cref="Attempt{T}"/> was successful.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Gets the exception associated with an unsuccessful attempt.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Gets the attempt result.
        /// </summary>
        public T Result { get; }

        /// <summary>
        /// Creates a successful attempt.
        /// </summary>
        /// <returns>The successful attempt.</returns>
        public static Attempt<T> Succeed()
        {
            return new Attempt<T>(true, default(T), null);
        }

        /// <summary>
        /// Creates a successful attempt with a result.
        /// </summary>
        /// <param name="result">The result of the attempt.</param>
        /// <returns>The successful attempt.</returns>
        public static Attempt<T> Succeed(T result)
        {
            return new Attempt<T>(true, result, null);
        }

        /// <summary>
        /// Creates a failed attempt.
        /// </summary>
        /// <returns>The failed attempt.</returns>
        public static Attempt<T> Fail()
        {
            return Failed;
        }

        /// <summary>
        /// Creates a failed attempt with an exception.
        /// </summary>
        /// <param name="exception">The exception causing the failure of the attempt.</param>
        /// <returns>The failed attempt.</returns>
        public static Attempt<T> Fail(Exception exception)
        {
            return new Attempt<T>(false, default(T), exception);
        }

        /// <summary>
        /// Creates a failed attempt with a result.
        /// </summary>
        /// <param name="result">The result of the attempt.</param>
        /// <returns>The failed attempt.</returns>
        public static Attempt<T> Fail(T result)
        {
            return new Attempt<T>(false, result, null);
        }

        /// <summary>
        /// Creates a failed attempt with a result and an exception.
        /// </summary>
        /// <param name="result">The result of the attempt.</param>
        /// <param name="exception">The exception causing the failure of the attempt.</param>
        /// <returns>The failed attempt.</returns>
        public static Attempt<T> Fail(T result, Exception exception)
        {
            return new Attempt<T>(false, result, exception);
        }

        /// <summary>
        /// Creates a successful or a failed attempt.
        /// </summary>
        /// <param name="condition">A value indicating whether the attempt is successful.</param>
        /// <returns>The attempt.</returns>
        public static Attempt<T> SucceedIf(bool condition)
        {
            return condition ? new Attempt<T>(true, default(T), null) : Failed;
        }

        /// <summary>
        /// Creates a successful or a failed attempt, with a result.
        /// </summary>
        /// <param name="condition">A value indicating whether the attempt is successful.</param>
        /// <param name="result">The result of the attempt.</param>
        /// <returns>The attempt.</returns>
        public static Attempt<T> SucceedIf(bool condition, T result)
        {
            return new Attempt<T>(condition, result, null);
        }

        /// <summary>
        /// Implicitly operator to check if the attempt was successful without having to access the 'success' property
        /// </summary>
        /// <param name="a">The attempt</param>
        /// <returns>A value indicating whether or not the attempt was successful</returns>
        public static implicit operator bool(Attempt<T> a)
        {
            return a.Success;
        }
    }
}