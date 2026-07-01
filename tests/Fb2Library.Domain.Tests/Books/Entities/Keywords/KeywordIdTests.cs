using Fb2Library.Domain.Books.Entities.Keywords;
using Fb2Library.Domain.Tests.Shared;

namespace Fb2Library.Domain.Tests.Books.Entities.Keywords
{
    public class KeywordIdTests : GuidIdentityTests<KeywordId>
    {
        protected override KeywordId ConvertFromGuid(Guid value)
        {
            KeywordId id = value;
            return id;
        }
        protected override KeywordId CreateFrom(Guid value) => KeywordId.From(value);
        protected override KeywordId CreateNew() => KeywordId.New();
    }
}
