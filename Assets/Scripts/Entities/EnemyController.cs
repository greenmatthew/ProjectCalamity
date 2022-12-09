using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using PC.Combat;

namespace PC.Entities
{
    /// <summary>
    /// EnemyController is a class that handles the enemy's AI.\n
    /// The enemy is programmed with a navmeshagent to follow and attack the player when the player is in a specific range.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        // \cond
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

            // if the player is within the look radius, chase the player
            if (distance <= _lookRadius)
            {
                // rotate towards target
                FaceTarget();

                agent.SetDestination(target.position);
                _animator.SetBool("IsWalking", true);

                if (distance <= agent.stoppingDistance)
                {
                    _animator.SetBool("IsWalking", false);

                    // attack target
                    if (this.TryGetComponent<EnemyCombat>(out EnemyCombat ec) && target != null)
                    {
                        ec.AttackTarget(target);
                        _animator.SetBool("IsAttacking", true);
                    }
                }
                else
                {
                        _animator.SetBool("IsAttacking", false);
                }

            }

            // disable if dead
            if (_animator.GetBool("IsDead"))
            {
                agent.enabled = false;
                this.enabled = false;
            }
        }
        // \endcond

        /// <summary>
        /// FaceTarget is a method that rotates the enemy towards the player during a chase.
        /// </summary>
        private void FaceTarget()
        {
            // rotate towards target
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // smooth rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
        }

        // \cond
        private void OnDrawGizmosSelected()
        {
            // visualize the look radius
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _lookRadius);
        }
        // \endcond

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }

}
