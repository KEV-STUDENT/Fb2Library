using Fb2Library.Domain.Books;
using Fb2Library.Domain.Tests.Shared;

namespace Fb2Library.Domain.Tests.Books
{
    public class BookInfoTests : ValueObjectTests<BookInfo, BookInfoVO>
    {
        protected override BookInfoVO Value1 => new("Test1", 1990, "Москва");
        protected override BookInfoVO Value2 => new("Test2", 1991, "Питер");

        public static IEnumerable<object[]> GetActualExpectedData()
        {
            yield return new object[] { new BookInfoVO("Test1 Test1", 1990, "Ь 123"), new BookInfoVO("Test1 Test1", 1990, "Ь 123") };
            yield return new object[] { new BookInfoVO("Test1  Test1", 1990, "Ь    123"), new BookInfoVO("Test1 Test1", 1990, "Ь 123") };
            yield return new object[] { new BookInfoVO("Test1 Test1", null!, "Ь    123"), new BookInfoVO("Test1 Test1", null!, "Ь 123") };
            yield return new object[] { new BookInfoVO("Test1 Test1", null!, null!), new BookInfoVO("Test1 Test1", null!, null!) };
            yield return new object[] { new BookInfoVO("Test1 Test1", null!, null!), new BookInfoVO("Test1 Test1", null!, "     ") };
        }
        protected override BookInfo Create(BookInfoVO value) => BookInfo.Create(value.Title, value.Year, value.City);

        [Theory]
        [MemberData(nameof(GetActualExpectedData))]
        public override void Value_ShouldFormatCorrectly(BookInfoVO actual, BookInfoVO expected)
             => Value_ShouldFormatCorrectly_Exec(actual, expected);

#pragma warning disable xUnit1013
        public override void Create_InvalidWord_ShouldThrowArgumentException(BookInfoVO code) => throw new NotImplementedException();
#pragma warning restore xUnit1013
    }
}
