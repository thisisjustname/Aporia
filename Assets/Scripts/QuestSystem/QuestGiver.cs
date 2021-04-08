using UnityEngine;

namespace QuestSystem
{
    public class QuestGiver
    {
        public bool AssignedQuest { get; set; }
        public bool Helped { get; set; }
        [SerializeField] private GameObject quests;

        public void Interact()
        {
            if (!AssignedQuest && !Helped)
            {
                
            }
            else if (AssignedQuest && !Helped)
            {
                
            }
        }
    }
}