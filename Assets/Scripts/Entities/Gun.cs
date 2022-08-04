using UnityEngine;
using UnityEngine.InputSystem;
using PC.Input;

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
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private float range = 100f;
    private InputActions inputActions; 
    #endregion Private Fields

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
        
    }

    private void Update()
    {
        
    }
    
    #endregion Private Methods
    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        // gain access to user inputs
        this.inputActions = new InputActions();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        // connect gun functionality to shoot input
        this.inputActions.Player.Shoot.performed += Shoot;
        this.inputActions.Enable();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        // disconnect gun functionality from shoot input
        this.inputActions.Player.Shoot.performed -= Shoot;
    }

    /// <summary>
    /// Activates muzzle flash and shoots a raycast in the direction the camera is facing
    /// If target has GetShot method, it is executed here
    /// </summary>
    /// <param name="obj"></param>
    private void Shoot(InputAction.CallbackContext obj)
    {
        // shoot raycast in direction of camera
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, this.range))
        {
            // if hit object has a Target script component, execute GetShot
            if (hit.transform.TryGetComponent<Target>(out Target ts))
            {
                ts.GetShot(ray.direction);
            }
        }

        // activate muzzle flash at gun muzzle

    }

    #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

    #region Enums, Structs, Classes
    #endregion Enums, Structs, Classes
}
