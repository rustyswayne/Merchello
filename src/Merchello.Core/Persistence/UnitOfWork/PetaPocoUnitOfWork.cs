﻿using System;
using System.Collections.Generic;
using System.Linq;
using Merchello.Core.Models.EntityBase;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace Merchello.Core.Persistence.UnitOfWork
{
    using Merchello.Core.Persistence.Repositories;

    using Umbraco.Core.Persistence.UnitOfWork;

    /// <summary>
	/// Represents the Unit of Work implementation for PetaPoco
	/// </summary>
	internal class PetaPocoUnitOfWork : DisposableObject, IDatabaseUnitOfWork
	{

		/// <summary>
		/// Used for testing
		/// </summary>
		internal Guid InstanceId { get; private set; }

		private Guid _key;
        private readonly Queue<Operation> _operations = new Queue<Operation>();

		/// <summary>
		/// Creates a new unit of work instance
		/// </summary>
		/// <param name="database"></param>
		/// <remarks>
		/// This should normally not be used directly and should be created with the UnitOfWorkProvider
		/// </remarks>
		internal PetaPocoUnitOfWork(UmbracoDatabase database)
		{
			Database = database;
			_key = Guid.NewGuid();
			InstanceId = Guid.NewGuid();
		}

		/// <summary>
		/// Registers an <see cref="IEntity" /> instance to be added through this <see cref="Umbraco.Core.Persistence.UnitOfWork" />
		/// </summary>
		/// <param name="entity">The <see cref="IEntity" /></param>
		/// <param name="repository">The <see cref="IUnitOfWorkRepository" /> participating in the transaction</param>
		public void RegisterAdded(IEntity entity, IUnitOfWorkRepository repository)
		{
            _operations.Enqueue(new Operation
            {
                Entity = entity,
                Repository = repository,
                Type = TransactionType.Insert
            });
		}

		/// <summary>
		/// Registers an <see cref="IEntity" /> instance to be changed through this <see cref="UnitOfWork" />
		/// </summary>
		/// <param name="entity">The <see cref="IEntity" /></param>
		/// <param name="repository">The <see cref="IUnitOfWorkRepository" /> participating in the transaction</param>
		public void RegisterChanged(IEntity entity, IUnitOfWorkRepository repository)
		{
		    _operations.Enqueue(
		        new Operation
		        {
		            Entity = entity,
		            Repository = repository,
		            Type = TransactionType.Update
		        });
		}

		/// <summary>
		/// Registers an <see cref="IEntity" /> instance to be removed through this <see cref="UnitOfWork" />
		/// </summary>
		/// <param name="entity">The <see cref="IEntity" /></param>
		/// <param name="repository">The <see cref="IUnitOfWorkRepository" /> participating in the transaction</param>
		public void RegisterRemoved(IEntity entity, IUnitOfWorkRepository repository)
		{
		    _operations.Enqueue(
		        new Operation
		        {
		            Entity = entity,
		            Repository = repository,
		            Type = TransactionType.Delete
		        });
		}

		/// <summary>
		/// Commits all batched changes within the scope of a PetaPoco transaction <see cref="Transaction"/>
		/// </summary>
		/// <remarks>
		/// Unlike a typical unit of work, this UOW will let you commit more than once since a new transaction is created per
		/// Commit() call instead of having one Transaction per UOW. 
		/// </remarks>
		public void Commit()
        {
		    Commit(null);
		}

        /// <summary>
        /// The Bulk Commit.
        /// </summary>
        /// <typeparam name="TEntity">
        /// The type of the entity
        /// </typeparam>
        public void CommitBulk<TEntity>() where TEntity : IEntity
        {
            CommitBulk<TEntity>(null);
        }

        /// <summary>
        /// Commits all batched changes within the scope of a PetaPoco transaction <see cref="Transaction"/>
        /// </summary>
        /// <param name="transactionCompleting">
        /// Allows you to set a callback which is executed before the transaction is committed, allow you to add additional SQL
        /// operations to the overall commit process after the queue has been processed.
        /// </param>
        internal void Commit(Action<UmbracoDatabase> transactionCompleting)
        {
            using (var transaction = Database.GetTransaction())
            {
                while (_operations.Count > 0)
                {
                    var operation = _operations.Dequeue();
                    switch (operation.Type)
                    {
                        case TransactionType.Insert:
                            operation.Repository.PersistNewItem(operation.Entity);
                            break;
                        case TransactionType.Delete:
                            operation.Repository.PersistDeletedItem(operation.Entity);
                            break;
                        case TransactionType.Update:
                            operation.Repository.PersistUpdatedItem(operation.Entity);
                            break;
                    }
                }

                //Execute the callback if there is one
                if (transactionCompleting != null)
                {
                    transactionCompleting(Database);    
                }

                transaction.Complete();
            }

            // Clear everything
            _operations.Clear();
            _key = Guid.NewGuid();
        }


        /// <summary>
        /// The commit bulk.
        /// </summary>
        /// <param name="transactionCompleting">
        /// The transaction completing.
        /// </param>
        /// <typeparam name="TEntity">
        /// The type of the entity
        /// </typeparam>
        internal void CommitBulk<TEntity>(Action<UmbracoDatabase> transactionCompleting) where TEntity : IEntity
        {
            if (_operations.Any(o => !(o.Repository is IBulkOperationRepository<TEntity>))) Commit(transactionCompleting);

            using (var transaction = Database.GetTransaction())
            {
                var operations = _operations.ToList();
                foreach (var operationGroup in operations.GroupBy(o => new { o.Repository, o.Type }))
                {
                    switch (operationGroup.Key.Type)
                    {
                        case TransactionType.Insert:
                            ((IBulkOperationRepository<TEntity>)operationGroup.Key.Repository).PersistNewItems(operationGroup.Select(o => (TEntity)o.Entity));
                            break;
                        case TransactionType.Update:
                            ((IBulkOperationRepository<TEntity>)operationGroup.Key.Repository).PersistUpdatedItems(operationGroup.Select(o => (TEntity)o.Entity));
                            break;
                        default:
                            throw new InvalidOperationException("Only Inserts and Updates are supported");
                            break;
                    }
                }

                _operations.Clear();

                //Execute the callback if there is one
                if (transactionCompleting != null)
                {
                    transactionCompleting(Database);
                }

                transaction.Complete();
            }

            // Clear everything
            _operations.Clear();
            _key = Guid.NewGuid();
        }

		public object Key
		{
			get { return _key; }
		}

		public UmbracoDatabase Database { get; private set; }

		#region Operation

		/// <summary>
		/// Provides a snapshot of an entity and the repository reference it belongs to.
		/// </summary>
		private sealed class Operation
		{
			/// <summary>
			/// Gets or sets the entity.
			/// </summary>
			/// <value>The entity.</value>
			public IEntity Entity { get; set; }

			/// <summary>
			/// Gets or sets the repository.
			/// </summary>
			/// <value>The repository.</value>
			public IUnitOfWorkRepository Repository { get; set; }

			/// <summary>
			/// Gets or sets the type of operation.
			/// </summary>
			/// <value>The type of operation.</value>
			public TransactionType Type { get; set; }
		}

		#endregion

		/// <summary>
		/// Ensures disposable objects are disposed
		/// </summary>		
		/// <remarks>
		/// Ensures that the Transaction instance is disposed of
		/// </remarks>
		protected override void DisposeResources()
		{
			_operations.Clear();
		}
	}
}