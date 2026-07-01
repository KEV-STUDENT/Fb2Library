using Fb2Library.Domain.Keywords;
using Fb2Library.Domain.Publishers;
using Fb2Library.Domain.Tests.Shared;

namespace Fb2Library.Domain.Tests.Publishers
{

    public class PublisherNameTests : ValueObjectTests<PublisherName, string>
    {
        protected override string Value1 => "test1";
        protected override string Value2 => "test2";
        public static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { null! };
        }
        public static IEnumerable<object[]> GetActualExpectedData()
        {
            yield return new object[] { "tEst", "tEst" };
            yield return new object[] { "TEST", "TEST" };
            yield return new object[] { "Test", "Test" };
            yield return new object[] { "  Test", "Test" };
            yield return new object[] { "  Test  ", "Test" };
            yield return new object[] { "Test  ", "Test" };
        }

        protected override PublisherName Create(string value) => PublisherName.Create(value);

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
