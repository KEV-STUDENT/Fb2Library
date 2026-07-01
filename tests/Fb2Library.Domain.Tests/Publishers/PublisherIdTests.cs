using Fb2Library.Domain.Publishers;
using Fb2Library.Domain.Tests.Shared;

namespace Fb2Library.Domain.Tests.Publishers
{
    public class PublisherIdTests : GuidIdentityTests<PublisherId>
    {
        protected override PublisherId ConvertFromGuid(Guid value)
        {
            PublisherId id = value;
            return id;
        }
        protected override PublisherId CreateFrom(Guid value) => PublisherId.From(value);
        protected override PublisherId CreateNew() => PublisherId.New();
    }
}
