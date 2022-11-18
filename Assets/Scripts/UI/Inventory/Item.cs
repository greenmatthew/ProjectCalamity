using UnityEngine;
using UnityEngine.UI;

using static PC.UI.Constants;

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
        public uint cellWidth;
        public uint cellHeight;

        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private ItemSO _itemSO = null;
        private Container _currentContainer = null;
        private RectTransform _rectTransform = null;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _contentImage;
        private RectTransform _contentRectTransform = null;
        private Vector2Int _originCellIndex = Vector2Int.zero;
        public Vector2Int OriginCellIndex => _originCellIndex;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods

        public Item Init(ItemSO itemSO)
        {
            _itemSO = itemSO;
            cellWidth = _itemSO.cellWidth;
            cellHeight = _itemSO.cellHeight;
            SetSize();
            SetImages();
            return this;
        }

        /// <summary>
        /// Caches the container the item is currently in.
        /// Sets the item's parent to the given container's content GameObject.
        /// Sets the item's position to the given cell index relative to the container's content GameObject.
        /// </summary>
        public void SetContainer(Container container, Vector2Int originCellIndex)
        {
            if (container == null) return;
            _currentContainer = container;
            _originCellIndex = originCellIndex;
            _rectTransform.SetParent(container.ContentsParent);
            Vector2 position = new Vector2(originCellIndex.x * (CellSideLength - 1), -1 * originCellIndex.y * (CellSideLength - 1));
            _rectTransform.localPosition = position;
        }

        public void RemoveContainer()
        {
            _currentContainer = null;
            _rectTransform.SetParent(null);
        }

        public Vector2Int GetOriginCellIndex()
        {
            return _originCellIndex;
        }

        /// <summary>
        /// Rotates the item or rather toggles the item's rotation between 0 and 90 degrees
        /// Useful for packing items together more efficiently
        /// </summary>
        public void Rotate()
        {
            // If the item is square no need to rotate, bc it provides no packing benefit
            if (cellWidth == cellHeight) return;

            var temp = cellWidth;
            cellWidth = cellHeight;
            cellHeight = temp;
            isRotated = !isRotated;
            // var rect = GetComponent<RectTransform>();
            // if (isRotated)
            // {
            //     // Rotate _contentImage.sprite by 90 degrees
            //     _contentImage.sprite
            // }
            // else
            // {
            //     _contentImage.sprite.rect.Set(_contentImage.sprite.rect.x, _contentImage.sprite.rect.y, _contentImage.sprite.rect.width, _contentImage.sprite.rect.height);
            // }
            
            SetSize();
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _contentRectTransform = _contentImage.GetComponent<RectTransform>();
        }

        /// <summary>
        /// Sets the RectTransform size of the item based on the cellWidth and cellHeight
        /// </summary>
        private void SetSize()
        {
            if (!isRotated)
            {
                _rectTransform.sizeDelta = new Vector2
                (
                    cellWidth * CellSideLength - cellWidth + 1,
                    cellHeight * CellSideLength - cellHeight + 1
                );
                _contentRectTransform.sizeDelta = new Vector2
                (
                    cellWidth * CellSideLength - cellWidth - 1,
                    cellHeight * CellSideLength - cellHeight - 1
                );
                _contentRectTransform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                _rectTransform.sizeDelta = new Vector2
                (
                    cellWidth * CellSideLength - cellWidth + 1,
                    cellHeight * CellSideLength - cellHeight + 1
                );
                _contentRectTransform.sizeDelta = new Vector2
                (
                    cellHeight * CellSideLength - cellHeight - 1,
                    cellWidth * CellSideLength - cellWidth - 1
                );
                _contentRectTransform.localEulerAngles = new Vector3(0, 0, 90);
            }
        }

        private void SetImages()
        {
            _backgroundImage.color = _itemSO.backgroundColor;
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