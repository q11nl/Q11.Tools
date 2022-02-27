using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q11.Tools.Extensions
{
    public static class LinqExtensions
    {
        public static bool IsIn<T>([DisallowNull] this T source, params T[] values)
        {
            if (source == null || values == null)
            {
                return false;
            }
            return values.Any(v => source.Equals(v));
        }
    }
}
