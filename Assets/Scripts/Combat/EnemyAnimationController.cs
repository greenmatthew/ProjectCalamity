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
        [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _enemyTransform = null;
        private Vector3 lastFacing = new Vector3(0, 0, 0);
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

            lastFacing = _enemyTransform.forward;

        }

        private void Update()
        {
            // calculate angular velocity from forward vector
            Vector3 forward = _enemyTransform.forward;
            //Vector3 angularVelocity = Vector3.Cross(forward, _enemyRB.velocity);
            //float angularVelocity = Vector3.SignedAngle(_enemyTransform.forward, _navMeshAgent.velocity, Vector3.up);
            float angularVelocity = Vector3.Angle(forward, lastFacing) / Time.deltaTime;
            lastFacing = forward;

            Debug.Log("LinearVelocity:" +  _navMeshAgent.velocity.magnitude);
            Debug.Log("AngularVelocity:" + angularVelocity);

            // set angular velocity of rigid body
            if (_animator)
            {
                _animator.SetFloat("LinearVelocity", _navMeshAgent.velocity.magnitude);
                _animator.SetFloat("AngularVelocity", angularVelocity);
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
