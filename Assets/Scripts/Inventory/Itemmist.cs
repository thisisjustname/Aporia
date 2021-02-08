using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inventory
{
    public class Itemmist : MonoBehaviour
    {
        public string  type;
        public int ID;
        public int cost;
        public string description;
        public Sprite icon;
        public bool pickedUp;
        public int def;
        [HideInInspector]
        public bool equipped;
    
        public GameObject weapon;
    
        [HideInInspector]
        public GameObject weaponManager;
    
        public bool playerWeapon;

        public void Awake()
        {
            weaponManager = GameObject.FindWithTag("WeaponManager");
            Debug.Log(gameObject.name + SceneManager.GetActiveScene().name);
            if (!playerWeapon)
            {
                int allWeapons = weaponManager.transform.childCount;
                for (int  i=0; i < allWeapons; i++)
                {
                    if (weaponManager.transform.GetChild(i).gameObject.GetComponent<Itemmist>().ID == ID)
                    {
                        weapon = weaponManager.transform.GetChild(i).gameObject;
                    }
                }
            }
        }

        public void Update()
        {
            weaponManager = GameObject.FindWithTag("WeaponManager");
            if (!playerWeapon)
            {
                int allWeapons = weaponManager.transform.childCount;
                for (int  i=0; i < allWeapons; i++)
                {
                    if (weaponManager.transform.GetChild(i).gameObject.GetComponent<Itemmist>().ID == ID)
                    {
                        weapon = weaponManager.transform.GetChild(i).gameObject;
                    }
                }
            }
        
            if (equipped)
            {
                if (Input.GetKeyDown(KeyCode.G))
                    equipped = false;
                if(equipped == false)
                    this.gameObject.SetActive(false);
            }
        }

        public void ItemUsage()
        {
            //wepon
            if (type == "Weapon")
            {
                weapon.SetActive(true);
                weapon.GetComponent<Itemmist>().equipped = true;
             
            }
        }
    }
}
