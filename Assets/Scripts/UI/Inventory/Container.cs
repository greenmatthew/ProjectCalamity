using UnityEngine;
using UnityEngine.EventSystems;

namespace PC.UI
{
    public class Container : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Fields

        #region Consts Fields

        private const int cellWidth = 10;
        private const int cellHeight = 10;
        public const float CellSideLength = 64f;

        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private RectTransform _rectTransform = null;

        // Background of the container
        [SerializeField] private RectTransform _anchor = null;
        [SerializeField] private RectTransform _cellBackgroundPrefab = null;
        [SerializeField] private RectTransform _cellBackgroundParent = null;

        // Position/Index within the container
        private Vector2 _mousePos = Vector2.zero;
        private Vector2 _currentPosition = Vector2.zero;
        private Vector2Int _currentCellIndex = Vector2Int.zero;

        // Contents of the container
        [SerializeField] private RectTransform _contentsParent = null;
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
        
        // Init items
        [SerializeField] private Item _itemPrefab = null;
        [SerializeField] private ItemSO[] _items = null;
        [SerializeField] private Vector2Int[] _itemPositions = null;

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

        public Vector2Int GetCellIndex(Vector2 mousePos)
        {
            _mousePos = mousePos;
            _currentPosition = new Vector2(mousePos.x - _anchor.position.x, _anchor.position.y - mousePos.y);
            _currentCellIndex = new Vector2Int((int)(_currentPosition.x / CellSideLength), (int)(_currentPosition.y / CellSideLength));

            return _currentCellIndex;
        }

        public Item GetItemAt(Vector2Int cellIndex)
        {
            if (cellIndex.x < 0 || cellIndex.x >= cellWidth || cellIndex.y < 0 || cellIndex.y >= cellHeight)
            {
                Debug.LogError("Cell index is out of range.");
                return null;
            }

            return _contents[cellIndex.x, cellIndex.y];
        }

        public bool RemoveItemAt(Vector2Int cellIndex)
        {
            if (cellIndex.x < 0 || cellIndex.x >= cellWidth || cellIndex.y < 0 || cellIndex.y >= cellHeight)
            {
                Debug.LogError("Cell index is out of range.");
                return false;
            }

            if (_contents[cellIndex.x, cellIndex.y] == null)
            {
                Debug.LogError("No item at cell index.");
                return false;
            }

            _contents[cellIndex.x, cellIndex.y].SetContainer(null);
            _contents[cellIndex.x, cellIndex.y] = null;
            return true;
        }

        public bool PlaceItemAt(Item item, Vector2Int cellIndex)
        {
            if (item == null)
            {
                Debug.LogError("Item is null.");
                return false;
            }

            if (cellIndex.x < 0 || cellIndex.x >= cellWidth || cellIndex.y < 0 || cellIndex.y >= cellHeight)
            {
                Debug.LogError("Cell index is out of range.");
                return false;
            }

            if (_contents[cellIndex.x, cellIndex.y] != null)
            {
                Debug.LogError("Cell is already occupied.");
                return false;
            }


            _contents[cellIndex.x, cellIndex.y] = item;
            item.SetContainer(this);
            var rectTransform = item.GetComponent<RectTransform>();
            rectTransform.SetParent(_contentsParent);
            Vector2 position = new Vector2(cellIndex.x * (CellSideLength - 1), -1 * cellIndex.y * (CellSideLength - 1));
            rectTransform.localPosition = position;
            return true;
        }

        public Item TakeItemAt(Vector2Int cellIndex)
        {
            var item = GetItemAt(cellIndex);
            if (item != null)
                RemoveItemAt(cellIndex);
            return item;
        }

        public void SetItemParent(RectTransform itemRectTransform)
        {
            itemRectTransform.SetParent(_contentsParent);
            itemRectTransform.SetAsLastSibling();
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            var size = new Vector2(cellWidth * CellSideLength, cellHeight * CellSideLength);

            InitBackground(size);
            InitContents(size);

            for (int i = 0; i < _items.Length; ++i)
                PlaceItemAt(Instantiate(_itemPrefab).Init(_items[i]), _itemPositions[i]);
        }

        private void InitBackground(Vector2 size)
        {
            _rectTransform.sizeDelta = size;
            _cellBackgroundParent.sizeDelta = size;
            for (int i = 0; i < cellWidth; ++i)
            {
                for (int j = 0; j < cellHeight; ++j)
                {
                    RectTransform slotBackground = Instantiate(_cellBackgroundPrefab, _cellBackgroundParent);
                }
            }
        }

        private void InitContents(Vector2 size)
        {
            _rectTransform.sizeDelta = size;
            _contentsParent.sizeDelta = size;

            _contents = new Item[cellWidth, cellHeight];
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}