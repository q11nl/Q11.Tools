using System;
using FluentAssertions;
using Q11.Tools.Conversion;
using Xunit;

namespace Q11.Tools.Tests.Conversion.Handlers;

public class StringToEnumHandlerTests
{
    [Fact]
    public void StringToEnum_ReturnsWeekday()
    {
        var input = "Monday";

        var actual = input.ChangeType<DayOfWeek>();

        actual.Should().Be(DayOfWeek.Monday);
    }
}