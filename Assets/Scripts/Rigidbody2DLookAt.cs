using UnityEngine;
using Pathfinding;

public class Rigidbody2DLookAt : MonoBehaviour
{
    public GameObject shileld;
    public AIPath aiPath;
    public Animator animator;
    private AnimatorClipInfo[] clipInfo;

    public float walking_forward = 5f;
    public float max_speed = 10;

    //
    // void Awake()
    // {
    //     shileld = GameObject.Find("Shield_all");
    // }

    private void Update()
    {
        float horizontal = aiPath.velocity.x / max_speed;
        float vertical = aiPath.velocity.y / max_speed;
        // Debug.Log(aiPath.velocity.x);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        animator.SetFloat("Speed", aiPath.velocity.sqrMagnitude);
        clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        if (clipInfo[0].clip.name == "player_back")
        {
            shileld.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            shileld.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }
}
