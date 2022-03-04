using System;
using System.Collections.Generic;

namespace Q11.Tools.Tests.Random;

public class Person
{
    public string Name { get; set; }
    public int SingleNumber { get; set; }
    public Person Child { get; set; }
    public DateTime SomeDate { get; set; }
    public List<long> MultipleNumbers { get; set; }
}