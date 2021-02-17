using Pathfinding;
using UnityEditor.UIElements;
using UnityEngine;

namespace UI
{
    public class OpenInterface: MonoBehaviour
    {
        public bool InventoryEnabled = true;
        public GameObject target;
        public float maxSpeed;
        public GameObject inventory;
        public GameObject equipment;
        public AIPath airPath;

        void Start()
        {
            target = GameObject.Find("Target");
            airPath = Player.instance.GetComponent<AIPath>();
            inventory = gameObject.transform.GetChild(0).gameObject;
            equipment = gameObject.transform.GetChild(1).gameObject;
            inventory.SetActive(InventoryEnabled);
            equipment.SetActive(InventoryEnabled);
            // inventoryCanvas.SetActive(InventoryEnabled);
            maxSpeed = Player.instance.GetComponent<AIPath>().maxSpeed;
        }

        public void Update()
        {
            Open();
        }
        
        public void Open()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                InventoryEnabled = !InventoryEnabled;
                if (InventoryEnabled)
                {
                    airPath.maxSpeed = 0;
                    target.transform.position = gameObject.transform.position;
                    target.SetActive(false);
                    inventory.SetActive(true);
                    equipment.SetActive(true);
                }
                else
                {
                    airPath.maxSpeed = maxSpeed;
                    inventory.SetActive(false);
                    target.SetActive(true);
                    equipment.SetActive(false);
                }
            }
        }
    }
}