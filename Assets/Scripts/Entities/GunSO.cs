using UnityEngine;

namespace PC.Entities
{
    /// <summary>
    /// This class defines various parameters that control the gun's functionality and behavior.
    /// For instance, the gun's damage, range, and fire rate default values are defined here.
    /// </summary>
    [CreateAssetMenu(fileName = "Gun", menuName = "Gun Scriptable Objects/Gun", order = 0)]
    public class GunSO : ScriptableObject
    {
        /// <summary>
        /// The range within the gun can deal damage. 
        /// </summary>
        [SerializeField] public float Range = 100f;

        /// <summary>
        /// The damage the gun deals to the target.
        /// </summary>
        [SerializeField] public float Damage = 20f;

        /// <summary>
        /// The number of rounds in the gun's magazine.
        /// </summary>
        [SerializeField] public int MagazineSize = 30;

        /// <summary>
        /// Max fire rate of the gun.
        /// </summary>
        [SerializeField] public float FireRate = 10f; // Rounds per second

        /// <summary>
        /// Time to reload.
        /// </summary>
        [SerializeField] public float ReloadTime = 3.0f;
        
        /// <summary>
        /// Vertical recoil component magnitude.
        /// </summary>
        [SerializeField] public float VerticalRecoil = 2f;

        /// <summary>
        /// Horizontal recoil component magnitude.
        /// </summary>
        [SerializeField] public float HorizontalRecoil = 0.5f;
    }
}