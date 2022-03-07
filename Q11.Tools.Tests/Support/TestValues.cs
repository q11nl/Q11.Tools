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

    public static IEnumerable<string> GuidStrings = new List<string>()
    {
        "f57bff44-a62c-4322-ac4c-d1e2764dfe1c",
        "a7d8202b-2bb8-425a-a68e-cc3d049741b5",
        "3089ad99-b9ab-4be2-8865-d8dad41cb0e6"
    }.Where(_ => true);

    public static List<Guid> Guids = new()
    {
        new Guid("f57bff44-a62c-4322-ac4c-d1e2764dfe1c"),
        new Guid("a7d8202b-2bb8-425a-a68e-cc3d049741b5"),
        new Guid("3089ad99-b9ab-4be2-8865-d8dad41cb0e6")
    };

    private static class ValueFactory
    {
        private static readonly GenericRandom GenericRandom = new();

        public static T Get<T>() => GenericRandom.Next<T>();
    }
}
