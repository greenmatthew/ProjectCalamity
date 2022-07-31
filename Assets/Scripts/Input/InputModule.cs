using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace PC.Input
{
    [RequireComponent(typeof(EventSystem), typeof(InputSystemUIInputModule))]
    public class InputModule : BaseInputModule
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields

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

        public static InputActions InputActions { get; private set; }

        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private static bool _awakeCalledFlag = false;
        private static InputModule _instance = null;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods

        public override void Process()
        {
        }

        #endregion Public Methods

        #region Protected Methods

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

        #endregion Protected Methods

        #region Private Methods
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}