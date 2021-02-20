namespace ScriptableObjects.Items.Scripts
{
    [System.Serializable]
    public class Item
    {
        public string Name;
        public ItemBuff[] buffs;
        public int Id;

        public Item()
        {
            Name = "";
            Id = -1;
        }
        public Item(ItemObject item)
        {
            Name = item.name;
            Id = item.data.Id;

            buffs = new ItemBuff[item.data.buffs.Length];
            for (int i = buffs.Length - 1; i >= 0; i--)
            {
                buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
                {
                    attribute = item.data.buffs[i].attribute
                };
            }
        }
    }
}