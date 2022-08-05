using UnityEngine;
using UnityEngine.InputSystem;

using PC.Input;
using PC.Audio;
using PC.UI;

namespace PC.Entities
{
    public class Gun : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        [SerializeField] private GameObject _muzzleFlash = null;
        [SerializeField] private Camera _camera = null;
        [SerializeField] private float _range = 100f;

        [SerializeField] private ScifiRifleSounds _audioClips = null;
        private AudioSource _gunAudioSource = null;

        private static InputActions inputActions => InputModule.InputActions;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
        
        #region Public Methods
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            // audio source component
            _gunAudioSource = GetComponent<AudioSource>();
            if (_gunAudioSource == null)
                Debug.LogError("Gun: AudioSource is null!");
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnEnable()
        {
            inputActions.Player.Shoot.performed += Shoot;
            inputActions.Player.Enable();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnDisable()
        {
            // disconnect gun functionality from shoot input
            inputActions.Player.Shoot.performed -= Shoot;
        }

        /// <summary>
        /// Activates muzzle flash and shoots a raycast in the direction the camera is facing
        /// If target has GetShot method, it is executed here
        /// </summary>
        /// <param name="obj"></param>
        private void Shoot(InputAction.CallbackContext obj)
        {
            // play gun sound
            _gunAudioSource.clip = _audioClips.shoot;
            _gunAudioSource.Play();

            // activate muzzle flash 


            // shoot raycast in direction of camera
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _range))
            {
                // if hit object has a Target script component, execute GetShot
                if (hit.transform.TryGetComponent<Target>(out Target ts))
                {
                    ts.GetShot(ray.direction);
                }
            }
        }
        
        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }    
}