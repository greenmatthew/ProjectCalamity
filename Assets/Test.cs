using UnityEngine;

public class Test : MonoBehaviour
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
    #endregion Public Methods

    #region Protected Methods
    #endregion Protected Methods

    #region Private Methods

    private void Start()
    {
        Vector2Int source = new Vector2Int(0, 0);
        Vector2Int target = new Vector2Int(1, 2);

        for (int r = source.y; r <= target.y; r++)
        {
            for (int c = source.x; c <= target.x; c++)
            {
                Vector2Int current = new Vector2Int(c, r);
                Debug.Log(current);
            }
        }
    }
    
    #endregion Private Methods

    #endregion Methods

//----------------------------------------------------------------------------------------------------------------------

    #region Enums, Structs, Classes
    #endregion Enums, Structs, Classes
}