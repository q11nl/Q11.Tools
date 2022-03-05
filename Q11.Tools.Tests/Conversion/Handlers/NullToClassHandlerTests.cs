using System;
using System.Collections.Generic;
using FluentAssertions;
using Q11.Tools.Conversion;
using Xunit;

namespace Q11.Tools.Tests.Conversion.Handlers;

public class NullToClassHandlerTests
{
    [Fact]
    public void NullToClass_ReturnsNull()
    {
        var input = default(List<int>);

        var actual = input.ChangeType<List<string>>();

        actual.Should().BeNull();
    }
}

public class StringToGuidHandlerTests
{
    [Fact]
    public void StringToGuidHandler_StandardFormat_ReturnsGuid()
    {
        var input = "70059267-32f0-4111-a90b-f760d35766a4";

        var actual = input.ChangeType<Guid>();

        var expected = new Guid("70059267-32f0-4111-a90b-f760d35766a4");
        actual.Should().Be(expected);
    }

    [Fact]
    public void StringToNullableGuidHandler_StandardFormat_ReturnsGuid()
    {
        var input = "70059267-32f0-4111-a90b-f760d35766a4";

        var actual = input.ChangeType<Guid?>();

        Guid? expected = new Guid("70059267-32f0-4111-a90b-f760d35766a4");
        actual.Should().Be(expected);
    }
}