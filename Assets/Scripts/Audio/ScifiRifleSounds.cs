using System.Collections.Generic;
using UnityEngine;

namespace PC.Audio
{
    [CreateAssetMenu(fileName = "ScifiRifleSounds", menuName = "Audio Scriptable Objects/Scifi Rifle Sounds", order = 1)]
    public class ScifiRifleSounds: ScriptableObject
    {
        [SerializeField] public AudioClip shoot;
        [SerializeField] public AudioClip reload;
        [SerializeField] public AudioClip dryFire;
        [SerializeField] public AudioClip handle;
    }
}
