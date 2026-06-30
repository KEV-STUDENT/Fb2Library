using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Sequences
{
    public sealed record SequenceName : StringValueObject
    {
        private SequenceName(string value) : base(value){}
        public static SequenceName Create(string value) => new(value);
        public override string ToString() => Value;
    }
}
