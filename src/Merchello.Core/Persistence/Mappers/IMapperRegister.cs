namespace Merchello.Core.Persistence.Mappers
{
    using System;

    using Merchello.Core.DI;

    /// <summary>
    /// Represents a register for <see cref="BaseMapper"/>.
    /// </summary>
    public interface IMapperRegister : IRegister<BaseMapper>
    {
        /// <summary>
        /// Indexed property for the <see cref="BaseMapper"/>.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="BaseMapper"/>.
        /// </returns>
        BaseMapper this[Type type] { get; }
    }
}
