using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
     public bool isActive;

     public string titel;
     public string description;
     public static int counter;
     public int goldReward;

     public QuestGoal goal;
}
