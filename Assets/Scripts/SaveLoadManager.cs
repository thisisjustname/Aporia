using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Aporia;

public class SaveLoadManager : MonoBehaviour
{
    public string name = "awef";
    public SaveData saveData;

    void Start()
    {
        saveData = new SaveData();
    }

    public void SaveGame()
    {
        string filePath = Application.persistentDataPath;
        
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(filePath + "/" + saveData.name + ".save", FileMode.Create);
        serializer.Serialize(stream, saveData);
        stream.Close();
    }

    public void LoadGame()
    {
        string filePath = Application.persistentDataPath;
        
        if (System.IO.File.Exists(filePath + "/" + saveData.name + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(filePath + "/" + saveData.name + ".save", FileMode.Open);
            saveData = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
    }

    public void DeleteData()
    {
        string filePath = Application.persistentDataPath;

        if (System.IO.File.Exists(filePath + "/" + saveData.name + ".save"))
        {
            File.Delete(filePath + "/" + saveData.name + ".save");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public static SaveData instance;

    public string name = "Position";
    public Vector3 spawnPosition;

    void Awake()
    {
        instance = this;
    }
}