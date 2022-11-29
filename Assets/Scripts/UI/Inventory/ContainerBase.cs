using UnityEngine;
using UnityEngine.EventSystems;

namespace PC.UI
{
    public abstract class ContainerBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Fields

        #region Consts Fields

        protected const int cellWidth = 10;
        protected const int cellHeight = 10;

        #endregion Consts Fields

        #region Public Fields

        public abstract void OnPointerEnter(PointerEventData eventData);
        public abstract void OnPointerExit(PointerEventData eventData);

        #endregion Public Fields

        #region Protected Fields

        /// <summary>
        /// Check the contents of the container.
        /// </summary>
        /// <param name="cellIndex">The cell index you want to check.</param>
        /// <returns>The item at the given cell index if an item was present or null if there was no item.</returns>
        protected Item GetCell(Vector2Int cellIndex)
        {
            return _contents[cellIndex.y, cellIndex.x];
        }

        /// <summary>
        /// Sets the contents of the container.
        /// </summary>
        /// <param name="cellIndex">The cell index you want to store an item at.</param>
        /// <param name="item">The item you want to store.</param>
        protected void SetCell(Vector2Int cellIndex, Item item)
        {
            _contents[cellIndex.y, cellIndex.x] = item;
        }

        /// <summary>
        /// Sets the cell at the given index to null.
        /// </summary>
        /// <param name="cellIndex">The cell index you want to set to null.</param>
        protected void EmptyCell(Vector2Int cellIndex)
        {
            SetCell(cellIndex, null);
        }

        #endregion Protected Fields

        #region Private Fields

        private Item[,] h_contents = null;
        private Item[,] _contents
        {
            get
            {
                return h_contents;
            }
            set
            {
                if (h_contents != null)
                {
                    Debug.LogError("Container contents can only be set once.");
                    return;
                }
                h_contents = value;
            }
        }

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        protected virtual void InitContents(Vector2 size)
        {
            _contents = new Item[cellWidth, cellHeight];
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}