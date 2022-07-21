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

        private void Start()
        {
            // Controls initialization
            _inputActions.PauseMenu.ClosePauseMenu.performed += ctx => Close();

            // UI initialization
            _closePauseMenuButton.onClick.AddListener(() => Close());
            _exitGameButton.onClick.AddListener(() => ExitGame());
        }

        public override void Open()
        {
            base.Open();
            _inputActions.PauseMenu.Enable();
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