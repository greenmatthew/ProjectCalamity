using UnityEngine;

namespace PC.Entities
{
    public class CameraChange : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        #endregion Private Fields
        [SerializeField] private GameObject _mainCam = null;
        [SerializeField] private GameObject _rifleCam = null;
        [SerializeField] private GameObject _reticle = null;


        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Awake()
        {
            // make sure cameras exist
            if(!_mainCam || !_rifleCam)
            {
                Debug.LogError("Player cameras not assigned. See Camera Monitor."); 
            }
            else
            {
                // only use main camera for now
                // rifle cam will be used for ADS view and needs to be swappable for diff weapons
                _rifleCam.SetActive(false);
                _mainCam.SetActive(true);
            }
        }

        private void Update()
        {
            // turn off reticle when main cam is active
            if (_mainCam.activeSelf)
            {
                _reticle.SetActive(false);
            }
            else
            {
                _reticle.SetActive(true);
            }
        }

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
