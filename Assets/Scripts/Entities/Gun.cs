using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

using PC.Input;
using PC.Audio;
using PC.UI;
using PC.VFX;

using Random = UnityEngine.Random;

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
        [SerializeField] private GameObject _muzzleFlashLight = null;
        private ParticleSystem _muzzleFlashParticles = null;

        [SerializeField] private Transform _recoil = null;
        [SerializeField] private Camera _camera = null;
        [SerializeField] private GunSO _gun = null;

        [SerializeField] private ScifiRifleSounds _audioClips = null;
        private AudioSource _gunAudioSource = null;

        private bool _isShooting = false;
        private Vector3 _currentRotation = Vector3.zero;
        private Vector3 _targetRotation = Vector3.zero;


        [SerializeField] private float _snappiness = 6f;
        [SerializeField] private float _returnSpeed = 2f;

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

        private void Start()
        {
            inputActions.Player.Shoot.started += (ctx) => _isShooting = true;
            inputActions.Player.Shoot.performed += Shoot;
            inputActions.Player.Shoot.canceled += (ctx) => _isShooting = false;

            inputActions.Player.Shoot.Enable();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnEnable()
        {
            inputActions.Player.Shoot.Enable();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnDisable()
        {
            // disconnect gun functionality from shoot input
            inputActions.Player.Shoot.Disable();
        }

        private void Update()
        {
            _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.fixedDeltaTime);
            _recoil.localRotation = Quaternion.Euler(_currentRotation);
        }

        /// <summary>
        /// Activates muzzle flash and shoots a raycast in the direction the camera is facing
        /// If target has GetShot method, it is executed here
        /// </summary>
        /// <param name="obj"></param>
        private async void Shoot(InputAction.CallbackContext obj)
        {
            while (_isShooting)
            {
                // play gun sound
                _gunAudioSource.clip = _audioClips.shoot;
                _gunAudioSource.Play();

                // play muzzle flash
                _muzzleFlashParticles = _muzzleFlash.GetComponent<ParticleSystem>();
                _muzzleFlashParticles.Play();

                // flicker muzzle flash background light
                if (_muzzleFlashLight != null)
                {
                    if (_muzzleFlashLight.transform.TryGetComponent<WFX_LightFlicker>(out WFX_LightFlicker flicker))
                        flicker.StartCoroutine("Flicker");
                    else
                        Debug.LogError("Muzzle flash: Flicker script is missing!");
                }
                else
                    Debug.LogError("Muzzle flash: background light is null!");


                // shoot raycast in direction of camera
                Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, _gun.Range))
                {
                    // if hit object has a Target script component, execute GetShot
                    if (hit.transform.TryGetComponent<Target>(out Target ts))
                    {
                        ts.GetShot(ray.direction);
                    }
                }

                // Apply recoil
                _targetRotation += new Vector3(-_gun.VerticalRecoil, Random.Range(-_gun.HorizontalRecoil, _gun.HorizontalRecoil), 0f);

                Debug.Log(_gun.FireRate);
                await Task.Delay(TimeSpan.FromSeconds(_gun.FireRate));
            }
        }

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }    
}