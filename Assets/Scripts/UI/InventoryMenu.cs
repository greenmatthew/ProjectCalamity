using UnityEngine;

namespace PC.UI
{
    public class InventoryMenu : MenuBase
    {
        private void Start()
        {
            m_inputActions.InventoryMenu.CloseInventoryMenu.performed += ctx => Close();
        }

        public override void Open()
        {
            base.Open();
            m_inputActions.InventoryMenu.Enable();
        }
    }
}
