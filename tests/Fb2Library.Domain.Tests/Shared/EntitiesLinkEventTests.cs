using Fb2Library.Domain.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests.Shared
{
    internal sealed record EntitiesLinkEvent4Test(Test1Id EntityId4Test1, Test2Id EntityId4Test2) : EntitiesLinkEvent<Test1Id, Test2Id>(EntityId4Test1, EntityId4Test2) { }
    public class EntitiesLinkEventTests
    {
        [Fact]
        public void EntityId_ShouldNotBeEmpty()
        {
            var event1 = new EntitiesLinkEvent4Test(Test1Id.New(), Test2Id.New());
            Assert.NotEqual(Guid.Empty, event1.EntityId4Test1.Value);
            Assert.NotEqual(Guid.Empty, event1.EntityId4Test2.Value);
        }

        [Fact]
        public void Create_WithNullEntityId1_ShouldThrowException()
        {
            Func<EntitiesLinkEvent4Test> act = () => new EntitiesLinkEvent4Test(null!, Test2Id.New());
            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*value*");
        }

        [Fact]
        public void Create_WithNullEntityId2_ShouldThrowException()
        {
            Func<EntitiesLinkEvent4Test> act = () => new EntitiesLinkEvent4Test(Test1Id.New(), null!);
            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*value*");
        }

        [Fact]
        public void WithSameBusinessProperties_ShouldBeEqual()
        {
            var id1 = Test1Id.New();
            var id2 = Test2Id.New();

            var event1 = new EntitiesLinkEvent4Test(id1, id2);
            var event2 = new EntitiesLinkEvent4Test(id1, id2);
            Assert.Equal(event1, event2);
        }
    }
}
