using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

namespace PC.Entities
{
    public class EnemyController : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        Transform target;
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        // radius within which the enemy will chase the player
        [SerializeField] private float _lookRadius = 10f;
        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Start()
        {
            target = PlayerManager.instance.player.transform;
        }

        private void Update()
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= _lookRadius)
            {
                // move towards the player
                transform.position = Vector3.MoveTowards(transform.position, target.position, 1f * Time.deltaTime);
            }
        }

        private void OnDrawGizmosSelected()
        {
            // visualize the look radius
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _lookRadius);
        }

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
