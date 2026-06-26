namespace Fb2Library.Domain.Persons
{
    public sealed record PersonName
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string? _middleName;
        private readonly string? _nickName;

        private PersonName(string firstName, string lastName, string? middleName = null, string? nickName = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Value cannot be empty", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Value cannot be empty", nameof(lastName));

            _firstName = NormalizeName(firstName);
            _lastName = NormalizeName(lastName);

            if (string.IsNullOrWhiteSpace(middleName))
                _middleName = middleName?.Trim();
            else
                _middleName = NormalizeName(middleName);

            _nickName = nickName?.Trim();
        }

        public static PersonName Create(string firstName, string lastName, string? middleName = null, string? nickName = null)
        {
            return new PersonName(firstName, lastName, middleName, nickName);
        }

        public string FirstName {
            get => _firstName;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Value cannot be empty");
                _firstName = NormalizeName(value);
            }
        }
        public string LastName {
            get => _lastName;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Value cannot be empty");
                _lastName = NormalizeName(value);
            }
        }
        public string? MiddleName {
            get => _middleName;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                    _middleName = value?.Trim();
                else
                    _middleName = NormalizeName(value);
            }
        }

        public string? NickName => _nickName;
        public string FullName => string.Join(" ", new[] { LastName, FirstName, MiddleName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        public string ShortName => string.Join(" ", new[] { FirstName, LastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        public string SortName => string.Join(" ", new[] { LastName, FirstName }.Where(x => !string.IsNullOrWhiteSpace(x)));

        public override string ToString() => FullName;

        private static string NormalizeName(string name)
        {
            name = name.Trim();
            return char.ToUpper(name[0]) + name[1..].ToLower();
        }

        // don't use FullName, ShortName & SortName when check Equals
        public bool Equals(PersonName? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return _firstName == other._firstName &&
                   _lastName == other._lastName &&
                   _middleName == other._middleName &&
                   _nickName == other._nickName;
        }

        public override int GetHashCode() => HashCode.Combine(_firstName, _lastName, _middleName, _nickName);
    }
}
