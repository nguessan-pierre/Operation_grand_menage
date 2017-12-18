using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour {

	public Image icon;			// Reference to the Icon image
	public Button removeButton;	// Reference to the remove button
	public Text amount;
	public GameObject panel;

	Item item;  // Current item in the slot

	// Add item to the slot
	public void AddItem (Item newItem, int amount)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.gameObject.SetActive(true);
		panel.SetActive(true);
		this.amount.text = amount.ToString();
		this.amount.gameObject.SetActive(true);
		removeButton.interactable = true;
	}

	// Clear the slot
	public void ClearSlot ()
	{
		item = null;

		icon.sprite = null;
		panel.SetActive(false);
		icon.gameObject.SetActive(false);
		this.amount.gameObject.SetActive(false);

		removeButton.interactable = false;
	}

	// Called when the remove button is pressed
	public void OnRemoveButton ()
	{
		//playerInfo.Remove(item);
	}

	// Called when the item is pressed
	public void UseItem (Player playerInfo)
	{
		if (item != null)
		{
			item.Use(playerInfo);
			playerInfo.PlayerInventory.Remove(item, 1);
		}
	}

}
