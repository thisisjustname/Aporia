using Questing;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public GameObject sign;
    public GameObject questGiver;
    public GameObject questDone;
    private void OnMouseDown()    
    {
        //
        sign.SetActive(false);
        questGiver.GetComponent<QuestGiver>().quest.goal.currentAmount = 1;
        transform.localScale = new Vector3(4, 4, 4);
    }
}
