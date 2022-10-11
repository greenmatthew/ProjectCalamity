using UnityEngine;

namespace PC.UI
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "EntitySOs/ItemSO", order = 0)]
    public class ItemSO : ScriptableObject
    {
        public int cellWidth = 1;
        public int cellHeight = 1;

        public Color backgroundColor = Color.white;
        public Texture2D itemIcon = null;
    }
}