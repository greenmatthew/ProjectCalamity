using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Stat
{
    #region Fields

    #region Consts Fields
    #endregion Consts Fields

    #region Public Fields
    #endregion Public Fields

    #region Protected Fields
    #endregion Protected Fields

    #region Private Fields
    [SerializeField] private int _baseValue = 0;
    #endregion Private Fields

    #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

    #region Methods

    #region Public Methods
    public int GetValue()
    {
        return _baseValue;
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
