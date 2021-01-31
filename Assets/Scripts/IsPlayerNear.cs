using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IsPlayerNear : MonoBehaviour
{
    public Rigidbody2D objects;
    private GameObject leaf;
    private Rigidbody2D myRigidbody;
    private bool playerDetected;
    public Transform Pos;
    public float width;
    public float height;

    public LayerMask whatIsPlayer;

    private void Start()
    {
        leaf = GameObject.FindGameObjectWithTag("leaf");
    }
    
    private void Update()
    {
        Pos = leaf.transform;
        playerDetected = Physics2D.OverlapBox(Pos.position, new Vector2(width, height), 0, whatIsPlayer);

        if (playerDetected == false)
        {
            myRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            Debug.Log("No");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Pos.position, new Vector3(width, height, 1));
    }
}
