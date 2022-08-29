using System.Threading;
using System.Collections.Generic;
using UnityEngine;

using PC.Extensions;

namespace PC.UI
{
    public class DevConsoleMenu : MenuBase
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        [SerializeField] private RectTransform _content = null;
        [SerializeField] private RectTransform _logItemPrefab = null;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods
        #endregion Public Methods

        #region Protected Methods

        protected override void AwakeExtension()
        {
            _inputActions.DevConsoleMenu.CloseMenu.performed += ctx => Close();

            Debug.Log("Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test");
        }

        protected override void OpenExtension()
        {
            _inputActions.DevConsoleMenu.Enable();
        }

        protected override void CloseExtension()
        {
        }

        #endregion Protected Methods

        #region Private Methods

        private void Update()
        {
            if (Debug.Backlog.Count > 0)
            {
                string message = Debug.Backlog.Dequeue();
                RectTransform logItem = Instantiate(_logItemPrefab, _content);
                var text = logItem.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                text.text = message;
            }
        }

        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}