using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q11.Tools.Random;

namespace Q11.Tools.Tests.Support;

internal static class TestValues
{
    public static string StringNonNullable = ValueFactory.Get<string>();
    public static string? StringNullable = ValueFactory.Get<string>();

    private static class ValueFactory
    {
        private static readonly GenericRandom GenericRandom = new();

        public static T Get<T>() => GenericRandom.Next<T>();
    }
}
