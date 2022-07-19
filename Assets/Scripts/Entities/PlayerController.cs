using UnityEngine;

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
		private const float m_walkingSpeed = 3.5f;
        private const float m_sprintingToWalkingRatio = 1.65f;
		private const float m_sprintingSpeed = m_sprintingToWalkingRatio * m_walkingSpeed;
		private const float m_angularDragConst = 0.001f;
		private const float m_groundCheckIdleRadius = 0.1f;
		private const float m_groundCheckWalkingRadius = 0.25f;
		private const float m_groundCheckSprintingRadius = 0.35f;
		private const float m_jumpHeight = 1.0f;
		
		private const float m_minVerticalAngle = -90.0f; // Counterclockwise(upward) is negative
		private const float m_maxVerticalAngle = 38.8f; // Clockwise(downward) is positive

		// Member vars
        [SerializeField] private Vector2 m_look
        {
            get
            {
                var input = m_inputActions.Player.Look.ReadValue<Vector2>() * PC.Settings.Mouse.SENSITIVTY * Time.deltaTime;
                return input;
            }
        }
        [SerializeField] private Vector3 _move = Vector3.zero;
        [SerializeField] private Vector3 m_move
        {
            get
            {
                var input = m_inputActions.Player.Movement.ReadValue<Vector2>() * MovementSpeed * Time.deltaTime;
                _move = transform.forward * input.y + transform.right * input.x;
                return _move;
            }
        }
		[SerializeField] private Vector3 m_velocity = Vector3.zero;
		private float m_lastMovementSpeed = 0.0f;
		

		// Jumping Drag Diagnostic Variables
		private float m_startingJumpAngle = 0.0f;
		private float m_endingJumpAngle = 0.0f;

		// Member var flags
		private float MovementSpeed
		{
			get
			{
				float speed;
                if (m_isGrounded.Active)
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

		private readonly Action m_isGrounded = new Action();
		private readonly Action m_isMoving = new Action();
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
            Look();
			Move();
		}

		private void Move()
		{
            if (m_isGrounded.Active && m_velocity.y < 0.0f)
				m_velocity.y = -2.0f;
			else
				m_velocity += Physics.gravity * Time.deltaTime;

			m_characterController.Move(m_move + m_velocity * Time.deltaTime);
		}

		private void Look()
		{
            m_body.Rotate(Vector3.up, m_look.x);
            m_head.Rotate(Vector3.left, m_look.y);
            // var xAngle = Mathf.Clamp(m_head.rotation.x, m_minVerticalAngle, m_maxVerticalAngle);
            // m_head.localRotation = Quaternion.Euler(new Vector3(xAngle, m_head.rotation.y, m_head.rotation.z));
		}

		private void SetupInput()
		{
			m_inputActions = new InputActions();
			m_inputActions.Player.Enable();

			// m_inputActions.Player.SprintHold.started += delegate { m_isSprinting.Enable(); };
			// m_inputActions.Player.SprintHold.canceled += delegate { m_isSprinting.Disable(); };

			// m_inputActions.Player.SprintToggle.performed += delegate { m_isSprinting.Toggle(); };

			// m_inputActions.Player.Jump.started += delegate { m_isJumping.Enable(); };

			// // Weapon Handling
			// m_inputActions.Player.ADSHold.performed += delegate { m_isADSing.Enable(); };
			// m_inputActions.Player.ADSHold.canceled+= delegate { m_isADSing.Disable(); };

			// m_inputActions.Player.ADSToggle.performed += delegate { m_isADSing.Toggle(); };

			// m_inputActions.Player.Shoot.performed += delegate { m_isShooting.Enable(); };
			// m_inputActions.Player.Shoot.canceled += delegate { m_isShooting.Disable(); };
		}

		private void SetupActions()
		{
			m_isGrounded.OnStart = delegate
			{
                Debug.Log("PlayerController: Grounded");
				// TODO: Set audio source to random audio clip
                // TODO: Play autio source
				m_isJumping.Disable();
			};
			m_isGrounded.OnCancel = delegate
			{
                Debug.Log("PlayerController: Not Grounded");
				m_isWalking.Disable();
				m_isSprinting.Disable();
				m_isADSing.Disable();
				m_isShooting.Disable();
			};
			m_isGrounded.RuntimeConditionCheck = delegate
			{
				// if (m_isWalking.Active)
				// 	return m_velocity.y <= 0.0f && Physics.CheckSphere(m_groundCheck.position, m_groundCheckWalkingRadius, m_groundMask);
				// if (m_isSprinting.Active)
				// 	return m_velocity.y <= 0.0f && Physics.CheckSphere(m_groundCheck.position, m_groundCheckSprintingRadius, m_groundMask);
				// return m_velocity.y <= 0.0f && Physics.CheckSphere(m_groundCheck.position, m_groundCheckIdleRadius, m_groundMask);

                return Physics.CheckSphere(m_groundCheck.position, 1f, m_groundMask);
			};
			m_isGrounded.OnDelta = delegate
			{
				//m_animator.SetBool("isGrounded", m_isGrounded.Active);
			};

			m_isMoving.RuntimeConditionCheck = delegate
			{
				return (m_velocity != new Vector3(0.0f, -2.0f, 0.0f) || m_move != Vector3.zero);
			};
			m_isMoving.OnDelta = delegate
			{
				//m_animator.SetBool("isMoving", m_isMoving.Active);
			};

			m_isJumping.OnStart = delegate
			{
				print("JUMPING");

				//print($"Starting angular velocity: {m_prevAngularVelocityX}");
				//stopwatch.Reset();
				//stopwatch.Start();
				//m_startingJumpAngle = m_endingJumpAngle = transform.rotation.eulerAngles.y;

				// TODO: Set audio source to random audio clip
                // TODO: Play audio source
				m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2 * Physics.gravity.y);
				m_isWalking.Disable();
				m_isSprinting.Disable();
				m_isGrounded.Disable();
			};
			m_isJumping.OnCancel = delegate
			{
				//stopwatch.Stop();
				//print($"Jumping took: {stopwatch.ElapsedMilliseconds * 0.001} seconds");
				//print($"Delta Theta: {m_endingJumpAngle - m_startingJumpAngle}");
			};
			m_isJumping.OnDelta = delegate
			{
				//m_animator.SetBool("isJumping", m_isJumping.Active);
			};

			m_isWalking.OnStart = delegate
			{
				//m_animator.SetBool("isWalking", m_isWalking.Active);
			};
			m_isWalking.RuntimeConditionCheck = delegate
			{
				return (m_isMoving.Active && m_isGrounded.Active && !m_isSprinting.Active);
			};
			m_isWalking.OnDelta = delegate
			{
				//m_animator.SetBool("isWalking", m_isWalking.Active);
			};
			m_isWalking.OnRuntimeActive = delegate
			{
				// if (!m_footstepsAudioSource.isPlaying)
				// {
				// 	m_footstepsAudioSource.clip = Utility.GetRandomItem(m_walkingFootsteps);
				// 	m_footstepsAudioSource.Play();
				// }
			};

			m_isSprinting.OnStart = delegate
			{
				m_isWalking.Disable();
				m_isADSing.Disable();
				m_isShooting.Disable();
			};
			m_isSprinting.OnDelta = delegate
			{
				// m_animator.SetBool("isSprinting", m_isSprinting.Active);
			};
			m_isSprinting.OnRuntimeActive = delegate
			{
				// if (!m_footstepsAudioSource.isPlaying)
				// {
				// 	m_footstepsAudioSource.clip = Utility.GetRandomItem(m_sprintingFootsteps);
				// 	m_footstepsAudioSource.Play();
				// }
			};


			m_isADSing.OnDelta = delegate
			{
				// m_animator.SetBool("isADSing", m_isADSing.Active);
			};

			m_isShooting.OnStart = delegate
			{
                // if (firemode == Firemode.SEMI_AUTOMATIC)
                // {
                //     TryShooting();
                //     m_isShooting.Disable();
                // }
                // else if (firemode == Firemode.FULLY_AUTOMATIC)
                // {
                //     TryShooting();
                // }
			};
			m_isShooting.OnCancel = delegate
			{
				// m_weaponMuzzleFlash.Stop();
			};
			m_isShooting.OnDelta = delegate
			{
				// m_animator.SetBool("isShooting", m_isShooting.Active);
			};
			m_isShooting.OnRuntimeActive = delegate
			{
				// TryShooting();
			};
		}
    }
}
