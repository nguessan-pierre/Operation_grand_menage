using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public int amount = 20;
    public float speedBoostPercentage = 1f;
    
    public override void Use(Player playerInfo)
    {
        base.Use(playerInfo);
        playerInfo.PlayerStatistics.Heal(amount);
        playerInfo.PlayerMovement.Speed *= speedBoostPercentage;
    }
}
