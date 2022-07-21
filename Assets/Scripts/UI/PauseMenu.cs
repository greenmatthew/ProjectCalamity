using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PC.UI
{
    public class PauseMenu : MenuBase
    {
        [SerializeField] private Button m_closePauseMenuButton = null;
        [SerializeField] private Button m_exitGameButton = null;

        private void Start()
        {
            // Controls initialization
            m_inputActions.PauseMenu.ClosePauseMenu.performed += ctx => Close();

            // UI initialization
            m_closePauseMenuButton.onClick.AddListener(() => Close());
            m_exitGameButton.onClick.AddListener(() => ExitGame());
        }

        public override void Open()
        {
            base.Open();
            m_inputActions.PauseMenu.Enable();
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