using System.Collections;
using System.Collections.Generic;
using Aporia;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : Singleton<SceneSwitchManager>
{
    public void Awake()
    {
        Debug.Log("Yep");
    }

    public string name = "awef";
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
