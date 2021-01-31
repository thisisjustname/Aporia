using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject player;
    
    public  GameObject agree;
    public  GameObject notArgree;

    public GameObject amulet;
    public GameObject korni;

    private float maxSpeed;

    void Start()
    {
        maxSpeed = player.GetComponent<AIPath>().maxSpeed;
        dialogueBox.SetActive(false);
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        player.GetComponent<AIPath>().maxSpeed = 0;
        target.transform.position = player.transform.position;
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
        player.GetComponent<AIPath>().maxSpeed = maxSpeed;
        questButton.SetActive(false);
        target.SetActive(true);
        dialogueBox.SetActive(false);
    }

     public void Diaable()
     {
         agree.SetActive(false);
         notArgree.SetActive(false);
     }

     public void Amulet()
     {
         amulet.SetActive(true);
     }
     
     public void Korni()
     {
         korni.SetActive(true);
     }
}