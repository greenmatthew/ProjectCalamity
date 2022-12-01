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
                _animator.SetBool("isWalking", false);
                _animator.SetBool("isTurningLeft", false);
                _animator.SetBool("isTurningRight", false);
                _animator.SetBool("isAttacking", false);
            }

        }

        private void Update()
        {
            // turning animation
            if (_animator.GetBool("isTurningRight"))
            {
                _animator.Play("Turn45Right");
                _animator.SetBool("isTurningRight", false);
            }
            if (_animator.GetBool("isTurningLeft"))
            {
                _animator.Play("Turn45Left");
                _animator.SetBool("isTurningLeft", false);
            }


            // walking animation
            if (_animator.GetBool("isWalking"))
            {
                _animator.Play("Crawl");
                _animator.SetBool("isWalking", false);
            }


            // attacking animation
            if (_animator.GetBool("isAttacking"))
            {
                _animator.Play("TongueAttack");
                _animator.SetBool("isAttacking", false);
            }
        }

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
