using UnityEngine;
using UnityEngine.UI;
using TMPro;

using PC.Extensions;
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
        public ItemType type;

        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private ItemSO _itemSO = null;
        private ContainerBase _currentContainer = null;
        
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

        /// <summary>
        /// Initializes the item.
        /// </summary>
        /// <param name="itemSO">The itemSO to set the item's details to.</param>
        /// <returns>The item.</returns>
        public Item Init(ItemSO itemSO)
        {
            _itemSO = itemSO;
            cellWidth = _itemSO.cellWidth;
            cellHeight = _itemSO.cellHeight;
            SetSize();
            SetImages();
            _nicknameLabel.text = _itemSO.nickname;
            transform.name = _itemSO.name;
            
            type = ItemType.Lookup(_itemSO.type);
            if (type == ItemType.Value.NONE)
            {
                Debug.LogError($"Item {name} type is not set @ {transform.HierarchyPath()}.");
            }

            return this;
        }

        /// <summary>
        /// Caches the container the item is currently in.
        /// Sets the item's parent to the given container's content GameObject.
        /// Sets the item's position to the given cell index relative to the container's content GameObject.
        /// </summary>
        public void SetContainer(ContainerBase container, Vector2Int originCellIndex)
        {
            if (container == null) return;
            _currentContainer = container;
            _originCellIndex = originCellIndex;
            RectTransform.SetParent(container.ContentsParent);
            Vector2 position = new Vector2(originCellIndex.x * (CellSideLength - 1), -1 * originCellIndex.y * (CellSideLength - 1));
            
            RectTransform.localPosition = position;
        }

        /// <summary>
        /// Removes the item's container reference and sets its parent to null.
        /// </summary>
        public void RemoveContainer()
        {
            _currentContainer = null;
            RectTransform.SetParent(null);
        }

        /// <summary>
        /// Gets the origin cell index of the item, relative to its container's content 2D array.
        /// </summary>
        /// <returns>The origin cell index of the item.</returns>
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

        /// <summary>
        /// Duplicates the item as another item.
        /// </summary>
        /// <returns>The duplicate item.</returns>
        public Item MakeCopy()
        {
            if (_copy != null)
            {
                Debug.LogError($"Item {name} already has a copy. There should never be more than one copy of an item at a time.");
                return null;
            }

            _copy = Instantiate(this, RectTransform.parent);
            _copy.type = type;
            _copy._source = this;
            return _copy;
        }

        /// <summary>
        /// Transfers the item to a given container at a given cell index.
        /// </summary>
        /// <param name="container">The container to transfer the item to.</param>
        /// <param name="cellIndex">The cell index to transfer the item to.</param>
        /// <returns>True if the transfer was successful, false otherwise.</returns>
        public bool TransferTo(ContainerBase container, Vector2Int cellIndex)
        {
            if (container == null) return false;
            return container.PlaceItemAt(this, cellIndex);
        }

        /// <summary>
        /// Destroys the item.
        /// </summary>
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

        /// <summary>
        /// Sets the item's images based on the item's ItemSO.
        /// </summary>
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