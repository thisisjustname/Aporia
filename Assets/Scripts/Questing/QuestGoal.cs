namespace Questing
{
    [System.Serializable]
    public class QuestGoal
    {
        public GoalType goalType;

        public int requiredAmount;
        public int currentAmount;

        public bool IsReached()
        {   
            return (currentAmount >= requiredAmount);
        }
    
    }

    public enum GoalType
    {
        Kill, 
        Gathering, 
        Choice
    }
}