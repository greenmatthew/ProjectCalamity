using UnityEngine;
using UnityEngine.InputSystem;

using PC.Input;
// using PC.UI;
using PC.Audio;

namespace PC.Entities
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(ActionHandler))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private HumanoidSounds audioClips = null;

        // Components
		private InputActions m_inputActions;
		
        private Transform m_head;
        private Transform m_body;
        private CharacterController m_characterController;
		[SerializeField] private Transform m_groundCheck;
        [SerializeField] private LayerMask m_groundMask;


		// Consts
        private static readonly Vector3 m_idleVelocity = new Vector3(0f, -2f, 0f);
		private const float m_walkingSpeed = 3.5f;
        private const float m_sprintingToWalkingRatio = 1.65f;
		private const float m_sprintingSpeed = m_sprintingToWalkingRatio * m_walkingSpeed;
		private const float m_angularDragConst = 0.001f;
		private const float m_groundCheckIdleRadius = 0.1f;
		private const float m_groundCheckWalkingRadius = 0.25f;
		private const float m_groundCheckSprintingRadius = 0.35f;
		private const float m_jumpHeight = 1f;
		
		private const float m_minVerticalAngle = -90f; // Counterclockwise(upward) is negative
		private const float m_maxVerticalAngle = 90f; // Clockwise(downward) is positive

		// Member vars
        private Vector2 m_look => m_inputActions.Player.Look.ReadValue<Vector2>() * PC.Settings.Mouse.SENSITIVTY * Time.deltaTime;
        private float m_xRotation = 0f;
        private Vector3 m_move
        {
            get
            {
                var input = m_inputActions.Player.Movement.ReadValue<Vector2>() * MovementSpeed * Time.deltaTime;
                return transform.forward * input.y + transform.right * input.x;
            }
        }
        
        private float m_lastMovementSpeed = 0f;
		private Vector3 m_velocity = Vector3.zero;

        // Conditions
        private bool m_isGrounded => GroundCheck();
        private bool m_isMoving => m_move != Vector3.zero && m_velocity != m_idleVelocity;

		// Jumping Drag Diagnostic Variables
		private float m_startingJumpAngle = 0f;
		private float m_endingJumpAngle = 0f;

		// Member var flags
		private float MovementSpeed
		{
			get
			{
				float speed;
                if (m_isGrounded)
                {
                    if (m_isWalking.Active)
					    speed = m_walkingSpeed;
                    else if (m_isSprinting.Active)
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

		//private readonly Actions m_actions = new Actions();

		private readonly Action m_isJumping = new Action();
		private readonly Action m_isWalking = new Action();
		private readonly Action m_isSprinting = new Action();
		private readonly Action m_isADSing = new Action();
		private readonly Action m_isShooting = new Action();

		private void Awake()
		{
            m_body = transform;
            if (m_body == null)
                Debug.LogError("PlayerController: Body transform is null!");

            m_head = GetComponentInChildren<Camera>().transform;
            if (m_head == null)
                Debug.LogError("PlayerController: Head transform is null!");

            m_characterController = GetComponent<CharacterController>();
            if (m_characterController == null)
                Debug.LogError("PlayerController: CharacterController not found!");

			m_groundCheck = transform.Find("GroundCheck");
            if (m_groundCheck == null)
                Debug.LogError("PlayerController: Ground check Transform not found!");
		}

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;

            SetupActions();
            SetupInput();
		}

		private void Update()
		{
            //move = transform.forward * input.y + transform.right * input.x;

            Look();
			Move();
		}

        private bool GroundCheck()
        {
            if (m_isWalking.Active)
				return m_velocity.y <= 0.0f && Physics.CheckSphere(m_groundCheck.position, m_groundCheckWalkingRadius, m_groundMask);
            if (m_isSprinting.Active)
                return m_velocity.y <= 0.0f && Physics.CheckSphere(m_groundCheck.position, m_groundCheckSprintingRadius, m_groundMask);
            return m_velocity.y <= 0.0f && Physics.CheckSphere(m_groundCheck.position, m_groundCheckIdleRadius, m_groundMask);
        }

        private void Look()
		{
            m_body.Rotate(Vector3.up, m_look.x);

            m_xRotation -= m_look.y;
            m_xRotation = Mathf.Clamp(m_xRotation, m_minVerticalAngle, m_maxVerticalAngle);
            m_head.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
            // m_head.localRotation = Quaternion.Euler(xAngle, 0, 0);
		}

		private void Move()
		{
            // Handle gravity
            if (m_isGrounded && m_velocity.y < 0.0f)
                // Check if I should use Vector3 instead of float for the y component
				m_velocity.y = m_idleVelocity.y;
			else
				m_velocity += Physics.gravity * Time.deltaTime;

			m_characterController.Move(m_move + m_velocity * Time.deltaTime);
		}

		private void SetupInput()
		{
			m_inputActions = new InputActions();
			m_inputActions.Player.Enable();

			m_inputActions.Player.Sprint.started += delegate { m_isSprinting.Enable(); };
			m_inputActions.Player.Sprint.canceled += delegate { m_isSprinting.Disable(); };

			// m_inputActions.Player.SprintToggle.performed += delegate { m_isSprinting.Toggle(); };

            m_inputActions.Player.Jump.wantsInitialStateCheck = true;

			m_inputActions.Player.Jump.performed += ctx => 
            {
                if (m_isGrounded)
                    m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2 * Physics.gravity.y);
            };

			// // Weapon Handling
			// m_inputActions.Player.ADSHold.performed += delegate { m_isADSing.Enable(); };
			// m_inputActions.Player.ADSHold.canceled+= delegate { m_isADSing.Disable(); };

			// m_inputActions.Player.ADSToggle.performed += delegate { m_isADSing.Toggle(); };

			// m_inputActions.Player.Shoot.performed += delegate { m_isShooting.Enable(); };
			// m_inputActions.Player.Shoot.canceled += delegate { m_isShooting.Disable(); };
		}

		private void SetupActions()
		{
			
		}
    }
}
