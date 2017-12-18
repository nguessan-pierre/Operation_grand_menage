using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Statistics;

public class SwitchingWeapon : MonoBehaviour {


    Inventory _inventory;
    private EquipmentManager _equipmentManager;
    List<Equipment> weaponsList;
    int _currentWeaponIndex = 0;

    public GameObject craftmenu;

    //Weapon
    GameObject AKM;
    GameObject Gatlin;
    GameObject GunFire;
    GameObject Katana;

    GameObject activeWeapon; //Default weapon activated

    //SetUp Transform
    Vector3 localposition;
    Vector3 eulerangle;
    Vector3 localscale;

    //Reference to PlayerShooting
    PlayerShooting scriptPS;



    //Reference to characterStatiscitc
    CharacterStatistics owner;
    string name;

    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    void Awake()
    {
        owner = GetComponent<CharacterStatistics>();
        name = owner.name;

        craftmenu = getChildGameObject(gameObject, "CraftMenu");

        _inventory = GetComponent<Inventory>();
        _equipmentManager = GetComponent<EquipmentManager>();
        
        weaponsList = _inventory.getWeapons();
        _inventory.onItemChangedCallback += UpdateWeaponsList;

        //Weapon
        AKM = getChildGameObject(gameObject, "AKM");
        //AKM.transform.SetParent(gameObject.transform, false); // to keep good scaling 

        Gatlin = getChildGameObject(gameObject, "Gatling");
        //Gatlin.transform.SetParent(gameObject.transform, false);

        GunFire = getChildGameObject(gameObject, "GunFire");
        //GunFire.transform.SetParent(gameObject.transform, false);

        Katana = getChildGameObject(gameObject, "Katana");


        //LocalPosition
        localposition = AKM.transform.localPosition;
        eulerangle = AKM.transform.eulerAngles;

        //Adapt to other weapon (Using the same animation)
        //Gatlin.transform.localScale = Gatlin.transform.localScale / 2;
        Gatlin.transform.localPosition = localposition;
        Gatlin.transform.eulerAngles = eulerangle;

        //GunFire.transform.localScale = GunFire.transform.localScale / 2;
        GunFire.transform.localPosition = localposition;
        GunFire.transform.eulerAngles = eulerangle;


        switch (name)
        {
            case "Swayne The Rock":
                activeWeapon = AKM;
                break;
            case  "Gaara" :
                activeWeapon = Katana;
                break;
        } 



        //Todo : same for katana try to use axe 

        // Reference to playerSHoting
        scriptPS = gameObject.GetComponent<PlayerShooting>();

    }
	
	// Update is called once per frame
	void Update () {
        float d = Input.GetAxis("Mouse ScrollWheel");
        if (!craftmenu.activeSelf)
        {
            if (d > 0) // up
            {
                UpdateCurrentWeaponIndex(1);
                UpdateWeapon();
            }
            else if (d < 0) // down
            {
                UpdateCurrentWeaponIndex(-1);
                UpdateWeapon();
            }
        }
	}

    void UpdateWeaponsList()
    {
        weaponsList = _inventory.getWeapons();
    }

    void UpdateWeapon()
    {
        //_equipmentManager.Equip(weaponsList[_currentWeaponIndex]);
        //UpdateCurrentWeaponIndex(-1);
        WeaponType nextweapon = GetNextWeaponType();
        AttachWeaponToPlayer(nextweapon);
        scriptPS.UpdateLayer(); //update animation
    }

    void AttachWeaponToPlayer(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.AKM: //AKM
                // to do 
                //Modify current weapon
                activeWeapon.SetActive(false); //DIsable old_go
                AKM.SetActive(true); //Activate new go
                activeWeapon = AKM;
                scriptPS.currentWeapon = getChildGameObject(AKM, "akm_end").GetComponent<AKMShooting>(); //CHange currentweapon in PlayerShooting Script
                _equipmentManager.updateWeapon(scriptPS.currentWeapon);
                Debug.LogError("Switching to AKM");
                break;
            case WeaponType.Gatlin: //Machine gun
                //Modify current weapon
                activeWeapon.SetActive(false);
                Gatlin.SetActive(true);
                activeWeapon = Gatlin;
                scriptPS.currentWeapon = getChildGameObject(Gatlin, "gatling_end").GetComponent<GatlingShooting>();
                _equipmentManager.updateWeapon(scriptPS.currentWeapon);
                Debug.LogError("Switching to Gatlin");
                break;

            case WeaponType.GunFire:
                //Modify current weapon
                activeWeapon.SetActive(false);
                GunFire.SetActive(true);
                activeWeapon = GunFire;
                scriptPS.currentWeapon = getChildGameObject(GunFire, "gun_end").GetComponent<GunFire>();
                _equipmentManager.updateWeapon(scriptPS.currentWeapon);
                Debug.LogError("Switching to GunFire");
                break;

            case WeaponType.Katana:
                //Modify current weapon
                activeWeapon.SetActive(false);
                Katana.SetActive(true);
                activeWeapon = Katana;
                scriptPS.currentWeapon = Katana.GetComponent<Katana>();
                _equipmentManager.updateWeapon(scriptPS.currentWeapon);
                Debug.LogError("Switching to Katana");
                break;
        }
    }

    WeaponType GetNextWeaponType()
    {
        Equipment arme = weaponsList[_currentWeaponIndex];
        return arme.weaponType;
    }

    void UpdateCurrentWeaponIndex(int increment)
    {
        if(weaponsList.Count > 1)
        {
            if(increment > 0)
            {
                if(_currentWeaponIndex + 1 >= weaponsList.Count)
                {
                    _currentWeaponIndex = 0;
                } else
                {
                    _currentWeaponIndex += 1;
                }
            } else
            {
                if(_currentWeaponIndex <= 0)
                {
                    _currentWeaponIndex = weaponsList.Count -1;
                }
                else
                {
                    _currentWeaponIndex += -1;
                }
                       
            }
        }       
    }
}
