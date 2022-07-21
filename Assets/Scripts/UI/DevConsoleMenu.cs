using UnityEngine;

namespace PC.UI
{
    public class DevConsoleMenu : MenuBase
    {
        private void Start()
        {
            _inputActions.DevConsoleMenu.CloseDevConsoleMenu.performed += ctx => Close();
        }

        public override void Open()
        {
            base.Open();
            _inputActions.DevConsoleMenu.Enable();
        }
    }
}
