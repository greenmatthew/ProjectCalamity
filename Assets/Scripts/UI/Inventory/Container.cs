using UnityEngine;
using UnityEngine.EventSystems;

using static PC.UI.Constants;

namespace PC.UI
{
    public class Container : ContainerBase
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        
        // Init items
        [SerializeField] private Item _itemPrefab = null;
        [SerializeField] private ItemSO[] _items = null;
        [SerializeField] private Vector2Int[] _itemPositions = null;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        protected override void Start()
        {
            cellWidth = 10;
            cellHeight = 10;

            var size = new Vector2(cellWidth * CellSideLength, cellHeight * CellSideLength);

            InitBackground(size);
            InitContents(size);

            for (int i = 0; i < _items.Length; ++i)
                PlaceItemAt(Instantiate(_itemPrefab, _contentsParent).Init(_items[i]), _itemPositions[i]);
        }
        
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}