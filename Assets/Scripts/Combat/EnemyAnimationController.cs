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
        public void Die()
        {
            _animator.SetBool("IsDead", true);
        }
        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private void Start()
        {
            if(_animator)
            {
                _animator.SetBool("IsAttacking", false);
            }

            lastFacing = _enemyTransform.forward;

        }

        private void Update()
        {
            // calculate angular velocity from forward vector
            Vector3 forward = _enemyTransform.forward;
            float angularVelocity = Vector3.Angle(forward, lastFacing) / Time.deltaTime;
            lastFacing = forward;

            // set angular velocity of rigid body
            if (_animator)
            {
                _animator.SetFloat("LinearVelocity", _navMeshAgent.velocity.magnitude);
                _animator.SetFloat("AngularVelocity", angularVelocity);
            }

        }

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
