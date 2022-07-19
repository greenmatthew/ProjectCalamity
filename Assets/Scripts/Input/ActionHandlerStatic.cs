using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.Input
{
    public static class ActionHandlerStatic
    {
        private static ActionHandler m_instance = null;
        private static readonly List<Action> m_actions = new List<Action>();

        public static void SetInstance(ActionHandler instance)
        {
            if (m_instance == null)
            {
                m_instance = instance;
            }
            else
            {
                Debug.LogError("ActionHandlerStatic.SetInstance: There MUST only be a single ActionHandlers in the scene. Deleting extra instance.");
                GameObject.Destroy(instance);
            }
        }

        public static void AddAction(Action action)
        {
            m_actions.Add(action);
        }

        public static void RuntimeConditionChecks(ActionHandler actionHandler)
        {
            if (m_instance == null)
            {
                Debug.LogError("ActionHandlerStatic.RuntimeConditionChecks: ActionHandler singleton instance not set.");
            }
            else if (m_instance == actionHandler)
            {
                foreach (var action in m_actions)
                {
                    action?.PerformRuntimeConditionChecks();
                }
            }
            else
            {
                Debug.LogError("ActionHandlerStatic.RuntimeConditionChecks: ActionHandler singleton instance is not the caller of this function. There MUST only be a single ActionHandlers in the scene.");
            }
        }
    }
}
