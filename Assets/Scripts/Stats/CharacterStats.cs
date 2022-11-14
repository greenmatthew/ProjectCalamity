using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    #region Fields

    #region Consts Fields
    #endregion Consts Fields

    #region Public Fields
    #endregion Public Fields

    #region Protected Fields
    #endregion Protected Fields

    #region Private Fields
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth { get; set; }
    [SerializeField] private Stat _damage;
    #endregion Private Fields

    #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

    #region Methods

    #region Public Methods
    public void TakeDamage(int damage)
    {
        _currentHealth -= _damage.GetValue();
        Debug.Log(transform.name + " takes " + _damage.GetValue() + " damage.");
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
    #endregion Public Methods

    #region Protected Methods
    #endregion Protected Methods

    #region Private Methods
    private void Awake()
    {
        _maxHealth = 100;
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
        }
    }
    
    #endregion Private Methods

    #endregion Methods

//----------------------------------------------------------------------------------------------------------------------

    #region Enums, Structs, Classes
    #endregion Enums, Structs, Classes
}
