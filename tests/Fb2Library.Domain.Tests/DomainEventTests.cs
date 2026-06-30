using Fb2Library.Domain.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests
{
    public class DomainEventTests
    {
        [Fact]
        public void EveryEvent_ShouldHaveUniqueEventId()
        {
            var event1 = new Test1CreatedDomainEvent(Test1Id.New());
            var event2 = new Test1CreatedDomainEvent(Test1Id.New());
            var event3 = new Test2CreatedDomainEvent(Test2Id.New());

            Assert.NotEqual(event1.Id, event2.Id);
            Assert.NotEqual(event1.Id, event3.Id);
        }

        [Fact]
        public void EveryEvent_ShouldHaveOccurredOnSet()
        {
            var event1 = new Test1CreatedDomainEvent(Test1Id.New());
            Assert.NotEqual(default, event1.OccurredOn);
        }

        [Fact]
        public void EventId_ShouldNotBeEmpty()
        {
            var event1 = new Test1CreatedDomainEvent(Test1Id.New());
            Assert.NotEqual(Guid.Empty, event1.TestId.Value);
        }

        [Fact]
        public void Create_WithNullBookId_ShouldThrowException()
        {
            Func<Test1CreatedDomainEvent> act = () => new Test1CreatedDomainEvent(null!); ;

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*value*");
        }
    }

    internal sealed record Test1CreatedDomainEvent(Test1Id TestId) : CreateEvent<Test1Id>(TestId) { }

    internal sealed record Test1Id : GuidIdentity, IIdentityFabric<Test1Id, Guid>
    {
        public Test1Id(Guid value) : base(value){}
        public static Test1Id From(Guid value) => new(value);
        public static Test1Id New() => From(Guid.NewGuid());
        public static implicit operator Test1Id(Guid value) => From(value);
    }

    internal sealed record Test2CreatedDomainEvent(Test2Id TestId) : CreateEvent<Test2Id>(TestId) { }

    internal sealed record Test2Id : GuidIdentity, IIdentityFabric<Test2Id, Guid>
    {
        public Test2Id(Guid value) : base(value) { }
        public static Test2Id From(Guid value) => new(value);
        public static Test2Id New() => From(Guid.NewGuid());
    }
}
