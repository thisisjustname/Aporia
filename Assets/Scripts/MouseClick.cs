using System;
using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour
{
    private GameObject dialog;
    private GameObject player;
    
    private Vector2 m_pos;
    public float step;
    private float progress;

    private void Awake()
    {
        dialog = GameObject.Find("DialogueBox");
        player = GameObject.FindGameObjectWithTag("Player");
        // transform.position = player.transform.position;
    }

    void Update ()
    {    
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = new Vector2(screenPosition.x, screenPosition.y);
        }
    }
}