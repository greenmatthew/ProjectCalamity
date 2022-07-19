using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Obtain a random item from a list.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="list">The list to get a random item from.</param>
        /// <returns>The random item from the list.</returns>
        public static T GetRandom<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default(T);
            }
            else
            {
                return list[UnityEngine.Random.Range(0, list.Count)];
            }
        }
    }
}
