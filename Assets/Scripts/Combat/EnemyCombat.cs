using UnityEngine;

namespace PC.Combat
{
    public class EnemyCombat : CharacterCombat
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
        public void AttackTarget(Transform target)
        {
            // play animation 

            // damage target
            if (target.TryGetComponent<CharacterStats>(out CharacterStats cs))
            {
                Attack(cs);
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
