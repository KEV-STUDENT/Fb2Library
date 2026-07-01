using Fb2Library.Domain.Sequences;
using Fb2Library.Domain.Tests.Shared;

namespace Fb2Library.Domain.Tests.Sequences
{
    public class SequenceTests : EntityTests<Sequence, SequenceId, SequenceName>
    {
        protected override Sequence CreateEntity() => Sequence.Create("Test");
        protected override SequenceName CreateValueObject() => SequenceName.Create("Test");
    }
}
