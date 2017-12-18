using System.Linq;
using UnityEngine;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;	// The entire UI

	public Player playerInfo;
	private Inventory inventory;	// Our current inventory

	InventorySlot[] slots;	// List of all the slots

	void Start ()
	{
		inventory = playerInfo.PlayerInventory;
		inventory.onItemChangedCallback += UpdateUI;	// Subscribe to the onItemChanged callback

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
			TryToUseSlot(0);
		
		if(Input.GetKeyDown(KeyCode.Alpha2))
			TryToUseSlot(1);
		
		if(Input.GetKeyDown(KeyCode.Alpha3))
			TryToUseSlot(2);
		
		if(Input.GetKeyDown(KeyCode.Alpha4))
			TryToUseSlot(3);
		
		if(Input.GetKeyDown(KeyCode.Alpha5))
			TryToUseSlot(4);
		
		if(Input.GetKeyDown(KeyCode.Alpha6))
			TryToUseSlot(5);
		
		if(Input.GetKeyDown(KeyCode.Alpha7))
			TryToUseSlot(6);
	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	void UpdateUI ()
	{
		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)	// If there is an item to add
			{
				Debug.Log("Updating item " + inventory.items.ElementAt(i).Key + ", amount: " + inventory.items.ElementAt(i).Value);
				slots[i].AddItem(inventory.items.ElementAt(i).Key, inventory.items.ElementAt(i).Value);	// Add it
			} else
			{
				// Otherwise clear the slot
				slots[i].ClearSlot();
			}
		}
	}

	private void TryToUseSlot(int slot)
	{
		if (slots[slot] != null)
		{
			slots[slot].UseItem(playerInfo);
		}
		
		UpdateUI();
	}
}
