using UnityEngine;
using PC.Stats;

/*!
 * \file index.html
 * \brief This page introduces a new horror video game.
 *
 * The game is set in a dark and eerie abandoned hospital, where the player must
 * navigate through twisted corridors and haunted rooms to uncover the secrets of
 * the hospital and survive the night. With immersive graphics and intense gameplay,
 * this game is sure to keep players on the edge of their seats.
 *
 * \warning This game is not suitable for children or those who are easily scared.
 */


namespace PC.Combat
{
    /// <summary>
    /// Defines the combat behavior specific to our enemies.
    /// </summary>
    public class EnemyCombat : CharacterCombat
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        /// <summary>
        /// Records whether or not the enemy and player colliders overlap.
        /// </summary>
        public bool _touchingPlayer = false;
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
        /// Activates the enemy's attack when the player is touched.
        /// </summary>
        /// <param name="target"></param>
        public void AttackTarget(Transform target)
        {
            // damage target
            if (_touchingPlayer && target.TryGetComponent<CharacterStats>(out CharacterStats cs))
            {
                _touchingPlayer = false;
                Attack(cs);
            }
        }

        // \cond
        public void Start()
        {
            mystats = GetComponent<CharacterStats>();

            // place touch sensors on all child colliders
            // allows damage to be dealt to player
            SetTags(this.transform);
        }
        
        public void Update()
        {
            // check if dead
            if (mystats._currentHealth <= 0)
            {
                // play death animation
                if (this.TryGetComponent<EnemyAnimationController>(out EnemyAnimationController eac))
                {
                    eac.Die();
                }
                else
                {
                    Debug.Log("EnemyCombat: EnemyAnimationController not found");
                }

            }
        }
        // \endcond

        /// <summary>
        /// Sets the tag of all child objects to "Enemy" so that collisions with appendages can be identified.
        /// Calls this recursively on all children.
        /// </summary>
        /// <param name="curr"> The transform of the object currently being expected. </param>
        public void SetTags(Transform curr)
        {
            foreach (Transform child in curr)
            {
                // set sensor
                //child.gameObject.AddComponent<TouchSensor>();
                child.gameObject.tag = "Enemy";

                // dfs
                SetTags(child);
            }
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
