using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Persons
{
    public sealed record PersonName : ValueObject<PersonNameVO>
    {
        private PersonName(PersonNameVO value) : base(value) { }


        public static PersonName Create(string firstName, string lastName, string? middleName = null, string? nickName = null)
            => new(new PersonNameVO(firstName, lastName, middleName, nickName));


        public string FirstName => Value.FirstName;
        public string LastName => Value.LastName;
        public string? MiddleName => Value.MiddleName;
        public string? NickName => Value.NickName;

        public string FullName => string.Join(" ", new[] { LastName, FirstName, MiddleName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        public string ShortName => string.Join(" ", new[] { FirstName, LastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        public string SortName => string.Join(" ", new[] { LastName, FirstName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        public override string ToString() => Value.ToString();


        protected override PersonNameVO NormalizeValue(PersonNameVO code)
        {
            code.LastName = PersonNameVO.NormalizeName(code.LastName);
            code.FirstName = PersonNameVO.NormalizeName(code.FirstName);
            if (code.MiddleName != null)
                code.MiddleName = PersonNameVO.NormalizeName(code.MiddleName);
            if (code.NickName != null)
                code.NickName = PersonNameVO.NormalizeName(code.NickName);
            return code;
        }

        protected override bool IsNullOrEmty(PersonNameVO code) => PersonNameVO.IsNullOrEmpty(code);
        protected override bool EqualsWith(ValueObject<PersonNameVO>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Value == other.Value;
        }
        protected override int GetHashCodeFromValue() => Value.GetHashCodeFromValue();
    }

    public sealed record PersonNameVO
    {
        public PersonNameVO(string firstName, string lastName, string? middleName = null, string? nickName = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Value cannot be empty", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Value cannot be empty", nameof(lastName));

            FirstName = NormalizeName(firstName);
            LastName = NormalizeName(lastName);

            if (string.IsNullOrWhiteSpace(middleName))
                MiddleName = middleName?.Trim();
            else
                MiddleName = NormalizeName(middleName);

            NickName = nickName?.Trim();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? NickName { get; set; }

        internal int GetHashCodeFromValue() => HashCode.Combine(FirstName, LastName, MiddleName, NickName);
        public static string NormalizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;

            char separator;

            name = name.Trim();
            if (name.Contains('-'))
                separator = '-';
            else if (name.Contains(' '))
                separator = ' ';
            else
                return char.ToUpper(name[0]) + name[1..].ToLower();

            IEnumerable<string> parts = name.Split(separator)
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select( p=> NormalizeName(p));

            return string.Join(separator, parts);
        }

        internal static bool IsNullOrEmpty(PersonNameVO code) => code == null || string.IsNullOrWhiteSpace(code.FirstName) || string.IsNullOrWhiteSpace(code.LastName);

         public override string ToString() => string.Join(" ", new[] { LastName, FirstName, MiddleName, NickName }.Where(x => !string.IsNullOrWhiteSpace(x)));
    }
}
