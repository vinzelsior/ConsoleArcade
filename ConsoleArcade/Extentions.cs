using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleArcade
{
    public static class Extentions
    {
        public static Random random = new Random();

        public static T PickRandom<T>(this List<T> list)
        {
            return list[random.Next(0, list.Count)];
        }
    }
}
