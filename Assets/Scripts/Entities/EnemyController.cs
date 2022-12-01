using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using PC.Combat;

namespace PC.Entities
{
    public class EnemyController : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        Transform target;
        NavMeshAgent agent;
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        // radius within which the enemy will chase the player
        [SerializeField] private float _lookRadius = 10f;
        [SerializeField] private Animator _animator;
        
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
            agent = GetComponent<NavMeshAgent>();
            target = PlayerManager.instance.player.transform;
        }

        private void Update()
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= _lookRadius)
            {
                // rotate towards target
                FaceTarget();

                agent.SetDestination(target.position);
                //_animator.SetBool("isWalking", true);

                if (distance <= agent.stoppingDistance)
                {
                    // play attack animation

                    // attack target
                    if (this.TryGetComponent<EnemyCombat>(out EnemyCombat ec) && target != null)
                    {
                        ec.AttackTarget(target);
                        //_animator.SetBool("isAttacking", true);
                    }
                }

            }
                // move towards the player -----OLD!!!!!
                //transform.position = Vector3.MoveTowards(transform.position, target.position, 1f * Time.deltaTime);
        }

        void FaceTarget()
        {
            // play rotation animation    
            //_animator.SetBool("isTurningLeft", true);
            //_animator.SetBool("isTurningRight", true);

            // rotate towards target
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            // smooth rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
