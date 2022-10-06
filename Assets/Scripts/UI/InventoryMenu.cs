using UnityEngine;

namespace PC.UI
{
    public class InventoryMenu : MenuBase
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private New_Container _currentContainer = null;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods
        #endregion Public Methods

        #region Protected Methods

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

        #endregion Protected Methods

        #region Private Methods

        private void Update()
        {
            if (_currentContainer == null) return;

            _currentContainer.GetGridPosition(UnityEngine.Input.mousePosition);
        }
        
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}