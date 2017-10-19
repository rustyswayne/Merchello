namespace Merchello.Core.Boot
{
    /// <summary>
    /// Boots Merchello
    /// </summary>
    public interface IBoot
    {
        /// <summary>
        /// Boots Merchello.
        /// </summary>
        void Boot();

        /// <summary>
        /// Terminates Merchello
        /// </summary>
        void Terminate();
    }
}