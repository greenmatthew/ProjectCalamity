using System.Collections.Generic;
using UnityEngine;

namespace PC.Extensions
{
    public static class Extensions
    {
        #region System.Collections.Generic.List<T> Extensions

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

        #endregion

        #region UnityEngine.Transform Extensions

        /// <summary>
        /// Get hierarchy path.
        /// </summary>
        /// <param name="transform">The transform to get the path of</param>
        /// <returns>The hierarchy path of the transform</returns>
        public static string HierarchyPath(this Transform transform)
        {
            var path = new System.Text.StringBuilder();
            var current = transform;
            while (current != null)
            {
                path.Insert(0, current.name);
                path.Insert(0, "/");
                current = current.parent;
            }
            return path.ToString();
        }

        #endregion

        #region UnityEngine.GameObject Extensions

        /// <summary>
        /// Get hierarchy path.
        /// </summary>
        /// <param name="gameObject">The GameObject to get the path of</param>
        /// <returns>The hierarchy path of the transform</returns>
        public static string HierarchyPath(this GameObject gameObject)
        {
            return gameObject.transform.HierarchyPath();
        }

        #endregion
    }
}
