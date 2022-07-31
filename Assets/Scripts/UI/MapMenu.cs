using UnityEngine;

namespace PC.UI
{
    public class MapMenu : MenuBase
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods
        #endregion Public Methods

        #region Protected Methods

        protected override void AwakeExtension()
        {
            _inputActions.MapMenu.CloseMenu.performed += ctx => Close();
        }

        protected override void OpenExtension()
        {
            _inputActions.MapMenu.Enable();
        }

        protected override void CloseExtension()
        {
        }

        #endregion Protected Methods

        #region Private Methods
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}