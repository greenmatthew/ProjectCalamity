using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PC.UI
{
    public class PopupWindow : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
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
            var prefab = Resources.Load<GameObject>("Prefabs/PR_PopupWindow");
            var window = GameObject.Instantiate(prefab, parent.transform).GetComponent<PopupWindow>();
            window.Init(title, message, buttonAction, additionalButtonActions);
            return window.gameObject;
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
            var prefab = Resources.Load<GameObject>("Prefabs/PR_PopupWindow");
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
        }

        /// <summary>
        /// Initialize the popup window.
        /// </summary>
        /// <param name="title">The title of the popup window.</param>
        /// <param name="message">The message of the popup window.</param>
        /// <param name="buttonAction">The required single button of the popup window.</param>
        /// <param name="additionalButtonActions">The optional multiple buttons of the popup window.</param>
        private void Init(string title, string message, ButtonAction buttonAction, params ButtonAction[] additionalButtonActions)
        {
            _title.text = title;
            _message.text = message;
            AddButton(buttonAction);
            foreach (var additionalButton in additionalButtonActions)
                AddButton(additionalButton);
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
            Init(title, message, buttonAction, additionalButtonActions);
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