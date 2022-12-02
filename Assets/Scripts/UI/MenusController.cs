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

        public static PauseMenu PauseMenu => _instance._pauseMenu;
        public static InventoryMenu InventoryMenu => _instance._inventoryMenu;
        public static DevConsoleMenu DevConsoleMenu => _instance._devConsoleMenu;
        public static MapMenu MapMenu => _instance._mapMenu;
        public static PMSMenu PMSMenu => _instance._pmsMenu;

        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private static MenusController _instance = null;

        [SerializeField] private PauseMenu _pauseMenu = null;
        [SerializeField] private InventoryMenu _inventoryMenu = null;
        [SerializeField] private DevConsoleMenu _devConsoleMenu = null;
        [SerializeField] private MapMenu _mapMenu = null;
        [SerializeField] private PMSMenu _pmsMenu = null;

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
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError("MenusController: Instance already exists, destroying object!");
                Destroy(gameObject);
            }
                
            _pauseMenu = GetComponentInChildren<PauseMenu>(true);
            if (_pauseMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: PauseMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
            
            _inventoryMenu = GetComponentInChildren<InventoryMenu>(true);
            if (_inventoryMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: InventoryMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");

            _devConsoleMenu = GetComponentInChildren<DevConsoleMenu>(true);
            if (_devConsoleMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: DevConsoleMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
            
            _mapMenu = GetComponentInChildren<MapMenu>(true);
            if (_mapMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: MapMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
            
            _pmsMenu = GetComponentInChildren<PMSMenu>(true);
            if (_pmsMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: PMSMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}