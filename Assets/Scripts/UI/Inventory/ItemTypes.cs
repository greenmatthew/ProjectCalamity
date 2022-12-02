using System.Collections.Generic;
using UnityEngine;

namespace PC.UI
{
    public class ItemTypes : MonoBehaviour
    {
        #region Fields

        #region Consts Fields
        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private List<ItemType> types = new List<ItemType>();

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods

        /// <summary>
        /// Checks if a type belongs to a list of types.
        /// </summary>
        /// <param name="item">The type to check.</param>
        /// <returns>True if the type belongs to the list of types, else false.</returns>
        public bool belongsTo(ItemType item)
        {
            foreach (var type in types)
            {
                if (type.belongsTo(item))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods
        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes
        #endregion Enums, Structs, Classes
    }
}