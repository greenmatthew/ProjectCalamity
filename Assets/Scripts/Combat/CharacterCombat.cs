using UnityEngine;
using PC.Stats;

namespace PC.Combat
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterCombat : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        private CharacterStats mystats;
        private float attackCooldown = 0f;
        #endregion Private Fields

        #endregion Fields

        //----------------------------------------------------------------------------------------------------------------------

        #region Methods

        #region Public Methods
        public void Attack(CharacterStats targetStats)
        {
            if (attackCooldown <= 0f)
            {
                targetStats.TakeDamage();
                if (mystats.attackSpeed.GetValue() != 0f)
                {
                    attackCooldown = 1f / mystats.attackSpeed.GetValue();
                }
            }
        }
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Start()
        {
            mystats = GetComponent<CharacterStats>();
        }

        private void Update()
        {
            // implement delay in character attacks
            attackCooldown -= Time.deltaTime;
        }
    
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
