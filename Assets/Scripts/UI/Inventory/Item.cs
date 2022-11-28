using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        public RectTransform RectTransform = null;
        public Vector2Int OriginCellIndex => _originCellIndex;
        public Item Copy => _copy;
        public Item Source => _source;

        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private ItemSO _itemSO = null;
        private Container _currentContainer = null;
        
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _contentImage;
        [SerializeField] private TMP_Text _nicknameLabel;
        private RectTransform _contentRectTransform = null;
        private Vector2Int _originCellIndex = Vector2Int.zero;
        private Item _copy = null;
        private Item _source = null;

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
            _nicknameLabel.text = _itemSO.nickname;
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
            RectTransform.SetParent(container.ContentsParent);
            Vector2 position = new Vector2(originCellIndex.x * (CellSideLength - 1), -1 * originCellIndex.y * (CellSideLength - 1));
            RectTransform.localPosition = position;
        }

        public void RemoveContainer()
        {
            _currentContainer = null;
            RectTransform.SetParent(null);
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

        public Item MakeCopy()
        {
            if (_copy != null)
            {
                Debug.LogError($"Item {name} already has a copy. There should never be more than one copy of an item at a time.");
                return null;
            }

            _copy = Instantiate(this, RectTransform.parent);
            _copy._source = this;
            Debug.Log($"Made source of {_copy._source.name}.");
            Debug.Log($"Made source of 2 {this.name}.");
            return _copy;
        }

        public bool TransferTo(Container container, Vector2Int cellIndex)
        {
            if (container == null) return false;
            return container.PlaceItemAt(this, cellIndex);
        }

        public void Destroy()
        {
            Debug.Log("Destroying " + name);
            if (_source != null)
            {
                _source._copy = null;
                _source = null;
            }
            Destroy(gameObject);
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            _contentRectTransform = _contentImage.GetComponent<RectTransform>();
        }

        /// <summary>
        /// Sets the RectTransform size of the item based on the cellWidth and cellHeight
        /// </summary>
        private void SetSize()
        {
            if (!isRotated)
            {
                RectTransform.sizeDelta = new Vector2
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
                RectTransform.sizeDelta = new Vector2
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