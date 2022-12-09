using UnityEngine;

namespace PC.Combat
{
    /// <summary>
    /// Records any contact between the player and the enemy. 
    /// This allows the enemy to apply damage to the player on contact.
    /// </summary>
    public class TouchSensor : MonoBehaviour
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
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods
        // \cond
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                // Find top parent
                Transform temp = other.gameObject.transform;
                Transform parent = other.gameObject.transform; 
                while (parent != null)
                {
                    temp = parent;
                    parent = parent.parent;
                }
                parent = temp;

                // set touching player to true
                // this will allow enemy to attack player
                parent.transform.GetComponent<EnemyCombat>()._touchingPlayer = true;

                Debug.Log("Touched Player");
            }
        }
        // \endcond

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
