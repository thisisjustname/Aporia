using SaveSystem;
using UnityEngine;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;
        
        public string id;
        public string ID { get => id; private set => id = value; }
        
        public void Awake()
        {
            ID = gameObject.scene.name + name + transform.position.sqrMagnitude + transform.GetSiblingIndex();
            if (CollectibleItemSet.Instance.CollectedItems.Contains(ID))
                Destroy(gameObject);
        }

        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            CollectibleItemSet.Instance.CollectedItems.Add(ID);
        }
    }
}
