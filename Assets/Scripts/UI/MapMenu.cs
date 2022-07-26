using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.UI
{
    public class MapMenu : MenuBase
    {
        protected override void AwakeExtension()
        {
            _inputActions.MapMenu.CloseMenu.performed += ctx => Close();
        }

        protected override void OpenExtension()
        {
            _inputActions.MapMenu.Enable();
        }

        protected override void CloseExtension()
        {
        }
    }
}
