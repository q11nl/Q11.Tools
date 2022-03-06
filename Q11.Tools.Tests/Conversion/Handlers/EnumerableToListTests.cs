using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Q11.Tools.Conversion;
using Xunit;

namespace Q11.Tools.Tests.Conversion.Handlers;

public class EnumerableToListTests
{
    private readonly IEnumerable<string> _input = new List<string>()
    {
        "f57bff44-a62c-4322-ac4c-d1e2764dfe1c",
        "a7d8202b-2bb8-425a-a68e-cc3d049741b5",
        "3089ad99-b9ab-4be2-8865-d8dad41cb0e6"
    }.Where(_ => true);

    private readonly List<Guid> _expected = new()
    {
        new Guid("f57bff44-a62c-4322-ac4c-d1e2764dfe1c"),
        new Guid("a7d8202b-2bb8-425a-a68e-cc3d049741b5"),
        new Guid("3089ad99-b9ab-4be2-8865-d8dad41cb0e6")
    };

    [Fact]
    public void EnumerableToList_ReturnsGuids()
    {
        var actual = _input.ChangeType<List<Guid>>();

        actual.Should().BeEquivalentTo(_expected);
    }

    [Fact]
    public void EnumerableToIList_ReturnsGuids()
    {
        var actual = _input.ChangeType<IList<Guid>>();

        actual.Should().BeEquivalentTo(_expected);
    }

    [Fact]
    public void EnumerableToIReadonlyList_ReturnsGuids()
    {
        var actual = _input.ChangeType<IReadOnlyList<Guid>>();

        actual.Should().BeEquivalentTo(_expected);
    }


    [Fact]
    public void EnumerableToIEnumerableReturnsGuids()
    {
        var actual = _input.ChangeType<IEnumerable<Guid>>();

        actual.Should().BeEquivalentTo(_expected);
    }

    [Fact]
    public void IReadonlyListToIList_ReturnsGuids()
    {
        var input = ((IReadOnlyList<string>)_input.ToList());
        var actual = input.ChangeType<IList<Guid>>();

        actual.Should().BeEquivalentTo(_expected);
    }

    [Fact]
    public void ArrayToIReadonlyList_ReturnsGuids()
    {
        var input = _input.ToArray();
        var actual = input.ChangeType<List<Guid>>();

        actual.Should().BeEquivalentTo(_expected);
    }
}