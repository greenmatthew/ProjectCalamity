using UnityEngine;
using UnityEngine.SceneManagement;

namespace PC.Stats
{
    /// <summary>
    /// This class is used to store the player's stats and log its death to the console.
    /// </summary>
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
        /// <summary>
        /// Logs the enemy's death and can specify other death behavior not related to 
        /// animations which are handled in the animation controller.
        /// </summary>
        public override void Die()
        {
            base.Die();

        }
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
