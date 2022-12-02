using UnityEngine;
using PC.Stats;
using TMPro;

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
        public GameObject player = null;
        public GameObject healthCount = null;
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

        private void Update()
        {
            // update health count
            if (player && healthCount)
            {
                TextMeshProUGUI tmp = healthCount.GetComponent<TextMeshProUGUI>();

                if (tmp)
                {
                    tmp.text = "Health: " + player.GetComponent<CharacterStats>()._currentHealth.ToString();
                }
                else
                {
                    Debug.Log("No text attribute found for health count");
                }
            }

        }

        #endregion Private Methods

        #endregion Methods

        //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}
