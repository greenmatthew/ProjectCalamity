using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PC.UI
{
    public class PopupWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _message;
        [SerializeField] private Transform _buttonPrefab;
        [SerializeField] private Transform _buttonContent;
        
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

        private void Init(string title, string message, ButtonAction buttonAction, params ButtonAction[] additionalButtons)
        {
            _title.text = title;
            _message.text = message;
            AddButton(buttonAction);
            foreach (var additionalButton in additionalButtons)
                AddButton(additionalButton);
        }

        private void Init(float width, float height, string title, string message, ButtonAction buttonAction, params ButtonAction[] additionalButtons)
        {
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width, height);
            Init(title, message, buttonAction, additionalButtons);
        }

        private void Close()
        {
            Destroy(gameObject);
        }

        public static GameObject Create(GameObject parent, string title, string message, ButtonAction buttonAction, params ButtonAction[] additionalButtons)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/PR_PopupWindow");
            var window = GameObject.Instantiate(prefab, parent.transform).GetComponent<PopupWindow>();
            window.Init(title, message, buttonAction, additionalButtons);
            return window.gameObject;
        }

        public static GameObject Create(GameObject parent, float width, float height, string title, string message, ButtonAction buttonAction, params ButtonAction[] additionalButtons)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/PR_PopupWindow");
            var window = GameObject.Instantiate(prefab, parent.transform).GetComponent<PopupWindow>();
            window.Init(width, height, title, message, buttonAction, additionalButtons);
            return window.gameObject;
        }
    }
}
