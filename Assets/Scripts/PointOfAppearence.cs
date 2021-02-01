using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfAppearence : MonoBehaviour
{
    public static PointOfAppearence instance;

    public void Awake()
    {
        instance = this;
    }
}
