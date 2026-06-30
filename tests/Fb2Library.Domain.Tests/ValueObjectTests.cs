using Fb2Library.Domain.Shared;
using FluentAssertions;

namespace Fb2Library.Domain.Tests
{
    public abstract class ValueObjectTests<T1,T2>
        where T1 : ValueObject<T2>
    {
        protected abstract T2 Value1 { get; }
        protected abstract T2 Value2 { get; }
        protected abstract T1 Create(T2 value);

        [Fact]
        protected void Create_WithValidValue_ShouldSetProperties()
        {
            // Arrange & Act
            T1 code = Create(Value1);

            // Assert
            code.Value.Should().Be(Value1);
        }

        [Fact]
        public void TwoInstances_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            T1 name1 = Create(Value1);
            T1 name2 = Create(Value1);

            // Assert
            name1.Should().Be(name2);
            name1.GetHashCode().Should().Be(name2.GetHashCode());
        }

        [Fact]
        public void TwoInstances_WithDifferentValues_ShouldNotBeEqual()
        {
            // Arrange
            T1 name1 = Create(Value1);
            T1 name2 = Create(Value2);

            // Assert
            name1.Should().NotBe(name2);
        }

        [Fact]
        public void ToString_ShouldBeValue()
        {
            // Arrange & Act
            T1 code = Create(Value1);

            // Assert
            code.ToString().Should().Be(Value1!.ToString());
        }

        public abstract void Create_InvalidWord_ShouldThrowArgumentException(T2 code);
        protected void Create_InvalidWord_ShouldThrowArgumentException_Exec(T2 code)
        {
            // Act
            Func<T1> act = () => Create(code);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("*value*");
        }

        public abstract void Value_ShouldFormatCorrectly(T2 actual, T2 expected);
        protected void Value_ShouldFormatCorrectly_Exec(T2 actual, T2 expected)
        {
            // Act
            T1 name = Create(actual);
            // Assert
            name.Value.Should().Be(expected);
        }
    }
}
