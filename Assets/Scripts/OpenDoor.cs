﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    private bool playerDetected;
    public Player player;
    public GameObject pointOfApp;
    public float width;
    public int SaveOrLoad;
    public float height;

    public LayerMask whatIsPlayer;

    [SerializeField]
    private string sceneName;

    SceneSwitchManager sceneSwitch;
    private void Start()
    {
        sceneSwitch = FindObjectOfType<SceneSwitchManager>();
    }

    private void OnMouseDown()
    {
        playerDetected = Physics2D.OverlapBox(gameObject.transform.position, new Vector2(width, height), 0, whatIsPlayer);

        if (playerDetected == true)
        {
            Player.appearInPoint = true;
            sceneSwitch.SwitchScene(sceneName);
        }
        Debug.Log("Hey");
    }

    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(gameObject.transform.position, new Vector2(width, height), 0, whatIsPlayer);
        // Debug.Log(playerDetected);
        if (playerDetected == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // if (SaveOrLoad == 0)
                //     player.SavePlayer();
                // else if (SaveOrLoad == 1)
                //     player.LoadPlayer();
                // else 
                //     player.LoadPlayerWithTransform();

                Player.appearInPoint = true;
                sceneSwitch.SwitchScene(sceneName);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(width, height, 1));
    }
}