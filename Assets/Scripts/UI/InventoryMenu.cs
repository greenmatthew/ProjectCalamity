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
        public static ContainerBase CurrentContainer
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
        private ContainerBase h_currentContainer = null;
        private ContainerBase _currentContainer
        {
            get
            {
                return h_currentContainer;
            }
            set
            {
                h_currentContainer = value;
                if (h_currentContainer != null && _currentItemCopy != null)
                    h_currentContainer.SetItemParent(_currentItemCopy.RectTransform);
            }
        }

        private Item h_currentItemSource = null;
        private Item _currentItemSource
        {
            get { return h_currentItemSource; }
            set
            {
                if (value != null)
                {
                    h_currentItemSource = value;
                    _currentItemCopy = h_currentItemSource.MakeCopy();
                }
                else
                {
                    h_currentItemSource = value;
                    _currentItemCopy = value;
                }
            }
        }
        private Item _currentItemCopy = null;
        

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
            if (_currentItemCopy != null)
                _currentItemCopy.RectTransform.position = UnityEngine.Input.mousePosition;
        }

        private void TryPickingUpItem(Vector2Int cellIndex)
        {
            if (_currentItemSource == null && _currentItemCopy == null)
            {
                _currentItemSource = _currentContainer.GetItemAt(cellIndex);
                if (_currentItemSource == null && _currentItemCopy == null) return;
                _currentItemCopy.RectTransform.SetAsLastSibling();
            }
            else
            {
                Debug.LogError("TryPickingUpItem() called but _currentItem is not null");
            }
        }

        private void TryReleasingItem(Vector2Int cellIndex)
        {
            if (_currentItemCopy != null)
            {
                if (_currentItemCopy.TransferTo(_currentContainer, cellIndex))
                {
                    var name = _currentItemSource.transform.name;
                    _currentItemSource.Destroy();
                    _currentItemCopy.transform.name = name;
                }
                else
                {
                    _currentItemCopy.Destroy();
                }
                _currentItemSource = null;
            }
            else
            {
                Debug.LogError("TryReleasingItem() called but _currentItem is null");
            }
        }

        private void TryRotatingItem()
        {
            if (_currentItemCopy != null)
            {
                _currentItemCopy.Rotate();
            }
        }
        
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}