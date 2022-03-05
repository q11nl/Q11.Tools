using FluentAssertions;
using Q11.Tools.Conversion;
using Q11.Tools.Tests.Support;
using Xunit;

namespace Q11.Tools.Tests.Conversion.Handlers;

public class StringToStringHandler
{
    [Fact]
    public void NonNullableStringToNonNullableString_ReturnsInput()
    {
        var actual = TestValues.StringNonNullable.ChangeType<string>();

        actual.Should().Be(TestValues.StringNonNullable);
    }

    [Fact]
    public void NullableStringToNullableString_ReturnsInput()
    {
        var actual = TestValues.StringNullable.ChangeType<string?>();

        actual.Should().Be(TestValues.StringNullable);
    }

    [Fact]
    public void NonNullableStringToNullableString_ReturnsInput()
    {
        var actual = TestValues.StringNonNullable.ChangeType<string>();
        string? expected = TestValues.StringNonNullable;
        actual.Should().Be(expected);
    }

    [Fact]
    public void NullableStringToNonNullableString_ReturnsInput()
    {
        var actual = TestValues.StringNullable.ChangeType<string?>();

        string expected = TestValues.StringNullable!;
        actual.Should().Be(expected);
    }
}