using UnityEngine;

using PC.Extensions;

namespace PC.Entities
{
    public class PlayerController : PlayerControllerBase
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods
        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Turns the Player object along x mouse input and turns camera along y mouse input.
        /// </summary>
        protected override void Look()
		{
            _body.Rotate(Vector3.up, _look.x);

            _xRotation -= _look.y;
            _xRotation = Mathf.Clamp(_xRotation, _minVerticalAngle, _maxVerticalAngle);
            _head.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
		}

        /// <summary>
        /// Moves the Player object by input from user and also applies gravity.
        /// </summary>
		protected override void Move()
		{
            // Handle gravity
            if (_isGrounded && _velocity.y < 0.0f)
                // Check if I should use Vector3 instead of float for the y component
				_velocity.y = _idleVelocity.y;
			else
				_velocity += Physics.gravity * Time.deltaTime;

            if (_isGrounded)
            {
                _characterController.Move(_move + _velocity * Time.deltaTime);

                if (_move != Vector3.zero && !_feetAudioSource.isPlaying)
                {
                    if (Performed(_inputActions.Player.Sprint.phase))
                        _feetAudioSource.clip = _audioClips.sprinting.GetRandom();
                    else
                        _feetAudioSource.clip = _audioClips.walking.GetRandom();
                    
                    _feetAudioSource.Play();
                }
            }
            else
            {
                _characterController.Move(_lastGroundedMove + _velocity * Time.deltaTime);
            }
		}

        /// <summary>
        /// Initializes the InputActions object, which handles all input from the user.
        /// </summary>
		protected override void SetupInput()
		{
			_inputActions.Player.Enable();

			_inputActions.Player.Jump.performed += ctx =>
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * Physics.gravity.y);
            };
            _inputActions.Player.OpenPauseMenu.performed += ctx => { Debug.Log("Menu"); _menuesController.PauseMenu.Open(); };
            _inputActions.Player.OpenDevConsoleMenu.performed += ctx => { Debug.Log("Developer Console"); _menuesController.DevConsoleMenu.Open(); };
            _inputActions.Player.OpenInventoryMenu.performed += ctx => { Debug.Log("Inventory"); _menuesController.InventoryMenu.Open(); };
            _inputActions.Player.OpenMapMenu.performed += ctx => { Debug.Log("Map"); _menuesController.MapMenu.Open(); };
		}

        #endregion Protected Methods

        #region Private Methods
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes, Interfaces
        #endregion Enums, Structs, Classes, Interfaces
        
    }
}
