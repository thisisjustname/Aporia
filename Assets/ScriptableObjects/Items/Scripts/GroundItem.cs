using SaveSystem;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    public class GroundItem: MonoBehaviour, ISerializationCallbackReceiver
    {
        public ItemObject item;

        public string id;
        public string ID { get => id; private set => id = value; }

        public void Awake()
        {
            ID = gameObject.scene.name + name + transform.position.sqrMagnitude + transform.GetSiblingIndex();
        }

        public void Start()
        {
            Debug.Log(ID);
            if (CollectibleItemSet.Instance.CollectedItems.Contains(ID))
                Destroy(gameObject);
        }
        
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            
            // GetComponentInChildren<SpriteRenderer>().sprite = item.icon;
            // EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
            ID = gameObject.scene.name + name + transform.position.sqrMagnitude + transform.GetSiblingIndex();
#endif            
        }

        public void OnAfterDeserialize()
        {
           
        }
    }
}
