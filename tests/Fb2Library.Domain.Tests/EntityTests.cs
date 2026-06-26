using Fb2Library.Domain.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests
{
    public abstract class EntityTests<T1,T2>
        where T1 : Entity<T2>
        where T2 : GuidIdentity
    {

        protected abstract T1 CreateEntity();

        [Fact]
        public void Create_ShouldbProperTypeNotNull()
        {
            // Act
            T1 entity = CreateEntity();

            // Assert
            entity.Should().BeOfType<T1>().And.NotBeNull();
            entity.Id.Value.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_TwoEntities_IdsAreNotEqual()
        {
            // Arrange & Act
            T1 entity1 = CreateEntity();
            T1 entity2 = CreateEntity();

            // Assert
            entity1.Id.Should().NotBe(entity2.Id);
        }
    }
}
