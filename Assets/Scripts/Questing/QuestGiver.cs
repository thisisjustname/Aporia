using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Questing
{
    public class QuestGiver : MonoBehaviour
    {
        public Quest quest;
        public Player player;
        public GameObject roots;
        public GameObject amulet;
        public GameObject questWindow;
        public GameObject done;
        private static bool questIdPicked = false;
        public TextMeshProUGUI counter;
        private bool windowIsActive;

        public TextMeshProUGUI titel;
        public TextMeshProUGUI description;

        public void AcceptQuest()
        {
            questIdPicked = true;
            quest.isActive = true;
            titel.text = quest.titel;
            description.text = quest.description;
            windowIsActive = true;
        
            Player.quest = quest;
        }

        public void Awake()
        {
            if (SceneManager.GetActiveScene().name == "start_of_forest")
            {
                roots = GameObject.Find("Root");
                amulet = GameObject.Find("Amulet");
            }
        }

        private void Update()
        {
            if (quest.goal.IsReached())
            {
                if (SceneManager.GetActiveScene().name == "start_of_forest")
                {
                    roots.GetComponent<SpriteRenderer>().enabled = true;
                    amulet.GetComponent<SpriteRenderer>().enabled = true;
                }

                Player.wasInShop = true;
                Player.wasInLords = false;
                done.SetActive(true);
            }
            counter.SetText(quest.goal.currentAmount + " из " + quest.goal.requiredAmount);
            if (questIdPicked & Input.GetKeyDown(KeyCode.Q))
            {
                windowIsActive = !windowIsActive;
                if (windowIsActive == false)
                {
                    questWindow.SetActive(true);    
                }
                else
                {
                    questWindow.SetActive(false);
                }
            }    
        }
    }
}
