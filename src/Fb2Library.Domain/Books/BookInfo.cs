using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Books
{
    public sealed record BookInfo : ValueObject<BookInfoVO>
    {
        private BookInfo(BookInfoVO value) : base(value) { }
        public static BookInfo Create(string title, uint? year, string? city) => new(new BookInfoVO(title, year, city));

        public string Title => Value.Title;
        public uint? Year => Value.Year;
        public string? City => Value.City;

        protected override bool EqualsWith(ValueObject<BookInfoVO>? other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.EqualsWith(other.Value);
        }
        protected override int GetHashCodeFromValue() => Value.GetHashCodeFromValue();
        protected override bool IsNullOrEmty(BookInfoVO? code) => BookInfoVO.IsNullOrEmpty(code);
        protected override BookInfoVO NormalizeValue(BookInfoVO bookInfo)
        {
            bookInfo.Title = BookInfoVO.NormalizeString(bookInfo.Title) ?? throw new ArgumentException("Incorrect Title");
            bookInfo.City = BookInfoVO.NormalizeString(bookInfo.City);
            return bookInfo;
        }
        public override string ToString() => Value.ToString();
    }

    public sealed record BookInfoVO
    {
        public BookInfoVO(string title, uint? year, string? city)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Value cannot be empty", nameof(title));

            Title = title;
            Year = year;
            City = string.IsNullOrWhiteSpace(city) ? null : city;
        }

        public string Title { get; set; }
        public uint? Year { get; set; }
        public string? City { get; set; }

        internal static bool IsNullOrEmpty(BookInfoVO? code)
            => code == null || (string.IsNullOrWhiteSpace(code.Title) && code.Year == 0 && string.IsNullOrWhiteSpace(code.City));
        internal int GetHashCodeFromValue() => HashCode.Combine(Title, Year, City);

        public bool EqualsWith(BookInfoVO? other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Year == other.Year
                && ((string.IsNullOrWhiteSpace(City) && string.IsNullOrWhiteSpace(other.City))
                    || (City?.Equals(other.City, StringComparison.InvariantCultureIgnoreCase) ?? false))
                && Title.Equals(other.Title, StringComparison.InvariantCultureIgnoreCase);
        }

        internal static string? NormalizeString(string? value)
        {
            if(string.IsNullOrWhiteSpace(value)) return null;

            return RegexPatterns.WhitespaceRegex().Replace(value, " ").Trim();
        }

        public override string ToString() => string.Join(" ", new[] {Title, City, Year.ToString()}.Where(x => !string.IsNullOrWhiteSpace(x)));
    }
}
