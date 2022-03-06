using System;
using FluentAssertions;
using Q11.Tools.Conversion;
using Q11.Tools.Tests.Support;
using Xunit;

namespace Q11.Tools.Tests.Conversion.Handlers;

public class SameTypeHandlerTests
{
    [Fact]
    public void SameType_ReturnsEnumString()
    {
        var input = DayOfWeek.Monday;

        var actual = input.ChangeType<DayOfWeek>();

        actual.Should().Be(DayOfWeek.Monday);
    }
}