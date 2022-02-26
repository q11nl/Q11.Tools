using System;
using System.Collections.Generic;
using Q11.Tools.Random;
using Xunit;

namespace Q11.Tools.Tests.Random;

public class GenericRandomTests
{
    [Fact]
    public void Test1()
    {
        var r0 = new GenericRandom(0);

        var result = r0.Next<Person>();
        var age = result.Age;
        var name = result.Name;
        var numbers = result.Numbers;
        var someDate = result.SomeDate;
    }
}

public class Person
{

    public Person(int value)
    {
        Age = 1;
    }

    public List<long> Numbers { get; set; }
    public string Name { get; set; }
    public int Age { get; }
    public DateTime SomeDate { get; set; }
    public Person Child { get; set; }
}
