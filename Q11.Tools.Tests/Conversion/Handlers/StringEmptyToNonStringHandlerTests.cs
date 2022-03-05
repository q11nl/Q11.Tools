using System;
using FluentAssertions;
using Q11.Tools.Conversion;
using Q11.Tools.Tests.Support;
using Xunit;

namespace Q11.Tools.Tests.Conversion.Handlers;

public class StringEmptyToNonStringHandlerTests
{
    [Fact]
    public void StringEmptyToNullableStruct_ReturnsNull()
    {
        var input = "";

        var actual = input.ChangeType<int?>();

        actual.Should().BeNull();
    }

    [Fact]
    public void StringEmptyToClass_ReturnsNull()
    {
        var input = "";

        var actual = input.ChangeType<TestClass>();

        actual.Should().BeNull();
    }


    [Fact]
    public void StringEmptyToStruct_ReturnDefaultValueWhenPossible_ReturnsNull()
    {
        var input = "";

        var actual = input.ChangeType<int?>();

        actual.Should().BeNull();
    }

    [Fact]
    public void StringEmptyToStruct_Not_ReturnDefaultValueWhenPossible_ThrowsException()
    {
        "".Invoking(y => y.ChangeType<int?>(false))
            .Should().Throw<FormatException>();
    }
}