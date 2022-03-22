using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;

namespace Systemagedon.App.Extensions
{

    public static class IEnumerableExtensions
    {
        public static T SelectRandom<T>(this IEnumerable<T> source)
        {
            if (source.Count() == 0)
            {
                throw new InvalidOperationException("Source is empty");
            }
            int random = Random.Range(0, source.Count());
            return source.ElementAt(random);
        }
    }

}
