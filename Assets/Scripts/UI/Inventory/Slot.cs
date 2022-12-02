using System.Collections.Generic;
using UnityEngine;

namespace PC.UI
{
    public class Slot : ContainerBase
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        [SerializeField] private ItemType.Value i_allowedType = ItemType.Value.NONE;
        private ItemType _allowedType;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods

        /// <summary>
        /// Gets the cell index relative to the container using the given mouse position.
        /// </summary>
        /// <param name="mousePos">Position of the mouse.</param>
        /// <returns>Cell index relative to the container.</returns>
        public override Vector2Int GetCellIndex(Vector2 mousePos)
        {
            _mousePos = mousePos;
            _currentPosition = new Vector2(mousePos.x - _anchor.position.x, _anchor.position.y - mousePos.y);
            _currentCellIndex = new Vector2Int((int)(_currentPosition.x / _rectTransform.sizeDelta.x), (int)(_currentPosition.y / _rectTransform.sizeDelta.y));

            return _currentCellIndex;
        }

        /// <summary>
        /// Removes the item at a given cellIndex from the container.
        /// </summary>
        /// <param name="cellIndex">The cell index of the item you want to remove.</param>
        /// <returns>Returns true if the operation was successful, otherwise false.</returns>
        public override bool RemoveItemAt(Vector2Int cellIndex)
        {
            Item item = GetItemAt(cellIndex);
            cellIndex = item.OriginCellIndex;


            if (IsCellOutOfRange(cellIndex))
            {
                Debug.LogError($"Removing item @ {cellIndex} w/ size ({item.cellWidth}, {item.cellHeight}) would result in part or all of the item's removal to be out of range of the container's internal contents array. Out of range cell index relative to container origin @ {cellIndex}.");
                return false;
            }

            if (IsCellOccupiedNotBySelfOrCopy(item, cellIndex))
            {
                Debug.LogError($"Removing item @ {cellIndex} w/ size ({item.cellWidth}, {item.cellHeight}) where the item never existed at. Null cell index relative to container origin @ {cellIndex}.");
                return false;
            }

            EmptyCell(cellIndex);
            // Sets the item's container to null to indicate that it is no longer in a container.
            item.RemoveContainer();
            // Successfully removed the item.
            return true;
        }

        public override bool PlaceItemAt(Item item, Vector2Int cellIndex)
        {
            if (item == null)
            {
                Debug.LogError("Item is null.");
                return false;
            }

            if (!_allowedType.belongsTo(item.type))
            {
                Debug.LogError($"Item type {item.type} does not match slot type {_allowedType}.");
                return false;
            }

            if (IsCellOutOfRange(cellIndex))
            {
                Debug.LogError($"Placing item @ {cellIndex} w/ size ({item.cellWidth}, {item.cellHeight}) would result in part or all of the item to be out of range of the container's internal contents array. Out of range cell index relative to container origin @ {cellIndex}.");
                return false;
            }

            if (IsCellOccupiedExcludingSelfOrCopy(item, cellIndex))
            {
                Debug.LogError($"Placing item @ {cellIndex} w/ size ({item.cellWidth}, {item.cellHeight}) would intersect with another item's occupied cells in the internal contents array. Intersection cell index relative to container origin @ {cellIndex}.");
                return false;
            }

            SetCell(cellIndex, item);
            // Update the container of the item.
            item.SetContainer(this, cellIndex);
            if (item.isRotated)
            {
                item.Rotate();
            }
            return true;
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        protected override void Start()
        {
            cellWidth = 1;
            cellHeight = 1;

            InitContents();
            _contentsParent.sizeDelta = _rectTransform.sizeDelta - new Vector2(2, 2);
            _allowedType = ItemType.Lookup(i_allowedType);
        }

        private void Update()
        {
        
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}