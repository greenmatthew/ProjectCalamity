using UnityEngine;
using UnityEngine.UI;

namespace PC.UI
{
    public class OLD_ContainerBackground : MonoBehaviour
    {
        private Transform _transform;
        private OLD_Container _container;
        private OLD_ContainerSO _containerSO;
        [SerializeField] private RectTransform _backgroundSlot;
        private GridLayoutGroup _gridLayoutGroup;

        private void Awake()
        {
            _transform = transform;
            _container = _transform.parent.GetComponent<OLD_Container>();
            _containerSO = _container._containerSO;
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();

            // Load prefab from resources dir
            _backgroundSlot = Resources.Load<RectTransform>("Prefabs/UI/PR_InventorySlotTemplate");
            _backgroundSlot.sizeDelta = new Vector2(_containerSO.Width, _containerSO.Height);
        }

        private void Start()
        {
            InitGridLayoutGroup();
            PopulateSlots();
        }

        private void InitGridLayoutGroup()
        {
            _gridLayoutGroup.cellSize = new Vector2(OLD_Container.SlotSideLength, OLD_Container.SlotSideLength);
            // _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            // _gridLayoutGroup.constraintCount = _containerSO.Width;
            //_gridLayoutGroup.padding.left = _gridLayoutGroup.padding.right = _gridLayoutGroup.padding.top = _gridLayoutGroup.padding.bottom = 0;
        }

        private void PopulateSlots()
        {
            int slots = _containerSO.Width * _containerSO.Height;
            for (int i = 0; i < slots; ++i)
            {
                var slot = Instantiate(_backgroundSlot, _transform);
                slot.gameObject.SetActive(true);
                //slot.localPosition = new Vector3(j * Container.SlotSideLength, -i * Container.SlotSideLength, 0);
            }
        }
    }
}
