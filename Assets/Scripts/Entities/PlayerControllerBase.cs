using UnityEngine;
using UnityEngine.InputSystem;

using PC.Input;
using PC.UI;
using PC.Audio;
using PC.Extensions;

namespace PC.Entities
{
    [RequireComponent(typeof(InputModule))]
    [RequireComponent(typeof(CharacterController))]
    public abstract class PlayerControllerBase : MonoBehaviour
    {
        #region Fields

        #region Consts Fields

        protected const float _minVerticalAngle = -90f; // Counterclockwise(upward) is negative
        protected const float _maxVerticalAngle = 90f; // Clockwise(downward) is positive

        protected static readonly Vector3 _idleVelocity = new Vector3(0f, -2f, 0f);
        protected const float _walkingSpeed = 3.5f;
        protected const float _sprintingToWalkingRatio = 1.65f;
        protected const float _sprintingSpeed = _sprintingToWalkingRatio * _walkingSpeed;
        protected const float _angularDragConst = 0.001f;
        protected const float _groundCheckIdleRadius = 0.1f;
        protected const float _groundCheckWalkingRadius = 0.25f;
        protected const float _groundCheckSprintingRadius = 0.35f;
        protected const float _jumpHeight = 1f;

        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields

        protected CharacterController _characterController;
        protected InputActions _inputActions => InputModule.InputActions;
        protected Camera _camera;
        protected Transform _body;
        protected AudioSource _feetAudioSource;

        [Header("Body")]
        [SerializeField] protected Transform _head;
        [SerializeField] protected Transform _feet;
        [SerializeField] protected Transform _groundCheck;
        [SerializeField] protected LayerMask _groundMask;

        // Scriptable Objects containg audio clips
        [Header("Audio")]
        [SerializeField] protected HumanoidSounds _audioClips = null;

        [Header("UI")]
        [SerializeField] protected MenusController _menuesController = null;

        protected Vector2 _look;
        protected float _xRotation = 0f;
        protected Vector3 _move;
        
        protected float _lastMovementSpeed = 0f;
        protected Vector3 _lastGroundedMove = Vector3.zero;
		protected Vector3 _velocity = Vector3.zero;

        protected float MovementSpeed
		{
			get
			{
				float speed;
                if (_isGrounded)
                {
                    if (_inputActions.Player.Sprint.phase == InputActionPhase.Performed)
                        speed = _sprintingSpeed;
                    else
                        speed = _walkingSpeed;
                    _lastMovementSpeed = speed;
                }
                else
                {
                    speed = _lastMovementSpeed;
                }
				
				return speed;
			}
		}

        // Jumping Drag Diagnostic Variables
		protected float _startingJumpAngle = 0f;
		protected float _endingJumpAngle = 0f;

        protected bool _isGrounded;
        protected bool _isMoving;

        #endregion Protected Fields

        #region Private Fields

        private bool h_isGrounded = false;

        #endregion Private Fields

        #endregion Fields

//----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods
        #endregion public Methods

        #region Protected Methods

        protected bool Performed (InputActionPhase phase) => phase == InputActionPhase.Performed;

        protected abstract void SetupInput();
        protected abstract void Look();
        protected abstract void Move();

        #endregion Protected Methods

        #region Private Methods

        private void Awake()
		{
            _characterController = GetComponent<CharacterController>();
            if (_characterController == null)
                Debug.LogError("PlayerController: CharacterController not found!");

            if (_head == null)
                Debug.LogError("PlayerController: Head transform is null!");
            
            _camera = _head.GetComponentInChildren<Camera>();
            if (_camera == null)
                Debug.LogError("PlayerController: Camera is null!");

            _body = transform;
            if (_body == null)
                Debug.LogError("PlayerController: Body transform is null!");
            
            if (_feet == null)
                Debug.LogError("PlayerController: Feet transform is null!");

            if (_groundCheck == null)
                Debug.LogError("PlayerController: Ground check Transform not found!");
            
            _feetAudioSource = _feet.GetComponent<AudioSource>();
            if (_feetAudioSource == null)
                Debug.LogError("PlayerController: Feet AudioSource is null!");
		}

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;

            SetupInput();
		}

		private void Update()
		{
            _look = _inputActions.Player.Look.ReadValue<Vector2>() * PC.Settings.Mouse.Sensitivity * Time.deltaTime;
            _move = GetMovement();
            _isMoving = _move != Vector3.zero && _velocity != _idleVelocity;
            _isGrounded = GroundedCheck();

            Look();
			Move();
		}

        private Vector3 GetMovement()
        {
            var input = _inputActions.Player.Movement.ReadValue<Vector2>() * MovementSpeed * Time.deltaTime;
            return transform.forward * input.y + transform.right * input.x;
        }

        private bool GroundedCheck()
        {
            // Check if the player's y velocity is at idle velocity
            bool cond = _velocity.y <= 0f;
            
            if (Performed(_inputActions.Player.Movement.phase))
            {
                if (Performed(_inputActions.Player.Sprint.phase))
                {
                    cond = cond && Physics.CheckSphere(_groundCheck.position, _groundCheckSprintingRadius, _groundMask);
                }
                else
                {
                    cond = cond && Physics.CheckSphere(_groundCheck.position, _groundCheckWalkingRadius, _groundMask);
                }
            }
            else
            {
                cond = cond && Physics.CheckSphere(_groundCheck.position, _groundCheckIdleRadius, _groundMask);
            }

            bool prevCond = h_isGrounded;
            h_isGrounded = cond;

            if (h_isGrounded != prevCond)
            {
                if (h_isGrounded)
                {
                    _inputActions.Player.Movement.Enable();
                    _inputActions.Player.Sprint.Enable();
                    _inputActions.Player.Jump.Enable();

                    _feetAudioSource.clip = _audioClips.landing.GetRandom();
                    _feetAudioSource.Play();
                }
                else
                {
                    _inputActions.Player.Movement.Disable();
                    _inputActions.Player.Sprint.Disable();
                    _inputActions.Player.Jump.Disable();

                    _feetAudioSource.clip = _audioClips.jumping.GetRandom();
                    _feetAudioSource.Play();
                }
            }

            if (h_isGrounded)
            {
                _lastGroundedMove = _move;
            }

            return h_isGrounded;
        }
    
        #endregion Private Methods

        #endregion Methods

//----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
