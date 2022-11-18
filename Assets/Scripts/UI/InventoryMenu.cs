using System.Text;
using System.Collections.Generic;
using UnityEngine;

using PC.Extensions;

namespace PC.UI
{
    public class InventoryMenu : MenuBase
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields

        // The current container that the player is interacting with
        public static Container CurrentContainer
        {
            set
            {
                instance._currentContainer = value;
            }
        }

        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        // Singletone vars
        private static List<string> _hierarchyPaths = new List<string>();
        private static InventoryMenu h_instance = null;
        private static InventoryMenu instance
        {
            get
            {
                if (h_instance == null)
                    Debug.LogError($"InventoryMenu.instance is null. Please make sure to have a {Debug.FormatBold("single")} instance in the scene.");
                return h_instance;
            }

            set
            {
                var mb = (MonoBehaviour)value;
                // Grab scene hiearchy path
                var hierarchyPath = mb.gameObject.HierarchyPath(true);
                _hierarchyPaths.Add(hierarchyPath);
                if (h_instance != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"InventoryMenu is a Singleton object, however, there is more than one active instance in the scene. Please remove all but one instances from the scene with help from this hierarchy pathes list:");
                    foreach (var path in _hierarchyPaths)
                        sb.AppendLine(Debug.FormatPath(path));
                    Debug.LogError(sb.ToString());
                    h_instance = null;
                    return;
                }
                h_instance = value;
            }
        }
        private Container h_currentContainer = null;
        private Container _currentContainer
        {
            get
            {
                return h_currentContainer;
            }
            set
            {
                h_currentContainer = value;
                if (h_currentContainer != null && _currentItem != null)
                    h_currentContainer.SetItemParent(_currentItem.rectTransform);
            }
        }
        private ItemContainerInfo _currentItem = null;
        // private ItemContainerInfo _currentItem = null;

        // private Item _currItemSource = null;
        // private Item _currItemCopy = null;
        

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods
        #endregion Public Methods

        #region Protected Methods

        protected override void AwakeExtension()
        {
            instance = this;

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
            ClampCurrentItemToCursor();

            if (_currentContainer == null) return;

            var cellIndex = _currentContainer.GetCellIndex(UnityEngine.Input.mousePosition);

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                TryPickingUpItem(cellIndex);
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                TryReleasingItem(cellIndex);
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                TryRotatingItem();
            }
        }

        private void ClampCurrentItemToCursor()
        {
            if (_currentItem != null)
                _currentItem.rectTransform.position = UnityEngine.Input.mousePosition;
        }

        private void TryPickingUpItem(Vector2Int cellIndex)
        {
            if (_currentItem == null)
            {
                _currentItem = ItemContainerInfo.Create(_currentContainer, cellIndex);
                if (_currentItem == null) return;
                _currentItem.rectTransform.SetAsLastSibling();
            }
            else
            {
                Debug.LogError("TryPickingUpItem() called but _currentItem is not null");
            }
        }

        private void TryReleasingItem(Vector2Int cellIndex)
        {
            if (_currentItem != null)
            {
                if (!_currentContainer.PlaceItemAt(_currentItem.item, cellIndex))
                {
                    if (_currentItem.item.isRotated != _currentItem.wasRotated)
                        _currentItem.item.Rotate();
                    _currentItem.sourceContainer.PlaceItemAt(_currentItem.item, _currentItem.sourceIndex);
                }
                    
                _currentItem = null;
            }
            else
            {
                Debug.LogError("TryReleasingItem() called but _currentItem is null");
            }
        }

        private void TryRotatingItem()
        {
            if (_currentItem != null)
            {
                _currentItem.item.Rotate();
            }
        }
        
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}