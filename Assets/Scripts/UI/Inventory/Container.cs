using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.UI
{
    public class Container : MonoBehaviour
    {
        public const float SlotSideLength = 64f;

        [SerializeField] public ContainerSO _containerSO;
        private RectTransform _rectTransform;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            var size = new Vector2(_containerSO.Width * SlotSideLength, _containerSO.Height * SlotSideLength);
            _rectTransform.sizeDelta = size;
            _rectTransform.parent.GetComponent<RectTransform>().sizeDelta = size;
        }
    }
}
