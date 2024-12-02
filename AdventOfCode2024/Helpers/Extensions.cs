using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, int index, T value) => enumerable.Select((x, i) => index == i ? value : x);
    }
}
