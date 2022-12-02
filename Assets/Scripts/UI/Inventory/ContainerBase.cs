using UnityEngine;
using UnityEngine.EventSystems;

using static PC.UI.Constants;

namespace PC.UI
{
    public abstract class ContainerBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields

        // Contents of the container
        [SerializeField] protected RectTransform _contentsParent = null;
        public RectTransform ContentsParent => _contentsParent;

        #endregion Public Fields

        #region Protected Fields

        protected int cellWidth = 10;
        protected int cellHeight = 10;

        protected RectTransform _rectTransform = null;

        // Background of the container
        [SerializeField] protected RectTransform _anchor = null;
        [SerializeField] protected RectTransform _cellBackgroundPrefab = null;
        [SerializeField] protected RectTransform _cellBackgroundParent = null;

        // Position/Index within the container
        protected Vector2 _mousePos = Vector2.zero;
        protected Vector2 _currentPosition = Vector2.zero;
        protected Vector2Int _currentCellIndex = Vector2Int.zero;

        

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            InventoryMenu.CurrentContainer = this;
            Debug.Log($"Entered container {gameObject.name}");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InventoryMenu.CurrentContainer = null;
            Debug.Log($"Exit container {gameObject.name}");
        }
        
        /// <summary>
        /// Sets the parent of an item to the content parent object of this container.
        /// </summary>
        /// <param name="itemRectTransform">The given item you want to set the parent of.</param>
        public void SetItemParent(RectTransform itemRectTransform)
        {
            itemRectTransform.SetParent(_contentsParent);
            itemRectTransform.SetAsLastSibling();
        }

        /// <summary>
        /// Gets the cell index relative to the container using the given mouse position.
        /// </summary>
        /// <param name="mousePos">Position of the mouse.</param>
        /// <returns>Cell index relative to the container.</returns>
        public virtual Vector2Int GetCellIndex(Vector2 mousePos)
        {
            _mousePos = mousePos;
            _currentPosition = new Vector2(mousePos.x - _anchor.position.x, _anchor.position.y - mousePos.y);
            _currentCellIndex = new Vector2Int((int)(_currentPosition.x / CellSideLength), (int)(_currentPosition.y / CellSideLength));

            return _currentCellIndex;
        }

        /// <summary>
        /// Gets the item at the given cell index.
        /// </summary>
        /// <param name="cellIndex">The cell index you want to check.</param>
        /// <returns>The item at the given cell index if an item was present or null if there was no item.</returns>
        public Item GetItemAt(Vector2Int cellIndex)
        {
            if (IsCellOutOfRange(cellIndex))
            {
                Debug.LogError("Cell index is out of range.");
                return null;
            }

            return GetCell(cellIndex);
        }

        /// <summary>
        /// Removes the item at a given cellIndex from the container.
        /// </summary>
        /// <param name="cellIndex">The cell index of the item you want to remove.</param>
        /// <returns>Returns true if the operation was successful, otherwise false.</returns>
        public virtual bool RemoveItemAt(Vector2Int cellIndex)
        {
            Item item = GetItemAt(cellIndex);
            cellIndex = item.OriginCellIndex;

            // Checks if the different cells that makes up the item are within the range of the container's area and if they are occupied by another item instead.
            for (int r = 0; r < item.cellHeight; ++r)
            {
                for (int c = 0; c < item.cellWidth; ++c)
                {
                    Vector2Int offset = new Vector2Int(c, r);
                    Vector2Int newCellIndex = cellIndex + offset;

                    if (IsCellOutOfRange(newCellIndex))
                    {
                        Debug.LogError($"Removing item @ {cellIndex} w/ size ({item.cellWidth}, {item.cellHeight}) would result in part or all of the item's removal to be out of range of the container's internal contents array. Out of range cell index relative to container origin @ {newCellIndex} (and relative to item origin {offset}). THIS SHOULD NEVER HAPPEN AND MEANS A BUG IS PRESENT.");
                        return false;
                    }

                    if (IsCellOccupiedNotBySelfOrCopy(item, newCellIndex))
                    {
                        Debug.LogError($"Removing item @ {cellIndex} w/ size ({item.cellWidth}, {item.cellHeight}) where the item never existed at. Null cell index relative to container origin @ {newCellIndex} (and relative to item origin {offset}). THIS SHOULD NEVER HAPPEN AND MEANS A BUG IS PRESENT.");
                        return false;
                    }
                }
            }

            // Sets each cell the item takes up to null
            for (int r = 0; r < item.cellHeight; ++r)
            {
                for (int c = 0; c < item.cellWidth; ++c)
                {
                    Vector2Int offset = new Vector2Int(c, r);
                    Vector2Int newCellIndex = cellIndex + offset;

                    EmptyCell(newCellIndex);
                }
            }
            // Sets the item's container to null to indicate that it is no longer in a container.
            item.RemoveContainer();
            // Successfully removed the item.
            return true;
        }

        public virtual bool PlaceItemAt(Item item, Vector2Int cellIndex)
        {
            if (item == null)
            {
                Debug.LogError("Item is null.");
                return false;
            }

            // Checks if the different cells that makes up the item are within the range of the container's area and if they are occupied by another item instead.
            for (int r = 0; r < item.cellHeight; ++r)
            {
                for (int c = 0; c < item.cellWidth; ++c)
                {
                    Vector2Int offset = new Vector2Int(c, r);
                    Vector2Int newCellIndex = cellIndex + offset;

                    if (IsCellOutOfRange(newCellIndex))
                    {
                        Debug.LogError($"Placing item @ {cellIndex} w/ size ({item.cellWidth}, {item.cellHeight}) would result in part or all of the item to be out of range of the container's internal contents array. Out of range cell index relative to container origin @ {newCellIndex} (and relative to item origin {offset}).");
                        return false;
                    }

                    if (IsCellOccupiedExcludingSelfOrCopy(item, newCellIndex))
                    {
                        Debug.LogError($"Placing item @ {cellIndex} w/ size ({item.cellWidth}, {item.cellHeight}) would intersect with another item's occupied cells in the internal contents array. Intersection cell index relative to container origin @ {newCellIndex} (and relative to item origin {offset}).");
                        return false;
                    }
                }
            }

            // Sets each cell the item takes up inside this container to the item.
            for (int r = 0; r < item.cellHeight; ++r)
            {
                for (int c = 0; c < item.cellWidth; ++c)
                {
                    Vector2Int offset = new Vector2Int(c, r);
                    Vector2Int newCellIndex = cellIndex + offset;

                    SetCell(newCellIndex, item);
                }
            }
            // Update the container of the item.
            item.SetContainer(this, cellIndex);
            return true;
        }

        /// <summary>
        /// Takes an item from a container
        /// </summary>
        /// <param name="cellIndex">The cell index of the item you want to take from the container.</param>
        /// <returns>The item taken from the container</returns>
        public virtual Item TakeItemAt(Vector2Int cellIndex)
        {
            var item = GetItemAt(cellIndex);
            if (item != null)
                RemoveItemAt(cellIndex);
            return item;
        }

        public void TransferItem(Vector2Int sourceCellIndex, Container targetContainer, Vector2Int targetCellIndex)
        {
            if (targetContainer == null)
            {
                Debug.LogError("Target container is null.");
                return;
            }

            Item item = TakeItemAt(sourceCellIndex);
            if (item != null)
                targetContainer.PlaceItemAt(item, targetCellIndex);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        protected virtual void Start()
        {
            
        }

        /// <summary>
        /// Initializes the contents of the container.
        /// </summary>
        protected void InitContents()
        {
            _contents = new Item[cellWidth, cellHeight];
        }

        /// <summary>
        /// Initializes the contents of the container.
        /// </summary>
        /// <param name="size">The area of the contents.</param>
        protected void InitContents(Vector2 size)
        {
            _contents = new Item[cellWidth, cellHeight];

            _rectTransform.sizeDelta = size;
            _contentsParent.sizeDelta = size;
        }

        /// <summary>
        /// Initializes the background of the container.
        /// </summary>
        /// <param name="size">The area of the background.</param>
        protected void InitBackground(Vector2 size)
        {
            _rectTransform.sizeDelta = size;
            _cellBackgroundParent.sizeDelta = size;
            for (int r = 0; r < cellWidth; ++r)
            {
                for (int c = 0; c < cellHeight; ++c)
                {
                    RectTransform slotBackground = Instantiate(_cellBackgroundPrefab, _cellBackgroundParent);
                }
            }
        }

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

        protected bool IsCellOutOfRange(Vector2Int cellIndex) => cellIndex.x < 0 || cellIndex.x >= cellWidth || cellIndex.y < 0 || cellIndex.y >= cellHeight;
        protected bool IsCellEmpty(Vector2Int cellIndex) => GetCell(cellIndex) == null;
        protected bool IsCellEmptyExcludingSelfOrCopy(Item item, Vector2Int cellIndex) => IsCellEmpty(cellIndex) || GetCell(cellIndex) == item || GetCell(cellIndex) == item.Source;
        protected bool IsCellOccupied(Vector2Int cellIndex) => !IsCellEmpty(cellIndex);
        protected bool IsCellOccupiedExcludingSelfOrCopy(Item item, Vector2Int cellIndex) => !IsCellEmptyExcludingSelfOrCopy(item, cellIndex);
        protected bool IsCellOccupiedBySelfOrCopy(Item item, Vector2Int cellIndex) => GetCell(cellIndex) == item || GetCell(cellIndex) == item.Source;
        protected bool IsCellOccupiedNotBySelfOrCopy(Item item, Vector2Int cellIndex) => GetCell(cellIndex) != item && GetCell(cellIndex) == item.Source;

        #endregion Protected Methods

        #region Private Methods

        
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}