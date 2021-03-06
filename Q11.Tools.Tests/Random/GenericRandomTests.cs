using System;
using System.Collections.Generic;
using FluentAssertions;
using Q11.Tools.Random;
using Xunit;

namespace Q11.Tools.Tests.Random;

public class GenericRandomTests
{
    [Fact]
    public void ClassTest_ForString_ShouldHaveSemiRandomResults()
    {
        var target = new GenericRandom(0);

        var actual1 = target.Next<string>();
        actual1.Should().Be("txvhMh4a");

        var actual2 = target.Next<string>();
        actual2.Should().Be("8RScmc9B");
    }

    [Fact]
    public void ClassTest_ForPerson_ShouldHaveValuesAtHighestLevel()
    {
        var target = new GenericRandom(0);

        var actual = target.Next<Person>();

        actual.Should().NotBeNull();
        actual.Name.Should().Be("txvhMh4a");
        actual.SingleNumber.Should().Be(97979476);
        actual.SomeDate.Should().Be(new DateTime(2000, 4, 22, 16, 7, 16));
        actual.MultipleNumbers.Should().Equal(new List<long> { 81528902, 40344430, 51148790, 23214253 });
    }

    [Fact]
    public void ClassTest_ForPerson_ShouldHave3Levels()
    {
        var target = new GenericRandom(0);

        var actual = target.Next<Person>();

        actual.Should().NotBeNull();
        actual.Name.Should().Be("txvhMh4a");

        actual.Child.Should().NotBeNull();
        actual.Child.Name.Should().Be("RScmc9B2");

        actual.Child.Child.Should().NotBeNull();
        actual.Child.Child.Name.Should().Be("pTxz9Brf");

        // Maximum 3 levels deep to prevent a stack overflow
        actual.Child.Child.Child.Should().BeNull();
    }
}