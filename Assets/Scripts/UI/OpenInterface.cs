using System;
using Pathfinding;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class OpenInterface: MonoBehaviour
    {
        public bool InventoryEnabled = true;
        public GameObject target;
        public float maxSpeed;
        public GameObject inventory;
        public GameObject equipment;
        public GameObject uIPlayerTest;
        public Canvas uIPlayerTestCanvas;
        public AIPath airPath;

        public void Awake()
        {
            uIPlayerTestCanvas = uIPlayerTest.GetComponent<Canvas>();
            // inventory = gameObject.transform.GetChild(0).gameObject;
            // equipment = gameObject.transform.GetChild(1).gameObject;
            //
            // inventory.SetActive(true);
            // equipment.SetActive(true);
        }

        void Start()
        {
            target = GameObject.Find("Target");
            airPath = Player.instance.GetComponent<AIPath>();

            uIPlayerTestCanvas.enabled = InventoryEnabled;
            // inventory.SetActive(InventoryEnabled);
            // equipment.SetActive(InventoryEnabled);
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
                    uIPlayerTestCanvas.enabled = InventoryEnabled;
                    // inventory.SetActive(true);
                    // equipment.SetActive(true);
                }
                else
                {
                    airPath.maxSpeed = maxSpeed;
                    target.SetActive(true);
                    // inventory.SetActive(false);
                    // equipment.SetActive(false);
                    uIPlayerTestCanvas.enabled = InventoryEnabled;
                }
            }
        }
    }
}