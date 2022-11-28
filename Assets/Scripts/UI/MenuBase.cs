using System.Collections.Generic;
using UnityEngine;

using PC.Input;

namespace PC.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class MenuBase : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields

        protected static InputActions _inputActions => InputModule.InputActions;

        #endregion Protected Fields

        #region Private Fields
        
        private static readonly List<MenuBase> menus = new List<MenuBase>();

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods

        /// <summary>
        /// DO NOT OVERRIDE/HIDE MenuBase.Open() USING THE NEW OR OVERRIDE KEYWORDS!!!
        /// Override the OpenExtension() function instead.
        /// Opens the menu. Closes all other menus. Enables base menu input while disabling all others. Unlocks the cursor.
        /// Calls OpenExtension() at end end of this function. OpenExtension() by default does nothing. Needs to be overriden if you want to add to the functionality of Open().
        /// </summary>
        public void Open()
        {
            DisableAll();
            transform.gameObject.SetActive(true);
            _inputActions.Menu.Enable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            OpenExtension();
        }

        /// <summary>
        /// DO NOT OVERRIDE/HIDE MenuBase.Close() USING THE NEW OR OVERRIDE KEYWORDS!!!
        /// Override the CloseExtension() function instead.
        /// Closes the menu. Locks the cursor.
        /// Calls CloseExtension() at end end of this function. CloseExtension() by default does nothing. Needs to be overriden if you want to add to the functionality of Close().
        public void Close()
        {
            DisableAll();
            _inputActions.Player.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            CloseExtension();
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// DO NOT HIDE/OVERRIDE THIS FUNCTION USING THE NEW OR OVERRIDE KEYWORDS!!!
        /// Override the AwakeExtension() function instead.
        /// </summary>
        protected void Awake()
        {
            menus.Add(this);

            _inputActions.Menu.CloseMenu.performed += ctx => Close();

            AwakeExtension();
        }

        /// <summary>
        /// DO NOT HIDE/OVERRIDE MenuBase.Awake() USING THE NEW OR OVERRIDE KEYWORDS!!!
        /// AwakeExtension() is called at the end of MenuBase.Awake(). If you need any code to run in MonoBehaviour.Awake() put it inside AwakeExtension().
        /// </summary>
        protected abstract void AwakeExtension();

        /// <summary>
        /// Gets called at the end of Open().
        /// </summary>
        protected abstract void OpenExtension();

        /// <summary>
        /// Gets called at the end of Close().
        /// </summary>
        protected abstract void CloseExtension();

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Hides the menu from the scene.
        /// </summary>
        private void Disable()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Hides ALL menus from the scene.
        /// </summary>
        private static void DisableAll()
        {
            foreach (var menu in menus)
            {
                menu.Disable();
            }
            _inputActions.Disable();
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
