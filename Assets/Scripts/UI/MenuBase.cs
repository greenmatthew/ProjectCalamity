using UnityEngine;

using PC.Input;

namespace PC.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class MenuBase : MonoBehaviour
    {
        private bool _awakeCalledFlag = false;
        private InputActions h_inputActions = null;
        protected InputActions _inputActions
        {
            get
            {
                if (!_awakeCalledFlag)
                {
                    Debug.LogError("MenuBase: _inputActions is not initialized yet. Please do not access _inputActions till after Awake() is called. If you are overriding MenuBase.Awake(), please call \"base.Awake()\" as first instruction inside \"private void override Awake()\".");
                }

                return h_inputActions;
            }
        }

        protected virtual void Awake()
        {
            h_inputActions = InputModule.instance.InputActions;

            _awakeCalledFlag = true;
        }

        public virtual void Open()
        {
            transform.gameObject.SetActive(true);
            _inputActions.Disable();
            Cursor.lockState = CursorLockMode.None;
        }

        public virtual void Close()
        {
            transform.gameObject.SetActive(false);
            _inputActions.Disable();
            _inputActions.Player.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
