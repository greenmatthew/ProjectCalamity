using UnityEngine;

namespace PC.Entities
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Gun Scriptable Objects/Gun", order = 0)]
    public class GunSO : ScriptableObject
    {
        

        [SerializeField] public float Range = 100f;
        [SerializeField] public float Damage = 20f;
        [SerializeField] private float _fireRate = 500f; // Rounds per minute
        private float h_fireRate = float.MinValue;
        public float FireRate
        {
            get
            {
                Debug.Log(_fireRate);
                Debug.Log(60f / _fireRate);
                if (h_fireRate == float.MinValue)
                    h_fireRate = RatePerMinToDelay(_fireRate);
                Debug.Log(h_fireRate);
                return h_fireRate;
            }
        }
        [SerializeField] public float ReloadTime = 3.0f;
        [SerializeField] public float VerticalRecoil = 2f;
        [SerializeField] public float HorizontalRecoil = 0.5f;

        private float RatePerMinToDelay(float ratePerMin)
        {
            Debug.Log($"Inside: {60f / ratePerMin}");
            return 60f / ratePerMin;
        }
    }
}