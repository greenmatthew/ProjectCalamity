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
		private CharacterController m_characterController;
		[SerializeField] private Transform m_groundCheck;

		// Consts
		private const float m_walkingSpeed = 3.5f;
		private const float m_sprintingSpeed = 1.65f * m_walkingSpeed;
		private const float m_accelerationOfGravity = -9.81f;
		private const float m_angularDragConst = 0.001f;
		private const float m_groundCheckIdleRadius = 0.1f;
		private const float m_groundCheckWalkingRadius = 0.25f;
		private const float m_groundCheckSprintingRadius = 0.35f;
		private const float m_jumpHeight = 1.0f;
		private const float m_mouseSensitivity = 15.0f; // 38.8
		private const float m_minVerticalAngle = -90.0f; // Counterclockwise(upward) is negative
		private const float m_maxVerticalAngle = 38.8f; // Clockwise(downward) is positive
		private const float m_maxVerticalFreelookDeltaAngle = 45.0f;
		private const float m_maxHorizontalFreelookDeltaAngle = 65.0f;

		// Member vars
		[SerializeField] private Vector2 m_movementInput;
		[SerializeField] private Vector3 m_move;
		[SerializeField] private Vector3 m_velocity;
		private float m_lastMovementSpeed = 0.0f;
		[SerializeField] private LayerMask m_groundMask;
		[SerializeField] private float m_xRotation = 0.0f;
		[SerializeField] private float m_prevAngularVelocityX = 0.0f;
		[SerializeField] private float m_xFreelookRotation = 0.0f;
		[SerializeField] private float m_yFreelookRotation = 0.0f;

		// Jumping Drag Diagnostic Variables
		private float m_startingJumpAngle = 0.0f;
		private float m_endingJumpAngle = 0.0f;

		// Member var flags
		private float MovementSpeed
		{
			get
			{
				float speed;
				if (m_isWalking.Active)
					speed = m_walkingSpeed;
				else if (m_isSprinting.Active)
					speed = m_sprintingSpeed;
				else
				{
					print("ERROR with getting movement speed!");
					speed = 0.0f;
				}
				m_lastMovementSpeed = speed;
				return speed;
			}
		}

		//private readonly Actions m_actions = new Actions();

		private readonly Action m_isGrounded = new Action();
		private readonly Action m_isMoving = new Action();
		private readonly Action m_isJumping = new Action();
		private readonly Action m_isWalking = new Action();
		private readonly Action m_isSprinting = new Action();
		private readonly Action m_isFreelooking = new Action();
		private readonly Action m_isADSing = new Action();
		private readonly Action m_isShooting = new Action();

		private void Awake()
		{
			m_groundCheck = transform.Find("GroundCheck");

			m_characterController = GetComponent<CharacterController>();
		}

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;

            SetupActions();
            SetupInput();
		}

		private void Update()
		{
			//m_actions.PerformRuntimeChecks();

			HandleGravityAndJumping();
			HandleMovement();
			HandleCamera();
		}

		private void HandleGravityAndJumping()
		{
			if (m_isGrounded.Active && m_velocity.y < 0.0f)
				m_velocity.y = -2.0f;
			else
				m_velocity.y += m_accelerationOfGravity * Time.deltaTime;

			m_characterController.Move(m_velocity * Time.deltaTime);
		}

		private void HandleMovement()
		{
			m_movementInput = m_inputActions.Player.Movement.ReadValue<Vector2>();

			if (m_isGrounded.Active)
			{
				if (m_movementInput != Vector2.zero)
				{
					m_move = transform.right * m_movementInput.x + transform.forward * m_movementInput.y;

					m_characterController.Move(m_move * (MovementSpeed * Time.deltaTime));
				}
			}
			else
			{
				m_characterController.Move(m_move * (m_lastMovementSpeed * Time.deltaTime));
			}
		}

		private void HandleCamera()
		{
			Vector2 input = m_inputActions.Player.Look.ReadValue<Vector2>() * (m_mouseSensitivity * Time.deltaTime);

			if (!m_isFreelooking.Active) // If NOT freelooking
			{
				m_xRotation -= input.y;

				m_xRotation = Mathf.Clamp(m_xRotation, m_minVerticalAngle, m_maxVerticalAngle);
				Quaternion verticalRotation = Quaternion.Euler(m_xRotation, 0.0f, 0.0f);
				transform.localRotation = verticalRotation;

				if (!m_isJumping.Active && m_isGrounded.Active)
				{
					m_prevAngularVelocityX = input.x;
					
					transform.Rotate(Vector3.up * input.x);
					
				}
				else
				{
					//print($"{m_prevAngularVelocityX * Mathf.Exp(-Time.deltaTime / m_angularDragConst)} degrees lossed in {Time.deltaTime} seconds");
					if (m_prevAngularVelocityX > 0)
						m_prevAngularVelocityX = Mathf.Clamp(m_prevAngularVelocityX - (m_prevAngularVelocityX * Mathf.Exp(-Time.deltaTime / m_angularDragConst)), 0.0f, float.MaxValue);
					else if (m_prevAngularVelocityX < 0)
						m_prevAngularVelocityX = Mathf.Clamp(m_prevAngularVelocityX - (m_prevAngularVelocityX * Mathf.Exp(-Time.deltaTime / m_angularDragConst)), float.MinValue, 0.0f);
					m_endingJumpAngle += m_prevAngularVelocityX;
					transform.Rotate(Vector3.up * m_prevAngularVelocityX);
				}
			}
		}

		private void SetupInput()
		{
			m_inputActions = new InputActions();
			m_inputActions.Player.Enable();

			// m_inputActions.Player.SprintHold.started += delegate { m_isSprinting.Enable(); };
			// m_inputActions.Player.SprintHold.canceled += delegate { m_isSprinting.Disable(); };

			// m_inputActions.Player.SprintToggle.performed += delegate { m_isSprinting.Toggle(); };

			// m_inputActions.Player.Jump.started += delegate { m_isJumping.Enable(); };

			// // Look
			// m_inputActions.Player.FreelookHold.performed += delegate { m_isFreelooking.Enable(); };
			// m_inputActions.Player.FreelookHold.canceled += delegate { m_isFreelooking.Disable(); };

			// m_inputActions.Player.FreelookToggle.performed += delegate { m_isFreelooking.Toggle(); };

			// // Weapon Handling
			// m_inputActions.Player.ADSHold.performed += delegate { m_isADSing.Enable(); };
			// m_inputActions.Player.ADSHold.canceled+= delegate { m_isADSing.Disable(); };

			// m_inputActions.Player.ADSToggle.performed += delegate { m_isADSing.Toggle(); };

			// m_inputActions.Player.Shoot.performed += delegate { m_isShooting.Enable(); };
			// m_inputActions.Player.Shoot.canceled += delegate { m_isShooting.Disable(); };
		}

		private void SetupActions()
		{
			//m_actions.AddAction(m_isGrounded);
			m_isGrounded.OnStart = delegate
			{
				// TODO: Set audio source to random audio clip
                // TODO: Play autio source
				m_isJumping.Disable();
			};
			m_isGrounded.OnCancel = delegate
			{
				m_isWalking.Disable();
				m_isSprinting.Disable();
				m_isADSing.Disable();
				m_isShooting.Disable();
			};
			m_isGrounded.RuntimeConditionCheck = delegate
			{
				if (m_isWalking.Active)
					return m_velocity.y <= 0.0f && Physics.CheckSphere(m_groundCheck.position, m_groundCheckWalkingRadius, m_groundMask);
				if (m_isSprinting.Active)
					return m_velocity.y <= 0.0f && Physics.CheckSphere(m_groundCheck.position, m_groundCheckSprintingRadius, m_groundMask);
				return m_velocity.y <= 0.0f && Physics.CheckSphere(m_groundCheck.position, m_groundCheckIdleRadius, m_groundMask);
			};
			m_isGrounded.OnDelta = delegate
			{
				//m_animator.SetBool("isGrounded", m_isGrounded.Active);
			};

			// m_actions.AddAction(m_isMoving);
			m_isMoving.RuntimeConditionCheck = delegate
			{
				return (m_velocity != new Vector3(0.0f, -2.0f, 0.0f) || m_movementInput != Vector2.zero);
			};
			m_isMoving.OnDelta = delegate
			{
				//m_animator.SetBool("isMoving", m_isMoving.Active);
			};

			// m_actions.AddAction(m_isJumping);
			m_isJumping.OnStart = delegate
			{
				print("JUMPING");

				//print($"Starting angular velocity: {m_prevAngularVelocityX}");
				//stopwatch.Reset();
				//stopwatch.Start();
				//m_startingJumpAngle = m_endingJumpAngle = transform.rotation.eulerAngles.y;

				m_move = transform.right * m_movementInput.x + transform.forward * m_movementInput.y;
				// TODO: Set audio source to random audio clip
                // TODO: Play autio source
				m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2 * m_accelerationOfGravity);
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

			// m_actions.AddAction(m_isWalking);
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

			// m_actions.AddAction(m_isSprinting);
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

			// m_actions.AddAction(m_isFreelooking);
			m_isFreelooking.OnStart = delegate
			{
				m_xFreelookRotation = m_xRotation;
				m_yFreelookRotation = 0.0f;
			};
			m_isFreelooking.OnDelta = delegate
			{
				// m_animator.SetBool("isFreelooking", m_isFreelooking.Active);
			};

			// m_actions.AddAction(m_isADSing);
			m_isADSing.OnDelta = delegate
			{
				// m_animator.SetBool("isADSing", m_isADSing.Active);
			};

			// m_actions.AddAction(m_isShooting);
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
