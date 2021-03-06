using System.IO;
using System.Xml.Serialization;
using Aporia;
using UnityEngine;

namespace SaveSystem
{
    public class PositionSaveLoadManager : Singleton<PositionSaveLoadManager>
    {
        public SavePositionData savePositionData;

        void Start()
        {
            savePositionData = new SavePositionData();
        
            GameEvents.savePlayersPositionAndPickedUpItems += SaveGame;
        }

        public void SaveGame()
        {
            savePositionData.spawnPosition = Player.instance.transform.position;
            string filePath = Application.persistentDataPath;
        
            var serializer = new XmlSerializer(typeof(SavePositionData));
            var stream = new FileStream(filePath + "/" + savePositionData.name + ".save", FileMode.Create);
            serializer.Serialize(stream, savePositionData);
            stream.Close();
        }

        public void LoadGame()
        {
            string filePath = Application.persistentDataPath;
        
            if (File.Exists(filePath + "/" + savePositionData.name + ".save"))
            {
                var serializer = new XmlSerializer(typeof(SavePositionData));
                var stream = new FileStream(filePath + "/" + savePositionData.name + ".save", FileMode.Open);
                savePositionData = serializer.Deserialize(stream) as SavePositionData;
                stream.Close();
            }
        }

        public void DeleteData()
        {
            string filePath = Application.persistentDataPath;

            if (File.Exists(filePath + "/" + savePositionData.name + ".save"))
            {
                File.Delete(filePath + "/" + savePositionData.name + ".save");
            }
        }
    }

    [System.Serializable]
    public class SavePositionData
    {
        public static SavePositionData instance;

        public string name = "Position";
        public Vector3 spawnPosition;

        void Awake()
        {
            instance = this;
        }
    }
}