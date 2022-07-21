using UnityEngine;

namespace PC.UI
{
    public class InventoryMenu : MenuBase
    {
        private void Start()
        {
            _inputActions.InventoryMenu.CloseInventoryMenu.performed += ctx => Close();
        }

        public override void Open()
        {
            base.Open();
            _inputActions.InventoryMenu.Enable();
        }
    }
}
