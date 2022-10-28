using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.UI
{
    [CreateAssetMenu(fileName = "OLD_ContainerSO", menuName = "SeniorDesignProject/OLD_ContainerSO", order = 0)]
    public class OLD_ContainerSO : ScriptableObject
    {
        public string Name;
        [Range(1, 100)] public int Width;
        [Range(1, 100)] public int Height;
    }
}
