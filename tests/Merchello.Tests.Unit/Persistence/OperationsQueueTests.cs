namespace Merchello.Tests.Unit.Persistence
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Persistence.Querying;

    using NUnit.Framework;

    [TestFixture]
    public class OperationsQueueTests
    {
        private Queue<Operation> _operations = new Queue<Operation>(); 

        [OneTimeSetUp]
        public void Initialize()
        {
            _operations.Enqueue(new Operation { Name = "1", Type = OperationType.Insert });
            _operations.Enqueue(new Operation { Name = "2", Type = OperationType.Update });
            _operations.Enqueue(new Operation { Name = "3", Type = OperationType.Insert });
            _operations.Enqueue(new Operation { Name = "4", Type = OperationType.Update });
            _operations.Enqueue(new Operation { Name = "5", Type = OperationType.Insert });
            _operations.Enqueue(new Operation { Name = "6", Type = OperationType.Update });
            _operations.Enqueue(new Operation { Name = "7", Type = OperationType.Update });
            _operations.Enqueue(new Operation { Name = "8", Type = OperationType.Insert });
        }

        [Test]
        public void Can_Group_Operations()
        {
            var group = _operations.ToList().GroupBy(o => new { o.Type });

            Assert.That(group.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// The types of unit of work operation.
        /// </summary>
        protected enum OperationType
        {
            /// <summary>
            /// Insert operation
            /// </summary>
            Insert,

            /// <summary>
            /// Update operation
            /// </summary>
            Update,

            /// <summary>
            /// Delete operation.
            /// </summary>
            Delete
        }

        /// <summary>
        /// Provides a snapshot of an entity and the repository reference it belongs to.
        /// </summary>
        protected sealed class Operation
        {
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the type of operation.
            /// </summary>
            /// <value>The type of operation.</value>
            public OperationType Type { get; set; }
        }
    }
}