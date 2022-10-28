using UnityEngine;

namespace PC.UI
{
    public class ItemContainerInfo
    {
        // The item found inside the source container at the source index
        public Item item { get; private set; } = null;
        // The source index of the item
        public Vector2Int sourceIndex { get; private set; } = Vector2Int.zero;
        // The source container of the item
        public Container sourceContainer { get; private set; } = null;
        // The RectTransform of the item
        public RectTransform rectTransform { get; private set; } = null;
        // Flag to indicate if the item was rotated upon the creation of this object
        public bool wasRotated { get; private set; } = false;

        /// <summary>
        /// Constructor for the ItemContainerInfo class
        /// </summary>
        /// <param name="container">The source container the item is currently in</param>
        /// <param name="index">The source index of the item in the source container</param>
        /// <returns>An ItemContainerInfo object containing info about the item's state in its source container</returns>
        private ItemContainerInfo(Container currentContainer, Vector2Int currentIndex)
        {
            sourceContainer = currentContainer;
            sourceIndex = currentIndex;
            item = sourceContainer.TakeItemAt(sourceIndex);
            rectTransform = item.GetComponent<RectTransform>();
            wasRotated = item.isRotated;
        }

        /// <summary>
        /// Creates an ItemContainerInfo object containing info about a given item's state in its source container
        /// </summary>
        /// <param name="container">The source container the item is currently in</param>
        /// <param name="index">The source index of the item in the source container</param>
        /// <returns>An ItemContainerInfo object containing info about the item's state in its source container</returns>
        public static ItemContainerInfo Create(Container container, Vector2Int index)
        {
            // If there was no container given, return null
            if (container == null) return null;
            // If there was no item at the given index, return null
            if (container.GetItemAt(index) == null) return null;
            // Return a object containing info about the item's state in the container
            return new ItemContainerInfo(container, index);
        }
    }
}