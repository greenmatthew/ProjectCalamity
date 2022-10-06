using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PC.UI
{
    [RequireComponent(typeof(RectTransform), typeof(HorizontalLayoutGroup))]
    public class HorizontalLayoutGroupHandler : MonoBehaviour
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
        private List<RectTransform> _children = new List<RectTransform>();

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

            foreach (RectTransform child in _rectTransform)
            {
                _children.Add(child);
            }
        }

        private void Update()
        {
            float height = _rectTransform.sizeDelta.y;
            foreach (RectTransform child in _children)
                height = Mathf.Max(height, child.sizeDelta.y);
                
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, height);
        }
        
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}