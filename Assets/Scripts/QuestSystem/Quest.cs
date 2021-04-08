namespace QuestSystem
{
    public class Quest
    {
        public enum QuestProgress
        {
            NotAvailable,
            Available,
            Accepted,
            Completed,
            Done,
        }

        public string title;
        public int id;
        public QuestProgress progress;
        public string description;
        public string hint;
        public string congratulation;
        public string summary;
    } 
} 