using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    public class GroundItem: MonoBehaviour, ISerializationCallbackReceiver
    {
        public ItemObject item;
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            
            GetComponentInChildren<SpriteRenderer>().sprite = item.icon;
            EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif            
        }

        public void OnAfterDeserialize()
        {
           
        }
    }
}
