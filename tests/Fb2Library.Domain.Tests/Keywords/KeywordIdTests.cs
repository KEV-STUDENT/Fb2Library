using Fb2Library.Domain.Keywords;

namespace Fb2Library.Domain.Tests.Keywords
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
