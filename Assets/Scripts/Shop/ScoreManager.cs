using TMPro;
using UnityEngine;

namespace Shop
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
        public TextMeshProUGUI text; 
        public int score;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void ChangeScore(int coinValue)
        {
            score += coinValue;
        }

        void Update()
        {
            text.text = score.ToString();
        }
    }
}
