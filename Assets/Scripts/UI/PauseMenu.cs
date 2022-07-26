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
            _exitGameButton.onClick.AddListener(() => ExitGame());
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