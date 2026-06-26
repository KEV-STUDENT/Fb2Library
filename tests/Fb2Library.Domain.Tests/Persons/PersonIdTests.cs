using Fb2Library.Domain.Persons;

namespace Fb2Library.Domain.Tests.Persons
{
    public class PersonIdTests : GuidIdentityTests<PersonId>
    {
        protected override PersonId ConvertFromGuid(Guid value)
        {
            PersonId id = value;
            return id;
        }
        protected override PersonId CreateFrom(Guid value) => PersonId.From(value);
        protected override PersonId CreateNew() => PersonId.New();
    }
}
