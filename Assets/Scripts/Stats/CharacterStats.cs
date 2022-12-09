using UnityEngine;

namespace PC.Stats
{
    /// <summary>
    /// This class is used to store general stats and damage behavior common to all game characters.
    /// </summary>
    public class CharacterStats : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        // \cond
        #region Public Fields
        public Stat attackSpeed;
        public int _currentHealth { get; set; }
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        [SerializeField] private int _maxHealth;
        [SerializeField] private Stat _damage;
        #endregion Private Fields

        #endregion Fields
        // \endcond

        //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods
        /// <summary>
        /// The general damage function. This is called by other scripts any time the relevant entity needs to lose health points.
        /// </summary>
        public void TakeDamage()
        {
            _currentHealth -= _damage.GetValue();
            Debug.Log(transform.name + " takes " + _damage.GetValue() + " damage.");
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Logs an entities death to the console. 
        /// Specific death behavior and animations will be defined in scripts specific to a character.
        /// </summary>
        public virtual void Die()
        {
            // Die in some way
            // This method is meant to be overwritten
            Debug.Log(transform.name + " died.");
        }
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods
        // \cond
        private void Awake()
        {
            _currentHealth = _maxHealth;
        }
        // \endcond

        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
