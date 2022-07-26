using UnityEngine;

namespace PC.UI
{
    public class DevConsoleMenu : MenuBase
    {
        protected override void AwakeExtension()
        {
            _inputActions.DevConsoleMenu.CloseMenu.performed += ctx => Close();
        }

        protected override void OpenExtension()
        {
            _inputActions.DevConsoleMenu.Enable();
        }

        protected override void CloseExtension()
        {
        }
    }
}
