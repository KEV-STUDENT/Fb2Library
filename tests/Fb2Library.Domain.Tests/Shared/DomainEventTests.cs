using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Tests.Shared
{
    internal sealed record Test2DomainEvent : DomainEvent
    {
        protected override IEnumerable<object> GetBusinessEqualityComponents() => throw new NotImplementedException();
    }

    public class DomainEventTests
    {
        [Fact]
        public void Event_ShouldHaveUniqueEventId()
        {
            var event1 = new Test2DomainEvent();
            var event2 = new Test2DomainEvent();

            Assert.NotEqual(event1.Id, event2.Id);
        }

        [Fact]
        public void EventId_ShouldNotBeEmptyOrDefault()
        {
            var event1 = new Test2DomainEvent();
            Assert.NotEqual(Guid.Empty, event1.Id.Value);
            Assert.NotEqual(default, event1.Id.Value);
        }

        [Fact]
        public void Event_ShouldHaveOccurredOnSet()
        {
            var event1 = new Test2DomainEvent();
            Assert.NotEqual(default, event1.OccurredOn);
        }
    }
}
