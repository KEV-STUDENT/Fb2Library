using Fb2Library.Domain.Sequences;
using Fb2Library.Domain.Tests.Shared;

namespace Fb2Library.Domain.Tests.Sequences
{
    public class SequenceNameTests : ValueObjectTests<SequenceName, string>
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

        protected override SequenceName Create(string value) => SequenceName.Create(value);

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
