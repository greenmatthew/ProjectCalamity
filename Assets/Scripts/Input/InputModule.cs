using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace PC.Input
{
    [RequireComponent(typeof(EventSystem), typeof(InputSystemUIInputModule))]
    public class InputModule : BaseInputModule
    {
        private static bool _awakeCalledFlag = false;
        private static InputModule _instance = null;
        public static InputModule instance
        {
            get
            {
                if (_instance == null)
                {
                    if (!_awakeCalledFlag)
                    {
                        Debug.LogError("InputModule instance is null. InputModule.Awake() has not been called yet.");
                    }
                    else
                    {
                        Debug.LogError("InputModule instance is null. InputModule.Awake() has been called, but there is no instance.");
                    }
                }
                return _instance;
            }
        }

        public InputActions InputActions = null;

        protected override void Awake()
        {
            base.Awake();

            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError("InputModule: There can only be one InputModule in the scene. Deleting additional InputModule.");
                Destroy(this);
            }

            InputActions = new InputActions();

            _awakeCalledFlag = true;
        }

        public override void Process()
        {
        }
    }
}
