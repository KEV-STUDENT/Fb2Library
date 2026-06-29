using Fb2Library.Domain.Sequences;

namespace Fb2Library.Domain.Tests.Sequences
{
    public class SequenceIdTests : GuidIdentityTests<SequenceId>
    {
        protected override SequenceId ConvertFromGuid(Guid value)
        {
            SequenceId id = value;
            return id;
        }
        protected override SequenceId CreateFrom(Guid value) => SequenceId.From(value);
        protected override SequenceId CreateNew() => SequenceId.New();
    }
}
