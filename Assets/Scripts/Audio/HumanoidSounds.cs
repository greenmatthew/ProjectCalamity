using System.Collections.Generic;
using UnityEngine;

namespace PC.Audio
{
    [CreateAssetMenu(fileName = "HumanoidSounds", menuName = "Custom/HumanoidSounds", order = 0)]
    public class HumanoidSounds : ScriptableObject
    {
        [SerializeField] private List<AudioClip> walkingSounds = new List<AudioClip>();
        [SerializeField] private List<AudioClip> sprintingSounds = new List<AudioClip>();
        [SerializeField] private List<AudioClip> jumpingSounds = new List<AudioClip>();
        [SerializeField] private List<AudioClip> landingSounds = new List<AudioClip>();
    }
}