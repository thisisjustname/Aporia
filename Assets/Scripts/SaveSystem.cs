using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem: MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
    }

    public void SaveGame()
    {
        
    }

    public void LoadGame()
    {
        
    }
}

[System.Serializable]
public class Save
{
    
}
