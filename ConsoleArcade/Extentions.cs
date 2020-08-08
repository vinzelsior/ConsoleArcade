using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleArcade
{
    static class Extentions
    {
        static Random random;

        static T PickRandom<T>(this List<T> list)
        {
            return list[random.Next(0, list.Count - 1)];
        }
    }
}
