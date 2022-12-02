using System;
using System.Collections.Generic;
using UnityEngine;

namespace PC.UI
{
    public class ItemType
    {
        #region Fields

        #region Consts Fields

        private const uint LAYER_1_MASK = 0xFF000000;
        private const uint LAYER_2_MASK = 0x00FF0000;
        private const uint LAYER_3_MASK = 0x0000FF00;
        private const uint LAYER_4_MASK = 0x000000FF;

        private static readonly Dictionary<Value, ItemType> dict = InitLookup();

        #endregion Consts Fields

        #region Public Fields
        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields

        private uint h_data = 0x00000000;
        private uint data
        {
            get => h_data;
            set
            {
                h_data = value;
                layers[0] = (byte)((h_data & LAYER_1_MASK) >> 24);
                layers[1] = (byte)((h_data & LAYER_2_MASK) >> 16);
                layers[2] = (byte)((h_data & LAYER_3_MASK) >> 8);
                layers[3] = (byte)(h_data & LAYER_4_MASK);
            }
        }

        private string dataAsHex => $"0x{data.ToString("X8")}";

        private byte[] layers = new byte[4];
        private byte layer1 => layers[0];
        private byte layer2 => layers[1];
        private byte layer3 => layers[2];
        private byte layer4 => layers[3];

        private int lastLayerIndex => layer4 == 0 ? layer3 == 0 ? layer2 == 0 ? 0 : 1 : 2 : 3;

        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods

        /// <summary>
        /// Checks if a type belongs to another type.
        /// </summary>
        /// <param name="item">The type to check.</param>
        /// <returns>True if the type belongs to the other type, else false.</returns>
        public bool belongsTo(ItemType item)
        {
            for (int i = lastLayerIndex; i >= 0; --i)
                if (layers[i] != item.layers[i])
                    return false;
            return true;
        }

        public static implicit operator uint(ItemType item)
        {
            return item.data;
        }

        public static bool operator==(ItemType a, ItemType b)
        {
            if (b is null)
                return false;
            return a.data == b.data;
        }

        public static bool operator==(ItemType a, Value b)
        {
            return a.data == (uint)b;
        }

        public override bool Equals(object a)
        {
            return data == ((ItemType)a).data;
        }

        public static bool operator!=(ItemType a, ItemType b)
        {
            return a.data != b.data;
        }

        public static bool operator!=(ItemType a, Value b)
        {
            return a.data != (uint)b;
        }

        public static ItemType Lookup(Value value)
        {
            dict.TryGetValue(value, out ItemType item);
            if (item == null)
                throw new Exception("ItemType not found: " + value);
            return item;
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods

        private ItemType(Value val)
        {
            this.data = (uint)val;
        }

        private static Dictionary<Value, ItemType> InitLookup()
        {
            var dict = new Dictionary<Value, ItemType>();
            foreach (Value value in (Value[]) Enum.GetValues(typeof(Value)))
            {
                dict.Add(value, new ItemType(value));
            }

            return dict;
        }

        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes

        public enum Value : System.UInt32
        {
            NONE               = 0x00000000,

            EQUIPMENT          = 0x01000000,
            EQUIPMENT_BACKPACK = 0x01010000,
            EQUIPMENT_ARMOR    = 0x01020000,
            EQUIPMENT_SHIELD   = 0x01030000,

            FIREARM            = 0x02000000,
            FIREARM_PISTOL     = 0x02010000,
            FIREARM_RIFLE      = 0x02020000,
            FIREARM_SHOTGUN    = 0x02030000,
            FIREARM_SNIPER     = 0x02040000,

            MAGAZINE           = 0x03000000,

            AMMUNITION         = 0x04000000,

            RESOURCE           = 0x05000000,

            QUEST              = 0x06000000,
        }

        #endregion Enums, Structs, Classes
    }
}