using UnityEngine;

namespace PC.UI
{
    public class InventoryMenu : MenuBase
    {
        protected override void AwakeExtension()
        {
            _inputActions.InventoryMenu.CloseMenu.performed += ctx => Close();
        }

        protected override void OpenExtension()
        {
            _inputActions.InventoryMenu.Enable();
        }

        protected override void CloseExtension()
        {
        }
    }
}