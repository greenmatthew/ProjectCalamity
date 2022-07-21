using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

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
        // Components
        protected CharacterController m_characterController;
		protected InputActions m_inputActions;
        protected Camera m_camera;
        protected Transform m_body;
        
        protected AudioSource m_feetAudioSource;

        [Header("Body")]
        [SerializeField] protected Transform m_head;
        [SerializeField] protected Transform m_feet;
        [SerializeField] protected Transform m_groundCheck;
        [SerializeField] protected LayerMask m_groundMask;

        // Scriptable Objects containg audio clips
        [Header("Audio")]
        [SerializeField] protected HumanoidSounds m_audioClips = null;

        [Header("UI")]
        [SerializeField] protected MenuesController m_menuesController = null;

		// Consts
        protected const float m_minVerticalAngle = -90f; // Counterclockwise(upward) is negative
		protected const float m_maxVerticalAngle = 90f; // Clockwise(downward) is positive

        protected static readonly Vector3 m_idleVelocity = new Vector3(0f, -2f, 0f);
		protected const float m_walkingSpeed = 3.5f;
        protected const float m_sprintingToWalkingRatio = 1.65f;
		protected const float m_sprintingSpeed = m_sprintingToWalkingRatio * m_walkingSpeed;
		protected const float m_angularDragConst = 0.001f;
		protected const float m_groundCheckIdleRadius = 0.1f;
		protected const float m_groundCheckWalkingRadius = 0.25f;
		protected const float m_groundCheckSprintingRadius = 0.35f;
		protected const float m_jumpHeight = 1f;

		// Member vars
        protected Vector2 m_look;
        protected float m_xRotation = 0f;
        protected Vector3 m_move;
        
        protected float m_lastMovementSpeed = 0f;
        protected Vector3 m_lastGroundedMove = Vector3.zero;
		protected Vector3 m_velocity = Vector3.zero;

        protected bool Performed (InputActionPhase phase) => phase == InputActionPhase.Performed;

        // Conditions
        
        

		// Jumping Drag Diagnostic Variables
		protected float m_startingJumpAngle = 0f;
		protected float m_endingJumpAngle = 0f;

		// Member var flags
		protected float MovementSpeed
		{
			get
			{
				float speed;
                if (m_isGrounded)
                {
                    if (m_inputActions.Player.Sprint.phase == InputActionPhase.Performed)
                        speed = m_sprintingSpeed;
                    else
                        speed = m_walkingSpeed;
                    m_lastMovementSpeed = speed;
                }
                else
                {
                    speed = m_lastMovementSpeed;
                }
				
				return speed;
			}
		}

        // Conditions
        private bool _isGrounded = false;
        protected bool m_isGrounded;
        protected bool m_isMoving;

        private void Awake()
		{
            m_characterController = GetComponent<CharacterController>();
            if (m_characterController == null)
                Debug.LogError("PlayerController: CharacterController not found!");

            if (m_head == null)
                Debug.LogError("PlayerController: Head transform is null!");
            
            m_camera = m_head.GetComponentInChildren<Camera>();
            if (m_camera == null)
                Debug.LogError("PlayerController: Camera is null!");

            m_body = transform;
            if (m_body == null)
                Debug.LogError("PlayerController: Body transform is null!");
            
            if (m_feet == null)
                Debug.LogError("PlayerController: Feet transform is null!");

            if (m_groundCheck == null)
                Debug.LogError("PlayerController: Ground check Transform not found!");
            
            m_feetAudioSource = m_feet.GetComponent<AudioSource>();
            if (m_feetAudioSource == null)
                Debug.LogError("PlayerController: Feet AudioSource is null!");
		}

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;

            m_inputActions = InputModule.instance.InputActions;
            SetupInput();
		}

		private void Update()
		{
            m_look = m_inputActions.Player.Look.ReadValue<Vector2>() * PC.Settings.Mouse.SENSITIVTY * Time.deltaTime;
            m_move = GetMovement();
            m_isMoving = m_move != Vector3.zero && m_velocity != m_idleVelocity;
            m_isGrounded = GroundedCheck();

            Look();
			Move();
		}

        private Vector3 GetMovement()
        {
            var input = m_inputActions.Player.Movement.ReadValue<Vector2>() * MovementSpeed * Time.deltaTime;
            return transform.forward * input.y + transform.right * input.x;
        }

        private bool GroundedCheck()
        {
            // Check if the player's y velocity is at idle velocity
            bool cond = m_velocity.y <= 0f;
            
            if (Performed(m_inputActions.Player.Movement.phase))
            {
                if (Performed(m_inputActions.Player.Sprint.phase))
                {
                    cond = cond && Physics.CheckSphere(m_groundCheck.position, m_groundCheckSprintingRadius, m_groundMask);
                }
                else
                {
                    cond = cond && Physics.CheckSphere(m_groundCheck.position, m_groundCheckWalkingRadius, m_groundMask);
                }
            }
            else
            {
                cond = cond && Physics.CheckSphere(m_groundCheck.position, m_groundCheckIdleRadius, m_groundMask);
            }

            bool prevCond = _isGrounded;
            _isGrounded = cond;

            if (_isGrounded != prevCond)
            {
                if (_isGrounded)
                {
                    m_inputActions.Player.Movement.Enable();
                    m_inputActions.Player.Sprint.Enable();
                    m_inputActions.Player.Jump.Enable();

                    m_feetAudioSource.clip = m_audioClips.landing.GetRandom();
                    m_feetAudioSource.Play();
                }
                else
                {
                    m_inputActions.Player.Movement.Disable();
                    m_inputActions.Player.Sprint.Disable();
                    m_inputActions.Player.Jump.Disable();

                    m_feetAudioSource.clip = m_audioClips.jumping.GetRandom();
                    m_feetAudioSource.Play();
                }
            }

            if (_isGrounded)
            {
                m_lastGroundedMove = m_move;
            }

            return _isGrounded;
        }

        protected abstract void SetupInput();
        protected abstract void Look();
        protected abstract void Move();
    }
}
