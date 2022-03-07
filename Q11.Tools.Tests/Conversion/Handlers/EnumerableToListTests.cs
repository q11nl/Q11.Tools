using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Q11.Tools.Conversion;
using Q11.Tools.Tests.Support;
using Xunit;

namespace Q11.Tools.Tests.Conversion.Handlers;

public class EnumerableToListTests
{

    [Fact]
    public void EnumerableToList_ReturnsGuids()
    {
        var actual = TestValues.GuidStrings.ChangeType<List<Guid>>();

        actual.Should().BeEquivalentTo(TestValues.Guids);
    }

    [Fact]
    public void EnumerableToIList_ReturnsGuids()
    {
        var actual = TestValues.GuidStrings.ChangeType<IList<Guid>>();

        actual.Should().BeEquivalentTo(TestValues.Guids);
    }

    [Fact]
    public void EnumerableToIReadonlyList_ReturnsGuids()
    {
        var actual = TestValues.GuidStrings.ChangeType<IReadOnlyList<Guid>>();

        actual.Should().BeEquivalentTo(TestValues.Guids);
    }


    [Fact]
    public void EnumerableToIEnumerableReturnsGuids()
    {
        var actual = TestValues.GuidStrings.ChangeType<IEnumerable<Guid>>();

        actual.Should().BeEquivalentTo(TestValues.Guids);
    }

    [Fact]
    public void IReadonlyListToIList_ReturnsGuids()
    {
        var input = ((IReadOnlyList<string>)TestValues.GuidStrings.ToList());
        var actual = input.ChangeType<IList<Guid>>();

        actual.Should().BeEquivalentTo(TestValues.Guids);
    }

    [Fact]
    public void ArrayToIReadonlyList_ReturnsGuids()
    {
        var input = TestValues.GuidStrings.ToArray();
        var actual = input.ChangeType<List<Guid>>();

        actual.Should().BeEquivalentTo(TestValues.Guids);
    }
}