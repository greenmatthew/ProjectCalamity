using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PC.UI
{
    public class PauseMenu : MenuBase
    {
        [SerializeField] private Button _closePauseMenuButton = null;
        [SerializeField] private Button _exitGameButton = null;

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

        public void ExitGame()
        {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        }
    }
}