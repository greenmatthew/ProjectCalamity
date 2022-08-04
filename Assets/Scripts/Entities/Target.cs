using UnityEngine;

public class Target : MonoBehaviour
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

    #endregion Fields

//----------------------------------------------------------------------------------------------------------------------

    #region Methods
    
    #region Public Methods
    /// <summary>
    /// Defines how the target reacts to getting shot by projectile
    /// </summary>
    /// <param name="direction">
    /// the direction the impacting projectile is traveling
    /// </param>
    public void GetShot(Vector3 direction)
    {
        // calculate force 
        Vector3 impactForce = direction * 5 + Vector3.up * 10;

        // add force to target's rigid body
        GetComponent<Rigidbody>().AddForce(impactForce, ForceMode.Impulse);

    }
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

    #endregion Methods

//----------------------------------------------------------------------------------------------------------------------

    #region Enums, Structs, Classes
    #endregion Enums, Structs, Classes
}
