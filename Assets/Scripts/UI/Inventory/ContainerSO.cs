using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC.UI
{
    [CreateAssetMenu(fileName = "ContainerSO", menuName = "SeniorDesignProject/ContainerSO", order = 0)]
    public class ContainerSO : ScriptableObject
    {
        public string Name;
        [Range(1, 100)] public int Width;
        [Range(1, 100)] public int Height;
    }
}
