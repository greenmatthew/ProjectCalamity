using UnityEngine;
using UnityEngine.UI;

namespace PC.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class LayoutMaintainPreferredHeight : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private RectTransform _rectTransform;
        private HorizontalOrVerticalLayoutGroup _layoutGroup;
        private float _preferredHeight = 0f;

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
            _layoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
        }

        private void Update()
        {
            if (_preferredHeight != _layoutGroup.preferredHeight)
            {
                _preferredHeight = _layoutGroup.preferredHeight;
                _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _preferredHeight);
            }
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}