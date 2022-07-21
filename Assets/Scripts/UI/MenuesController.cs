using UnityEngine;

using PC.Input;

namespace PC.UI
{
    public class MenuesController : MonoBehaviour
    {
        [SerializeField] public PauseMenu PauseMenu = null;
        [SerializeField] public InventoryMenu InventoryMenu = null;
        [SerializeField] public DevConsoleMenu DevConsoleMenu = null;

        private void Awake()
        {
            if (PauseMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: PauseMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
            
            if (InventoryMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: InventoryMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");

            if (DevConsoleMenu.gameObject.activeSelf)
                Debug.LogWarning("MenuesController: DevConsoleMenu is enabled on application start. Please disable it in the inspector, otherwise input handling will not work.");
        }
    }
}
