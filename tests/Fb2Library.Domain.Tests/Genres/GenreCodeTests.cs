using Fb2Library.Domain.Genres;
using Fb2Library.Domain.Tests.Shared;

namespace Fb2Library.Domain.Tests.Genres
{
    public class GenreCodeTests : ValueObjectTests<GenreCode, string>
    {
        protected override string Value1 => "sf";
        protected override string Value2 => "dt";
        public static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { null! };
        }
        public static IEnumerable<object[]> GetActualExpectedData()
        {
            yield return new object[] { "sf", "sf" };
            yield return new object[] { "Sf", "sf" };
            yield return new object[] { "SF", "sf" };
            yield return new object[] { "sF", "sf" };
            yield return new object[] { "  sf  ", "sf" };
            yield return new object[] { "  sF", "sf" };
            yield return new object[] { "Sf  ", "sf" };
        }
        protected override GenreCode Create(string value) => GenreCode.Create(value);

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public override void Create_InvalidWord_ShouldThrowArgumentException(string code)
            => Create_InvalidWord_ShouldThrowArgumentException_Exec(code);

        [Theory]
        [MemberData(nameof(GetActualExpectedData))]
        public override void Value_ShouldFormatCorrectly(string actual, string expected)
            => Value_ShouldFormatCorrectly_Exec(actual, expected);
    }
}
