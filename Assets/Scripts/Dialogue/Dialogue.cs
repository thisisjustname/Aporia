using UnityEngine;

namespace Dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        public int isChoise;
        public string name;
    
        [TextArea(3, 10)]
        public string[] sentences;
    
    }
}