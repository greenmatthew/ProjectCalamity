using UnityEngine;
using UnityEngine.UI;

namespace PC.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class Item : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields

        [HideInInspector] public bool isRotated = false;
        public uint cellWidth => _itemSO.cellWidth;
        public uint cellHeight => _itemSO.cellHeight;

        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private ItemSO _itemSO = null;
        private Container _currentContainer = null;
        private RectTransform _rectTransform = null;
        [SerializeField] private Image _backgoundImage;
        [SerializeField] private Image _contentImage;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods

        public Item Init(ItemSO itemSO)
        {
            _itemSO = itemSO;
            SetSize();
            SetImages();
            return this;
        }

        public void SetContainer(Container container)
        {
            _currentContainer = container;
        }

        /// <summary>
        /// Rotates the item or rather toggles the item's rotation between 0 and 90 degrees
        /// Useful for packing items together more efficiently
        /// </summary>
        public void Rotate()
        {
            // If the item is square no need to rotate, bc it provides no packing benefit
            if (_itemSO.cellWidth == _itemSO.cellHeight) return;

            // If the item is not within a container, always allow rotation, bc you can't possibly have items' cell overlaps or out of bounds
            if (_currentContainer == null)
            {
                var temp = _itemSO.cellWidth;
                _itemSO.cellWidth = _itemSO.cellHeight;
                _itemSO.cellHeight = temp;
                isRotated = !isRotated;
                SetSize();
            }
            // If the item is withing a container, allow rotation if there are no items' cell overlaps or out of bounds would occur
            else
            {
                // ADD CHECK HERE
                // if (_currentContainer.CanRotate(this))
                // {
                    var temp = _itemSO.cellWidth;
                    _itemSO.cellWidth = _itemSO.cellHeight;
                    _itemSO.cellHeight = temp;
                    isRotated = !isRotated;
                    SetSize();
                // }
                Debug.LogWarning("Need to handle whether the item can be rotated or not. Currently rotates regardless.");
            }
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// Sets the RectTransform size of the item based on the cellWidth and cellHeight
        /// </summary>
        private void SetSize()
        {
            _rectTransform.sizeDelta = new Vector2
            (
                _itemSO.cellWidth * Container.CellSideLength - _itemSO.cellWidth + 1,
                _itemSO.cellHeight * Container.CellSideLength - _itemSO.cellHeight + 1
            );
        }

        private void SetImages()
        {
            _backgoundImage.color = _itemSO.backgroundColor;
            if (_itemSO.itemIcon != null)
            {
                _contentImage.sprite = _itemSO.itemIcon;
            }
            else
            {
                _contentImage.color = new Color(1, 1, 1, 0);
            }
                
        }

        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}