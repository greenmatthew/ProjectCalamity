using UnityEngine;

using PC.Input;

namespace PC.UI
{
    public class MenusController : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields

        public PauseMenu PauseMenu = null;
        public InventoryMenu InventoryMenu = null;
        public DevConsoleMenu DevConsoleMenu = null;
        public MapMenu MapMenu = null;
        public PMSMenu PMSMenu = null;

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
        #endregion Protected Methods

        #region Private Methods

        private void Awake()
        {
            PauseMenu = GetComponentInChildren<PauseMenu>(true);
            if (PauseMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: PauseMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
            
            InventoryMenu = GetComponentInChildren<InventoryMenu>(true);
            if (InventoryMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: InventoryMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");

            DevConsoleMenu = GetComponentInChildren<DevConsoleMenu>(true);
            if (DevConsoleMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: DevConsoleMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
            
            MapMenu = GetComponentInChildren<MapMenu>(true);
            if (MapMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: MapMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
            
            PMSMenu = GetComponentInChildren<PMSMenu>(true);
            if (PMSMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: PMSMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}