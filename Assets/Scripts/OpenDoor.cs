using SaveSystem;
using UnityEngine;


public class OpenDoor : MonoBehaviour
{
    private bool playerDetected;
    public Player player;
    public GameObject pressE;
    public float width;
    public bool saveData;
    public float height;

    public LayerMask whatIsPlayer;

    [SerializeField]
    private string sceneName;
    
    SceneSwitchManager sceneSwitch;
    private void Start()
    {
        sceneSwitch = FindObjectOfType<SceneSwitchManager>();
    }

    // private void OnMouseDown()
    // {
    //     playerDetected = Physics2D.OverlapBox(gameObject.transform.position, new Vector2(width, height), 0, whatIsPlayer);
    //
    //     if (playerDetected == true)
    //     {
    //         Player.appearInPoint = true;
    //         sceneSwitch.SwitchScene(sceneName);
    //     }
    //     Debug.Log("Hey");
    // }

    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(gameObject.transform.position, new Vector2(width, height), 0, whatIsPlayer);
        // Debug.Log(playerDetected);
        if (playerDetected)
        {
            pressE.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (saveData)
                {
                    GameEvents.savePlayersPositionAndPickedUpItems?.Invoke();
                    Player.appearInPoint = false;
                }
                else
                {   
                    Player.appearInPoint = true;
                }
                Debug.Log("Saved");
                sceneSwitch.SwitchScene(sceneName);
            }
        }
        else
        {
            pressE.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(width, height, 1));
    }
}
