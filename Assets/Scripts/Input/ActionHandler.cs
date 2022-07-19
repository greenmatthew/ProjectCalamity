using System.Collections.Generic;
using UnityEngine;

namespace PC.Input
{
    public class ActionHandler : MonoBehaviour
    {
        private void Awake()
        {
            ActionHandlerStatic.SetInstance(this);
        }

        private void Update()
        {
            ActionHandlerStatic.RuntimeConditionChecks(this);
        }

        public static void AddAction(Action action)
        {
            ActionHandlerStatic.AddAction(action);
        }
    }
}