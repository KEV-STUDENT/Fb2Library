using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Publishers
{
    public sealed record PublisherName : StringValueObject
    {
        private PublisherName(string value) : base(value){}
        public static PublisherName Create(string name) => new(name);
        public override string ToString() => Value;
    }
}
