using System;
using System.Collections.Generic;

namespace WordUp.Shared.StaticShared
{
    static class RandomExtensions
    {
        public static void Shuffle<T> (this Random rng, IList<T> list)
        {
            int n = list.Count;
            while (n > 1) 
            {
                int k = rng.Next(n--);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}