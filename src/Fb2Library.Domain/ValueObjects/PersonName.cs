using Fb2Library.Domain.Exceptions;

namespace Fb2Library.Domain.ValueObjects
{
    public class PersonName : IEquatable<PersonName>
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string? _middleName;
        private readonly string? _nickName;

        public PersonName(string firstName, string lastName, string? middleName = null, string? nickName = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First Name must be specified.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Last Name must be specified.");

            _firstName = NormalizeName(firstName);
            _lastName = NormalizeName(lastName);

            if (string.IsNullOrWhiteSpace(middleName))
                _middleName = middleName?.Trim();
            else
                _middleName = NormalizeName(middleName);

            _nickName = nickName?.Trim();
        }

        public string FirstName => _firstName;
        public string LastName => _lastName;
        public string? MiddleName => _middleName;
        public string? NickName => _nickName;
        public string FullName => string.Join(" ", new[] {LastName,  FirstName, MiddleName}.Where(x => !string.IsNullOrWhiteSpace(x)));
        public string ShortName => string.Join(" ", new[] { FirstName, LastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        public string SortName => string.Join(" ", new[] { LastName, FirstName }.Where(x => !string.IsNullOrWhiteSpace(x)));

        bool IEquatable<PersonName>.Equals(PersonName? other) => Equals(other);

        public override bool Equals(object? obj) => Equals(obj as PersonName);

        public bool Equals(PersonName? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(LastName, other.LastName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(MiddleName, other.MiddleName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(NickName, other.NickName, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode() => HashCode.Combine(FirstName, LastName, MiddleName, NickName);

        public override string ToString() => FullName;

        public static bool operator ==(PersonName? left, PersonName? right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(PersonName? left, PersonName? right)
        {
            return !(left == right);
        }

        private static string NormalizeName(string name)
        {
            name = name.Trim();
            return char.ToUpper(name[0]) + name[1..].ToLower();
        }
    }
}
