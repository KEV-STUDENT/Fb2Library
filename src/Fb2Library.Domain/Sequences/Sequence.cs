using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Sequences
{
    public sealed class Sequence : Entity<SequenceId, SequenceName>
    {
        private Sequence(SequenceName valueObject) : base(valueObject){}

        public static Sequence Create(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new DomainException("Sequence's name must be specified");

            return new(SequenceName.Create(name));
        }


        protected override SequenceId GetNewId() => SequenceId.New();
    }
}
