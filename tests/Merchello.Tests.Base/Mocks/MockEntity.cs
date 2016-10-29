namespace Merchello.Tests.Base.Mocks
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;

    public class MockEntity : Entity
    {
        public string EntityValue { get; set; } = "Initial";
    }

    public class MockDeployableEntity : DeployableEntity
    {
        public string EntityValue { get; set; } = "Initial";
    }

    public class MockShallowCloneEntity : Entity, IShallowClone
    {
        public string EntityValue { get; set; } = "Initial";

        public object ShallowClone()
        {
            var obj = (Entity)this.MemberwiseClone();
            obj.ResetDirtyProperties();
            return obj;
        }
    }

    public class MockDeepCloneableEntity : Entity, IDeepCloneable
    {
        public string EntityValue { get; set; } = "Initial";

        public MockShallowCloneEntity InternalEntity { get; set; } = new MockShallowCloneEntity();

        public object DeepClone()
        {
            var entity = (MockDeepCloneableEntity)this.MemberwiseClone();
            entity.InternalEntity = (MockShallowCloneEntity)this.InternalEntity.ShallowClone();
            entity.ResetDirtyProperties();
            return entity;
        }
    }
}