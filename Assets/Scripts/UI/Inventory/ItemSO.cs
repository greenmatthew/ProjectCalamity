using UnityEngine;

namespace PC.UI
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "EntitySOs/ItemSO", order = 0)]
    public class ItemSO : ScriptableObject
    {
        public uint cellWidth = 1;
        public uint cellHeight = 1;

        public Color backgroundColor = Color.white;
        public Sprite itemIcon = null;

        public new string name;
        public string nickname;
        public string description;
        public float mass;
        public uint value;
    }
}