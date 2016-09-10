namespace Merchello.Core.Acquired.Logging
{
    /// <summary>
    /// Borrowed from https://github.com/cjbhaines/Log4Net.Async - will reference Nuget packages directly in v8
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// UMBRACO_SRC
    /// REFACTOR-remove when Umbraco V8 is released
    internal interface IQueue<T>
    {
        void Enqueue(T item);
        bool TryDequeue(out T ret);
    }
}