using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class DontMoveWithMagic : MonoBehaviour
{
    public GameObject stick;

    public void Update()
    {
        if (stick.activeSelf)
        {
            gameObject.GetComponent<AIPath>().maxSpeed = 0;
        }
        else
        {
            gameObject.GetComponent<AIPath>().maxSpeed = 18;
        }
    }
}
