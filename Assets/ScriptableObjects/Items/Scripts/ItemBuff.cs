using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [System.Serializable]
    public class ItemBuff : IModifier
    {
        public Attributes attribute;
        public int value;
        public int min;
        public int max;

        public ItemBuff(int _min, int _max)
        {
            min = _min;
            max = _max;

            GenerateValue();
        }

        public void GenerateValue()
        {
            value = Random.Range(min, max);
        }

        public void AddValue(ref int baseValue)
        {
            baseValue += value;
        }

    }
}