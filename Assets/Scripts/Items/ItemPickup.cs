using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable {
    public Item item;	// Item to put in the inventory on pickup
    public int amount = 1;

    // When the player interacts with the item
    public override bool Interact(Player playerInfo)
    {
        base.Interact(playerInfo);

        return PickUp(playerInfo);	// Pick it up!
    }

    // Pick up the item
    bool PickUp (Player playerInfo)
    {
        if (item.type == Item.ItemType.NONE || playerInfo.PickableTypes.Contains(item.type))
        {
            Debug.LogError("Picking up " + item.name);
            bool wasPickedUp = playerInfo.PlayerInventory.Add(item, amount); // Add to inventory

            // If successfully picked up
            if (wasPickedUp)
                Destroy(gameObject); // Destroy item from scene

            return true;
        }
        
        playerInfo.DisplayMessage("error", "Vous ne pouvez pas ramasser ce type d'objet.");
        return false;
    }
    
        
    public override void ToggleToolTip(Player player)
    {
        item.ToggleToolTip(player);
    }
}