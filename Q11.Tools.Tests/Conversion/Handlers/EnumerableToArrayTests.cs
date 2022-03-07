using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using FluentAssertions;
using Q11.Tools.Conversion;
using Q11.Tools.Tests.Support;
using Xunit;

namespace Q11.Tools.Tests.Conversion.Handlers;

public class EnumerableToArrayTests
{
    [Fact]
    public void EnumerableToArray_ReturnsGuids()
    {
        var actual = TestValues.GuidStrings.ChangeType<List<Guid>>();

        actual.Should().BeEquivalentTo(TestValues.Guids);
    }
}