namespace Merchello.Core.Persistence.Mappers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.DI;

    /// <inheritdoc/>
    internal class MapperRegister : Register<BaseMapper>, IMapperRegister
    {
        /// <summary>
        /// maintain our own index for faster lookup
        /// </summary>
        private readonly ConcurrentDictionary<Type, BaseMapper> _index = new ConcurrentDictionary<Type, BaseMapper>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MapperRegister"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public MapperRegister(IEnumerable<BaseMapper> items)
            : base(items)
        {
        }

        /// <inheritdoc/>
        public BaseMapper this[Type type]
        {
            get
            {
                return _index.GetOrAdd(
                    type,
                    t =>
                    {
                        // check if any of the mappers are assigned to this type
                        var mapper = this.FirstOrDefault(x => x.GetType()
                            .GetCustomAttributes<MapperForAttribute>(false)
                            .Any(m => m.EntityType == type));

                        if (mapper != null) return mapper;

                        throw new Exception($"Could not find a mapper matching type {type.FullName}.");
                    });
            }
        }
    }
}