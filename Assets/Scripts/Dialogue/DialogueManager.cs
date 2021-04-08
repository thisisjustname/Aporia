using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;

        private Queue<string> sentences;

        public  GameObject dialogueBox;
        public GameObject questButton;
        public GameObject questButtonShop;
        private int isChoise;
        public  GameObject target;

        public  GameObject agree;
        public  GameObject notArgree;

        private float maxSpeed;

        void Start()
        {
            maxSpeed = Player.instance.GetComponent<AIPath>().maxSpeed;
            dialogueBox.SetActive(false);
            sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            Player.instance.GetComponent<AIPath>().maxSpeed = 0;
            target.transform.position = Player.instance.transform.position;
            dialogueBox.SetActive(true);
            target.SetActive(false);
            
            nameText.text = dialogue.name;
            isChoise = dialogue.isChoise;
        
            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 1)
                if (isChoise == 0 & Player.wasInLords)
                {
                    questButtonShop.SetActive(true);
                }
                else if (isChoise == 0 & !Player.wasInLords)
                {
                    questButton.SetActive(true);
                }
                else if (isChoise == 2)
                {
                    agree.SetActive(true);
                    notArgree.SetActive(true);
                }


            if (sentences.Count == 0)    
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
        }

        public void EndDialogue()
        {
            Player.instance.GetComponent<AIPath>().maxSpeed = maxSpeed;
            questButton.SetActive(false);
            target.SetActive(true);
            dialogueBox.SetActive(false);
        }

        public void Diaable()
        {
            agree.SetActive(false);
            notArgree.SetActive(false);
        }
    }
}