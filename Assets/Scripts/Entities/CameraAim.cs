using UnityEngine;

public class CameraAim : MonoBehaviour
{
    #region Fields

    #region Consts Fields
    #endregion Consts Fields

    #region Public Fields
    #endregion Public Fields

    #region Protected Fields
    #endregion Protected Fields

    #region Private Fields
    [SerializeField] private Camera _camera = null;
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
        //Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            // move transform to hit point
            transform.position = hit.point;
        }
    }
    
    #endregion Private Methods

    #endregion Methods

//----------------------------------------------------------------------------------------------------------------------

    #region Enums, Structs, Classes
    #endregion Enums, Structs, Classes
}
