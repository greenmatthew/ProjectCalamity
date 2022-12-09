using UnityEngine;
using PC.Stats;

namespace PC.Combat
{
    /// <summary>
    /// Defined the combat behavior specific to the player character.
    /// </summary>
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterCombat : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        // \cond
        protected CharacterStats mystats;
        // \endcond
        #endregion Protected Fields

        #region Private Fields
        //private float attackCooldown = 0f;
        #endregion Private Fields

        #endregion Fields

        //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods
        /// <summary>
        /// Attack the target. This is called when the player's gun gets a hit on a damageable object.
        /// </summary>
        /// <param name="targetStats"> The stats object for the target being hit. This controls the amount of damage it recieves. </param>
        public void Attack(CharacterStats targetStats)
        {
            targetStats.TakeDamage();
        }
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        // \cond
        private void Start()
        {
            mystats = GetComponent<CharacterStats>();
        }

        private void Update()
        {
            // implement delay in character attacks
            //attackCooldown -= Time.deltaTime;
        }
        // \endcond
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
