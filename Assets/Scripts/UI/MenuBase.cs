using UnityEngine;

using PC.Input;

namespace PC.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class MenuBase : MonoBehaviour
    {
        protected InputActions _inputActions => InputModule.InputActions;

        /// <summary>
        /// DO NOT HIDE/OVERRIDE THIS FUNCTION USING THE NEW OR OVERRIDE KEYWORDS!!!
        /// Override the AwakeExtension() function instead.
        /// </summary>
        protected void Awake()
        {
            _inputActions.Menu.CloseMenu.performed += ctx => Close();

            AwakeExtension();
        }

        /// <summary>
        /// DO NOT HIDE/OVERRIDE MenuBase.Awake() USING THE NEW OR OVERRIDE KEYWORDS!!!
        /// AwakeExtension() is called at the end of MenuBase.Awake(). If you need any code to run in MonoBehaviour.Awake() put it inside AwakeExtension().
        /// </summary>
        protected abstract void AwakeExtension();

        public void Open()
        {
            transform.gameObject.SetActive(true);
            _inputActions.Disable();
            _inputActions.Menu.Enable();
            Cursor.lockState = CursorLockMode.None;

            OpenExtension();
        }

        public abstract void OpenExtension();

        public void Close()
        {
            transform.gameObject.SetActive(false);
            _inputActions.Disable();
            _inputActions.Player.Enable();
            Cursor.lockState = CursorLockMode.Locked;

            CloseExtension();
        }

        public abstract void CloseExtension();
    }
}
