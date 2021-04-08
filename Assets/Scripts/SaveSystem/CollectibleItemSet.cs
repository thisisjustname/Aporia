using System.Collections.Generic;
using Aporia;
using UnityEngine;

namespace SaveSystem
{
    public class CollectibleItemSet : Singleton<CollectibleItemSet>
    {
        public HashSet<string> CollectedItems { get; private set; } = new HashSet<string>();

        private void Awake()
        {
            Load();
            GameEvents.savePlayersPositionAndPickedUpItems += Save;
            if(CollectedItems == null)
                Debug.Log("nuuuulllll");
        }
        
        public void Save()
        {
            SaveLoad.Save(CollectedItems, "CollectedItems");
        }
        
        public void Load()
        {
            if (SaveLoad.SaveExists("CollectedItems"))
            {
                CollectedItems = SaveLoad.Load<HashSet<string>>("CollectedItems");
            }
        }

        [ContextMenu("Clear")]
        public void DeleteSave()
        {
            SaveLoad.DeleteSaveFile("CollectedItems");
        }

        private void OnApplicationQuit()
        {
            DeleteSave();
        }
    }
}