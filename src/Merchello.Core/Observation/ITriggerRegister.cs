namespace Merchello.Core.Observation
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.DI;

    /// <summary>
    /// Represents a register for <see cref="ITrigger"/> classes.
    /// </summary>
    public interface ITriggerRegister : IRegister<ITrigger>
    {
        /// <summary>
        /// Gets a collection of <see cref="ITrigger"/> by the area defined in the attribute
        /// </summary>
        /// <param name="topic">The "area"</param>
        /// <returns>A <see cref="ITrigger"/></returns>
        IEnumerable<ITrigger> GetTriggersByArea(Topic topic);

        /// <summary>
        /// Gets a collection <see cref="ITrigger"/> from the resolver
        /// </summary>
        /// <param name="alias">
        /// The alias.
        /// </param>
        /// <param name="topic">
        /// The topic.
        /// </param>
        /// <returns>
        /// A <see cref="ITrigger"/>
        /// </returns>
        /// <remarks>
        /// 
        /// By design there should only ever be one of these per alias, but someone might think
        /// of something we haven't
        /// 
        /// </remarks>
        IEnumerable<ITrigger> GetTriggersByAlias(string alias, Topic topic = Topic.Notifications);

        /// <summary>
        /// Gets the collection of all registered <see cref="ITrigger"/>s
        /// </summary>
        /// <typeparam name="T">
        /// The type of the trigger.
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        IEnumerable<T> GetAllTriggers<T>();

        /// <summary>
        /// Gets the collection of all registered <see cref="ITrigger"/>s
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{ITrigger}"/>.
        /// </returns>
        IEnumerable<ITrigger> GetAllTriggers();

        /// <summary>
        /// Gets a <see cref="ITrigger"/> from the resolver
        /// </summary>
        /// <typeparam name="T">
        /// The type of the trigger.
        /// </typeparam>
        /// <returns>
        /// A <see cref="ITrigger"/>
        /// </returns>
        T GetTrigger<T>();

        /// <summary>
        /// Gets a <see cref="ITrigger"/> from the resolver
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// A <see cref="ITrigger"/>
        /// </returns>
        ITrigger GetTrigger(Type type);
    }
}