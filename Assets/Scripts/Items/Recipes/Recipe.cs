
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipes")]
public class Recipe : ScriptableObject
{
    public Item targetItem;

    [SerializeField]
    public List<Item> Components = new List<Item>();

    [SerializeField]
    public List<int> ComponentsAmounts  = new List<int>();

    public bool CanBeAssembled(Inventory inventory)
    {
        int leftOvers = Components.Count;
        int i = 0;
        
        foreach (var item in Components)
        {
            if (inventory.GetItemPossessedAmount(item) >= ComponentsAmounts[i])
                leftOvers--;

            i++;
        }

        return leftOvers == 0;
    }

    public void ConsumeComponents(Inventory inventory)
    {
        int i = 0;
        
        foreach (var item in Components)
        {
            inventory.Remove(item, ComponentsAmounts[i]);

            i++;
        }
    }


    public int GetAmountOfComponents()
    {
        return ComponentsAmounts.Count;
    }
}
