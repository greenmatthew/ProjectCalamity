using UnityEngine;

namespace PC.Combat
{ 
    public class EnemyAnimationController : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _enemyRB;
        [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
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
            if(_animator)
            {
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsTurningLeft", false);
                _animator.SetBool("IsTurningRight", false);
                _animator.SetBool("IsAttacking", false);
            }

        }

        private void Update()
        {
            // track the enemy's velocity and angular velocity
            //Debug.Log("Enemy's velocity: " + _enemyRB.velocity.magnitude);
            //Debug.Log("Enemy's angular velocity: " + _enemyRB.angularVelocity.x);
            Debug.Log("Enemy's velocity: " + _navMeshAgent.velocity.magnitude);
            Debug.Log("Enemy's angular velocity: " + _enemyRB.angularVelocity.y * 100);
            // set angular velocity of rigid body
            if (_animator)
            {
                _animator.SetFloat("LinearVelocity", _enemyRB.velocity.magnitude);
                _animator.SetFloat("AngularVelocity", _enemyRB.angularVelocity.magnitude);
            }


            // turning animation
            //if (_animator.GetBool("IsTurningRight"))
            //{
            //    _animator.Play("Turn45Right");
            //    _animator.SetBool("IsTurningRight", false);
            //}
            //if (_animator.GetBool("IsTurningLeft"))
            //{
            //    _animator.Play("Turn45Left");
            //    _animator.SetBool("IsTurningLeft", false);
            //}


            //// walking animation
            //if (_animator.GetBool("IsWalking"))
            //{
            //    _animator.Play("Crawl");
            //    _animator.SetBool("IsWalking", false);
            //}


            //// attacking animation
            //if (_animator.GetBool("IsAttacking"))
            //{
            //    _animator.Play("TongueAttack");
            //    _animator.SetBool("IsAttacking", false);
            //}
        }

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
