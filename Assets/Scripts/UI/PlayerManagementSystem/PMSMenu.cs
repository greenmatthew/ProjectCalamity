using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PC.UI
{
    public class PMSMenu : MenuBase
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        [SerializeField] private TMP_Text _label = null;
        [SerializeField] private Button _questsModuleButton = null;
        [SerializeField] private Button _marketModuleButton = null;
        [SerializeField] private Button _stashModuleButton = null;
        [SerializeField] private Button _craftingModuleButton = null;
        [SerializeField] private Button _suitModuleButton = null;
        [SerializeField] private Button _weaponUpgradingModuleButton = null;
        [SerializeField] private Button _transportationModuleButton = null;
        [SerializeField] private Button _memoryModuleButton = null;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods
        #endregion Public Methods

        #region Protected Methods

        protected override void AwakeExtension()
        {
            _inputActions.PMSMenu.CloseMenu.performed += ctx => Close();

            _stashModuleButton.onClick.AddListener(() => MenusController.InventoryMenu.OpenWithStash());
        }

        protected override void OpenExtension()
        {
            _inputActions.PMSMenu.Enable();
        }

        protected override void CloseExtension()
        {
        }

        public void Init(BetaPMS PMS)
        {
            if (PMS == null)
            {
                Debug.LogError("PMS is null");
                return;
            }

            _label.text = "β Player Management System (βPMS)";

            _questsModuleButton.interactable = PMS.QuestsModule != null;
            _marketModuleButton.interactable = false;
            _stashModuleButton.interactable = false;
            _craftingModuleButton.interactable = PMS.CraftingModule != null;
            _suitModuleButton.interactable = PMS.SuitModule != null;
            _weaponUpgradingModuleButton.interactable = PMS.WeaponUpgradingModule != null;
            _transportationModuleButton.interactable = false;
            _memoryModuleButton.interactable = false;
        }

        public void Init(AlphaPMS PMS)
        {
            if (PMS == null)
            {
                Debug.LogError("PMS is null");
                return;
            }

            _label.text = "α Player Management System (αPMS)";

            _questsModuleButton.interactable = PMS.QuestsModule != null;
            _marketModuleButton.interactable = PMS.MarketModule != null;
            _stashModuleButton.interactable = PMS.StashModule != null;
            _craftingModuleButton.interactable = PMS.CraftingModule != null;
            _suitModuleButton.interactable = PMS.SuitModule != null;
            _weaponUpgradingModuleButton.interactable = PMS.WeaponUpgradingModule != null;
            _transportationModuleButton.interactable = PMS.TransportationModule != null;
            _memoryModuleButton.interactable = PMS.MemoryModule != null;
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