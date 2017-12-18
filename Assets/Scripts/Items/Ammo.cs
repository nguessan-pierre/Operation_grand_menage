using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Inventory/Ammo")]
public class Ammo : Item
{
    public int amount = 20;
    
    public override void Use(Player playerInfo)
    {
        base.Use(playerInfo);
        playerInfo.EquipmentManager.AddAmmo(amount);
        RemoveFromInventory(playerInfo);
    }
}
