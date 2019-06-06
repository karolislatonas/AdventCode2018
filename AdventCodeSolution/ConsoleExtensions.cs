using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCodeSolution
{
    public static class ConsoleExtensions
    {
        public static void WriteLine<T>(this T obj) => Console.WriteLine(obj);

        public static void WriteLine<T>(this T obj, string prefix) => Console.WriteLine($"{prefix}{obj}");
    }
}
