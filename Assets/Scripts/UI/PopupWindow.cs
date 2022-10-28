using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PC.UI
{
    public class PopupWindow : MonoBehaviour
    {
        #region Fields

        #region Consts Fields

        private const string _popupWindowPrefabPath = "Prefabs/UI/PR_PopupWindow";
        private const float _defaultPopupWindowWidth = 300f;
        private const float _defaultPopupWindowHeight = 200f;
        private const float _minPopupWindowWidth = 100f;
        private const float _minPopupWindowHeight = 75f;

        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _message;
        [SerializeField] private Transform _buttonPrefab;
        [SerializeField] private Transform _buttonContent;

        private readonly List<Button> _buttons = new List<Button>();

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods

        /// <summary>
        /// Create a popup window.
        /// </summary>
        /// <param name="parent"The parent transform to attach the popup window to.</param>
        /// <param name="title">The title of the popup window.</param>
        /// <param name="message">The message of the popup window.</param>
        /// <param name="buttonAction">The required single button of the popup window.</param>
        /// <param name="additionalButtonActions">The optional multiple buttons of the popup window.</param>
        /// <returns>The GameObject created with the given parameters.</returns>
        public static GameObject Create(GameObject parent, string title, string message, ButtonAction buttonAction, params ButtonAction[] additionalButtonActions)
        {
            return Create(parent, _defaultPopupWindowWidth, _defaultPopupWindowHeight, title, message, buttonAction, additionalButtonActions);
        }

        /// <summary>
        /// Create a popup window.
        /// </summary>
        /// <param name="parent">The parent transform to attach the popup window to.</param>
        /// <param name="width">The desired width of the popup window.</param>
        /// <param name="height">The desired height of the popup window.</param>
        /// <param name="title">The title of the popup window.</param>
        /// <param name="message">The message of the popup window.</param>
        /// <param name="buttonAction">The required single button of the popup window.</param>
        /// <param name="additionalButtonActions">The optional multiple buttons of the popup window.</param>
        /// <returns>The GameObject created with the given parameters.</returns>
        public static GameObject Create(GameObject parent, float width, float height, string title, string message, ButtonAction buttonAction, params ButtonAction[] additionalButtonActions)
        {
            // Make sure the popup window is not too small
            width = Mathf.Max(width, _minPopupWindowWidth);
            height = Mathf.Max(height, _minPopupWindowHeight);

            var prefab = Resources.Load<GameObject>(_popupWindowPrefabPath);
            var window = GameObject.Instantiate(prefab, parent.transform).GetComponent<PopupWindow>();
            window.Init(width, height, title, message, buttonAction, additionalButtonActions);
            return window.gameObject;
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Add a button to this popup window.
        /// </summary>
        /// <param name="buttonAction">The label and action to perform when the button is clicked.</param>
        private void AddButton(ButtonAction buttonAction)
        {
            var buttonInstance = Instantiate(_buttonPrefab, _buttonContent);
            var label = buttonInstance.GetComponentInChildren<TMP_Text>();
            var button = buttonInstance.GetComponent<Button>();
            label.text = buttonAction.Label;
            if (buttonAction.Action != null)
                button.onClick.AddListener(buttonAction.Action);
            button.onClick.AddListener(Close);
            // Turn on game object so it is visible.
            buttonInstance.gameObject.SetActive(true);

            // Add to list of buttons.
            _buttons.Add(button);
        }

        /// <summary>
        /// Initialize the popup window.
        /// </summary>
        /// <param name="width">The desired width of the popup window.</param>
        /// <param name="height">The desired height of the popup window.</param>
        /// <param name="title">The title of the popup window.</param>
        /// <param name="message">The message of the popup window.</param>
        /// <param name="buttonAction">The required single button of the popup window.</param>
        /// <param name="additionalButtonActions">The optional multiple buttons of the popup window.</param>
        private void Init(float width, float height, string title, string message, ButtonAction buttonAction, params ButtonAction[] additionalButtonActions)
        {
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, height);
            _title.text = title;
            _message.text = message;
            AddButton(buttonAction);
            foreach (var additionalButton in additionalButtonActions)
                AddButton(additionalButton);
            
            foreach (var button in _buttons)
            {
                var rect = button.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(width / _buttons.Count, Mathf.Min(rect.sizeDelta.y, height));
            }
                
        }

        /// <summary>
        /// Close the popup window.
        /// </summary>
        private void Close()
        {
            Destroy(gameObject);
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}