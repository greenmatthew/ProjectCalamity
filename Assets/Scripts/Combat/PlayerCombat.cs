using UnityEngine;
using PC.Stats;

namespace PC.Combat
{
    public class PlayerCombat : CharacterCombat
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

        // shooting enemy with gun, no cooldown
        public void AttackTarget(RaycastHit hit)
        {
            // play animation 

            // find parent object
            Transform hit_obj = hit.transform;
            Debug.Log("Hit Obj: " + hit_obj.name);
            if (hit_obj)
            {
                Transform temp = null;  // getting errors when not using temp var
                while (hit_obj != null)
                {
                    temp = hit_obj;
                    hit_obj = hit_obj.parent;
                }
                hit_obj = temp;

                // damage target
                if (hit_obj.TryGetComponent<CharacterStats>(out CharacterStats cs))
                {
                    cs.TakeDamage();
                }
                else
                {
                    Debug.Log("No CharacterStats component found on target");
                }
            }

            // not using this bc it includes a cooldown
            //Attack(cs);
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
}
