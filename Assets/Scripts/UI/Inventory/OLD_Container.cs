using UnityEngine;
using UnityEngine.UI;

using PC.Extensions;

namespace PC.UI
{
    public class OLD_Container : MonoBehaviour
    {
        #region Fields

        #region Consts Fields

        public const float SlotSideLength = 64f;

        #endregion Consts Fields

        #region Public Fields

        [SerializeField] public OLD_ContainerSO _containerSO;

        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private RectTransform _rectTransform;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods
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
            var size = new Vector2(_containerSO.Width * SlotSideLength, _containerSO.Height * SlotSideLength);
            _rectTransform.sizeDelta = size;
            // var parent = _rectTransform.parent.GetComponent<RectTransform>();
            // SetParentsHeight(parent, size.y);
        }

        private void SetParentsHeight(RectTransform rectTransform, float height)
        {
            while (rectTransform != null)
            {
                Debug.Log(rectTransform.transform.HierarchyPath());

                HorizontalOrVerticalLayoutGroup layoutGroup = rectTransform.GetComponent<HorizontalLayoutGroup>();
                if (layoutGroup != null)
                    rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
                layoutGroup = rectTransform.GetComponent<VerticalLayoutGroup>();
                if (layoutGroup != null)
                    rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.x + height);
                else
                    return;

                rectTransform = rectTransform.parent.GetComponent<RectTransform>();
            }
        }

        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
