using UnityEngine;
using UnityEngine.SceneManagement;

namespace PC.Stats
{
    public class PlayerStats : CharacterStats
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
        public override void Die()
        {
            base.Die();

            // reload scene when player dies
            // TODO: figure out why all the assets like audio seem to disappear when reloading the scene
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
