using System;
using FluentAssertions;
using Q11.Tools.Conversion;
using Q11.Tools.Tests.Support;
using Xunit;

namespace Q11.Tools.Tests.Conversion.Handlers;

public class NonStringToStringHandlerTests
{
    [Fact]
    public void EnumToString_ReturnsEnumString()
    {
        var input = DayOfWeek.Monday;

        var actual = input.ChangeType<string>();

        actual.Should().Be(nameof(DayOfWeek.Monday));
    }

    [Fact]
    public void NonStringToString_ReturnsTheInput()
    {
        var input = new TestClass();

        var actual = input.ChangeType<string>();

        var expected = input.ToString();
        actual.Should().Be(expected);
    }
}

public class StringInScientificNotationToFloatingPointHandlerTests
{
    [Fact]
    public void EnumToString_ReturnsEnumString()
    {
        var input = DayOfWeek.Monday;

        var actual = input.ChangeType<string>();

        actual.Should().Be(nameof(DayOfWeek.Monday));
    }
}