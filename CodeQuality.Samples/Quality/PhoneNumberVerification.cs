using FluentAssertions;

namespace CodeQuality.Samples.Quality;

public class PhoneNumberVerification
{
    public static bool v(string t)
    {
        if (t == null)
            return false;
        if (t.Length < 8 || t.Length > 15)
            return false;
        if (!t.StartsWith("+"))
            return false;
        for (var i = 1; i < t.Length; i++)
            if (!int.TryParse(t[i].ToString(), out _))
                return false;

        return true;
    }
}

public class PhoneNumberVerificationTest
{
    [Fact]
    public void ShouldReturnFalse_GivenValueIsNull() => PhoneNumberVerification.v(null).Should().BeFalse();
    
    [Fact]
    public void ShouldReturnFalse_GivenValueIsEmpty() => PhoneNumberVerification.v(string.Empty).Should().BeFalse();
    
    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    [InlineData("1234567")]
    public void ShouldReturnFalse_GivenLengthIsLowerThan8(string input) => PhoneNumberVerification.v(input).Should().BeFalse();
    
    [Theory]
    [InlineData("1234567890123456")]
    public void ShouldReturnFalse_GivenLengthIsHigherThan15(string input) => PhoneNumberVerification.v(input).Should().BeFalse();
    
    [Fact]
    public void ShouldReturnFalse_GivenDoesNotStartWithPlusSign() => PhoneNumberVerification.v("123456789").Should().BeFalse();
    
    [Fact]
    public void ShouldReturnFalse_GivenNotAllCharactersAreNumerics() => PhoneNumberVerification.v("+1a34!6789").Should().BeFalse();
    
    [Fact]
    public void ShouldReturnTrue_GivenValueIsValidE164() => PhoneNumberVerification.v("+123456789").Should().BeTrue();
}