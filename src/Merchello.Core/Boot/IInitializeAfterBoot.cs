namespace Merchello.Core.Boot
{
    /// <summary>
    /// Represents an service or provider that requires Merchello to be completely booted (e.g. completely installed) 
    /// before it can finish loading or initializing.
    /// </summary>
    public interface IInitializeAfterBoot
    {
        /// <summary>
        /// Initializes the service or provider after Merchello .
        /// </summary>
        void InitializeAfterBoot();
    }

}