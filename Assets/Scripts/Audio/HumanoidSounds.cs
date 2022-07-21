using System.Collections.Generic;
using UnityEngine;

namespace PC.Audio
{
    [CreateAssetMenu(fileName = "HumanoidSounds", menuName = "Audio Scriptable Objects/Humanoid Sounds", order = 0)]
    public class HumanoidSounds : ScriptableObject
    {
        [SerializeField] public List<AudioClip> walking = new List<AudioClip>();
        [SerializeField] public List<AudioClip> sprinting = new List<AudioClip>();
        [SerializeField] public List<AudioClip> jumping = new List<AudioClip>();
        [SerializeField] public List<AudioClip> landing = new List<AudioClip>();
    }
}