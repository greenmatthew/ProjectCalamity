using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.Input
{
    public class ActionHandler : MonoBehaviour
    {
        // Enforce as a singleton
        private static ActionHandler _instance;

        private readonly List<Action> actions = new List<Action>();
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            foreach (var action in actions)
            {
                action?.PerformRuntimeConditionChecks();
            }
        }

        private void AddAction(Action action)
        {
            actions.Add(action);
        }
    }
}
