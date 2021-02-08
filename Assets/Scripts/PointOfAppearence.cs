using UnityEngine;

public class PointOfAppearence : MonoBehaviour
{
    public static PointOfAppearence instance;

    public void Awake()
    {
        instance = this;
    }
}
