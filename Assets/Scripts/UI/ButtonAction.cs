using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.UI
{
    public struct ButtonAction
    {
        public string Label;
        public UnityEngine.Events.UnityAction Action;

        public ButtonAction(string label, UnityEngine.Events.UnityAction action = null)
        {
            Label = label;
            Action = action;
        }
    }
}
