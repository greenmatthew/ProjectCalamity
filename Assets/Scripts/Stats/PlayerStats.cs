using UnityEngine;
using PC.Entities;

public class PlayerStats : CharacterStats
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
    public override void Die()
    {
        base.Die();
        // Kill the player
        PlayerManager.instance.KillPlayer();
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
