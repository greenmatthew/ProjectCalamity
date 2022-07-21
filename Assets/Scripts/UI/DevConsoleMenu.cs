using UnityEngine;

namespace PC.UI
{
    public class DevConsoleMenu : MenuBase
    {
        private void Start()
        {
            m_inputActions.DevConsoleMenu.CloseDevConsoleMenu.performed += ctx => Close();
        }

        public override void Open()
        {
            base.Open();
            m_inputActions.DevConsoleMenu.Enable();
        }
    }
}
