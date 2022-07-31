using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PC.UI
{
    public class PauseMenu : MenuBase
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        [SerializeField] private Button _closePauseMenuButton = null;
        [SerializeField] private Button _exitGameButton = null;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods

        public void ExitGame()
        {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void AwakeExtension()
        {
            _closePauseMenuButton.onClick.AddListener(() => Close());
            _exitGameButton.onClick.AddListener( delegate
            {
                PopupWindow.Create
                (
                    gameObject,
                    "Exit Game",
                    "Are you sure you want to exit the game?",
                    new ButtonAction("Yes", ExitGame),
                    new ButtonAction("No")
                );
            } );
        }

        protected override void OpenExtension()
        {
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