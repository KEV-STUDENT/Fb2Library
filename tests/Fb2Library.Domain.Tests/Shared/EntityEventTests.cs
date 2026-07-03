using Fb2Library.Domain.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Shared
{
    internal sealed record EntityEvent4Test(Test1Id EntityId4Test) : EntityEvent<Test1Id>(EntityId4Test) { }
    public class EntityEventTests
    {
        [Fact]
        public void EntityId_ShouldNotBeEmpty()
        {
            var event1 = new EntityEvent4Test(Test1Id.New());
            Assert.NotEqual(Guid.Empty, event1.EntityId4Test.Value);
        }

        [Fact]
        public void Create_WithNullEntityId_ShouldThrowException()
        {
            Func<EntityEvent4Test> act = () => new EntityEvent4Test(null!); ;

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*value*");
        }

        [Fact]
        public void WithSameBusinessProperties_ShouldBeEqual()
        {
            var id = Test1Id.New();
            var event1 = new EntityEvent4Test(id);
            var event2 = new EntityEvent4Test(id);
            Assert.Equal(event1, event2);
        }
    }    
}
