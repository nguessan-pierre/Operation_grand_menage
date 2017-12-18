using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Statistics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public EquipmentManager EquipmentManager;
    public Inventory PlayerInventory;
    public Text ErrorText;
    public Text AreaName, AreaZombies;
    public Transform itemInfo;
    public Item.ItemType[] PickableTypes;
    public CharacterStatistics PlayerStatistics;
    public PlayerMovement PlayerMovement;
    
    public List<Recipe> Recipes = new List<Recipe>();
    
    [Header("Player Game Statistics")]
    public int normalZombiesKilled = 0;

    public int timesFired = 0;

    private void Start()
    {
        EquipmentManager = GetComponent<EquipmentManager>();
        PlayerInventory = GetComponent<Inventory>();
        PlayerStatistics = GetComponent<CharacterStatistics>();
        PlayerMovement = GetComponent<PlayerMovement>();
        ErrorText.enabled = false;
    }

    public void DisplayMessage(string type, string message)
    {
        ErrorText.enabled = true; // Enable the text so it shows
        ErrorText.text = message;
        
        if (isLocalPlayer)
        {
            if (type == "error")
            {
                ErrorText.color = Color.red;
            }

            if (type == "success")
            {
                ErrorText.color = Color.green;
            }

            if (type == "warning")
            {
                ErrorText.color = Color.yellow;
            }
        
            Invoke("HideMessages", 3f);
        }
    }

    private void HideMessages()
    {
        ErrorText.enabled = false; // Disable the text so it is hidden
    }

    public void SavePlayerObject()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Player object saved for other scenes.");
            DontDestroyOnLoad(this);
        }
    }
}
