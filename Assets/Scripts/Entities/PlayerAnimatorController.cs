using UnityEngine;
using PC.Input;

namespace PC.Entities
{
    /// <summary>
    /// This class is responsible for controlling the player's animations.
    /// For our purposes, it simply regularly updates the variables used by the animator to determine which animation to play.
    /// </summary>
    public class PlayerAnimatorController : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        // Player Weapon States
        private const int UNARMED = 0;
        private const int ASSAULT_RIFLE = 1;
        private const int BEAM_GUN = 2;
        private const int GATLIN_GUN = 3;
        private const int MISSLE_LAUNCHER = 4;

        // Player Movement States
        private const int IDLE = 0;
        private const int WALKING = 1;
        private const int RUNNING = 2;
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        #endregion Private Fields
        [SerializeField] private Animator _animator = null;
        private static InputActions inputActions => InputModule.InputActions;

        #endregion Fields

        //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Start()
        {
            // temporary default state for testing
            _animator.SetInteger("CurrentWeapon", ASSAULT_RIFLE);
            _animator.SetInteger("MovementState", IDLE);
            
        }

        private void Update()
        {
            var input = inputActions.Player.Movement.ReadValue<Vector2>();

            // nonzero input means character is walking
            if(input.magnitude == 0) _animator.SetInteger("MovementState", IDLE);
            else if(input.magnitude > 0) _animator.SetInteger("MovementState", WALKING);
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
