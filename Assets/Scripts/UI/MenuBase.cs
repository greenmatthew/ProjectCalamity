using UnityEngine;

using PC.Input;

namespace PC.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class MenuBase : MonoBehaviour
    {
        private bool m_awakeCalledFlag = false;
        private InputActions _inputActions = null;
        protected InputActions m_inputActions
        {
            get
            {
                if (!m_awakeCalledFlag)
                {
                    Debug.LogError("MenuBase: m_inputActions is not initialized yet. Please do not access m_inputActions till after Awake() is called. If you are overriding MenuBase.Awake(), please call \"base.Awake()\" as first instruction inside \"private void override Awake()\".");
                }

                return _inputActions;
            }
        }

        protected virtual void Awake()
        {
            _inputActions = InputModule.instance.InputActions;

            m_awakeCalledFlag = true;
        }

        public virtual void Open()
        {
            transform.gameObject.SetActive(true);
            m_inputActions.Disable();
            Cursor.lockState = CursorLockMode.None;
        }

        public virtual void Close()
        {
            transform.gameObject.SetActive(false);
            m_inputActions.Disable();
            m_inputActions.Player.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
