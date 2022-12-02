using UnityEngine;

namespace PC.Combat
{
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

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
