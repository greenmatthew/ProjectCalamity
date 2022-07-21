using UnityEngine;

using PC.Extensions;

namespace PC.Entities
{
    public class PlayerController : PlayerControllerBase
    {
        /// <summary>
        /// Turns the Player object along x mouse input and turns camera along y mouse input.
        /// </summary>
        protected override void Look()
		{
            m_body.Rotate(Vector3.up, m_look.x);

            m_xRotation -= m_look.y;
            m_xRotation = Mathf.Clamp(m_xRotation, m_minVerticalAngle, m_maxVerticalAngle);
            m_head.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
		}

        /// <summary>
        /// Moves the Player object by input from user and also applies gravity.
        /// </summary>
		protected override void Move()
		{
            // Handle gravity
            if (m_isGrounded && m_velocity.y < 0.0f)
                // Check if I should use Vector3 instead of float for the y component
				m_velocity.y = m_idleVelocity.y;
			else
				m_velocity += Physics.gravity * Time.deltaTime;

            if (m_isGrounded)
            {
                m_characterController.Move(m_move + m_velocity * Time.deltaTime);

                if (m_move != Vector3.zero && !m_feetAudioSource.isPlaying)
                {
                    if (Performed(m_inputActions.Player.Sprint.phase))
                        m_feetAudioSource.clip = m_audioClips.sprinting.GetRandom();
                    else
                        m_feetAudioSource.clip = m_audioClips.walking.GetRandom();
                    
                    m_feetAudioSource.Play();
                }
            }
            else
            {
                m_characterController.Move(m_lastGroundedMove + m_velocity * Time.deltaTime);
            }
		}

        /// <summary>
        /// Initializes the InputActions object, which handles all input from the user.
        /// </summary>
		protected override void SetupInput()
		{
			m_inputActions.Player.Enable();

			m_inputActions.Player.Jump.performed += ctx =>
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2 * Physics.gravity.y);
            };
            m_inputActions.Player.OpenPauseMenu.performed += ctx => { Debug.Log("Menu"); m_menuesController.PauseMenu.Open(); };
            m_inputActions.Player.OpenDevConsoleMenu.performed += ctx => { Debug.Log("Developer Console"); m_menuesController.DevConsoleMenu.Open(); };
            m_inputActions.Player.OpenInventoryMenu.performed += ctx => { Debug.Log("Inventory"); m_menuesController.InventoryMenu.Open(); };
		}
    }
}
