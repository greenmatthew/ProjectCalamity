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

        public static void Enqueue<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            foreach (T item in items)
                queue.Enqueue(item);
        }

        #endregion

        #region UnityEngine.Transform Extensions

        /// <summary>
        /// Get hierarchy path.
        /// </summary>
        /// <param name="transform">The transform to get the path of</param>
        /// <param name="includeSceneName">Include the scene name as the root of the path</param>
        /// <returns>The hierarchy path of the transform</returns>
        public static string HierarchyPath(this Transform transform, bool includeSceneName = false)
        {
            var path = new System.Text.StringBuilder();
            var current = transform;
            while (current != null)
            {
                path.Insert(0, current.name);
                path.Insert(0, "/");
                current = current.parent;
            }

            if (includeSceneName)
                path.Insert(0, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

            return path.ToString();
        }

        #endregion UnityEngine.Transform Extensions

        #region UnityEngine.GameObject Extensions

        /// <summary>
        /// Get hierarchy path.
        /// </summary>
        /// <param name="gameObject">The GameObject to get the path of</param>
        /// <param name="includeSceneName">Include the scene name as the root of the path</param>
        /// <returns>The hierarchy path of the transform</returns>
        public static string HierarchyPath(this GameObject gameObject, bool includeSceneName = false)
        {
            return gameObject.transform.HierarchyPath(includeSceneName);
        }

        #endregion
    }
}
