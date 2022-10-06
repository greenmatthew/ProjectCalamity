using UnityEngine;
using UnityEngine.EventSystems;

namespace PC.UI
{
    public class New_Container : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Fields

        #region Consts Fields

        private const int ContainerWidth = 10;
        private const int ContainerHeight = 10;
        private const float SlotSideLength = 64f;

        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private RectTransform _rectTransform = null;
        [SerializeField] private RectTransform _anchor = null;
        [SerializeField] private RectTransform _slotBackgroundPrefab = null;
        [SerializeField] private RectTransform _slotBackgroundParent = null;
        private Vector2 _mousePos = Vector2.zero;
        private Vector2 _slotPositionRaw = Vector2.zero;
        private Vector2Int _gridPosition = Vector2Int.zero;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Pointer entered");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Pointer exited");
        }

        public Vector2Int GetGridPosition(Vector2 mousePos)
        {
            _slotPositionRaw = new Vector2(mousePos.x - _anchor.position.x, _anchor.position.y - mousePos.y);
            
            _gridPosition = new Vector2Int((int)(_slotPositionRaw.x / SlotSideLength), (int)(_slotPositionRaw.y / SlotSideLength));

            return _gridPosition;
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            var tempSize = new Vector2(ContainerWidth * SlotSideLength, ContainerHeight * SlotSideLength);
            _rectTransform.sizeDelta = tempSize;
            _slotBackgroundParent.sizeDelta = tempSize;
            for (int i = 0; i < ContainerWidth; i++)
            {
                for (int j = 0; j < ContainerHeight; j++)
                {
                    RectTransform slotBackground = Instantiate(_slotBackgroundPrefab, _slotBackgroundParent);
                }
            }
        }

        private void Update()
        {
            _mousePos = UnityEngine.Input.mousePosition;
            GetGridPosition(_mousePos);
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}