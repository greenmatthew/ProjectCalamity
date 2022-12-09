using UnityEngine;

namespace PC.Stats
{
    /// <summary>
    /// This class is used to store the enemy's stats and log its death.
    /// </summary>
    public class EnemyStats : CharacterStats
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
