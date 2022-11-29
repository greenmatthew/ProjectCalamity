using UnityEngine;

namespace PC.Entities
{
    public class PlayerManager : MonoBehaviour
    {
        #region Singleton
        public static PlayerManager instance;
        #endregion

        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        public GameObject player;
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
        private void Awake()
        {
            instance = this;
        }

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
