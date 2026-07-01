using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Publishers
{
    public sealed class Publisher : AggregateRoot<PublisherId, PublisherName>
    {
        public Publisher(PublisherName publisherName) : base(publisherName) {}

        protected override PublisherId GetNewId() => PublisherId.New();

        public static Publisher Create(string publisher)
        {
            if (string.IsNullOrWhiteSpace(publisher)) throw new DomainException("Publisher's name must be specified");

            return new Publisher((PublisherName.Create(publisher)));
        }
    }
}
