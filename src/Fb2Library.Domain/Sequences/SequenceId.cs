using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Sequences
{
    public sealed record SequenceId : GuidIdentity
    {
        public SequenceId(Guid value) : base(value) { }

        public static SequenceId From(Guid value) => new(value);
        public static SequenceId New() => new(Guid.NewGuid());

        public static implicit operator SequenceId(Guid value) => From(value);
    }
}
