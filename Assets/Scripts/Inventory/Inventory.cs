using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	void Start ()
	{
		instance = this;
		PlayerMovement = GetComponent<PlayerMovement>();
		PlayerInfo = GetComponent<Player>();
	}

	#endregion

	// Callback which is triggered when
	// an item gets added/removed.
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;


	public int space = 9;	// Amount of slots in inventory
	public float maximumWeight = 20f;
	public PlayerMovement PlayerMovement;
	public Player PlayerInfo;

	// Current list of items in inventory
	public Dictionary<Item, int> items = new Dictionary<Item, int>();

	// Add a new item. If there is enough room we
	// return true. Else we return false.
	public bool Add (Item item, int amount)
	{
		// Don't do anything if it's a default item
		if (!item.isDefaultItem)
		{
			if ((this.GetWeight() + item.weight) > PlayerMovement.Statistics.GetCarriableWeight())
			{
				Debug.Log("Not enough strengh: can only carry " + PlayerMovement.Statistics.GetCarriableWeight() + ".");
				return false;
			}
			
			// Check if out of space
			if (items.Count >= space)
			{
				Debug.Log("Not enough room.");
				return false;
			}

			if (items.ContainsKey(item))
			{
				items[item] += amount;
			}
			else
			{
				items.Add(item, amount);
			}
			Debug.Log("New inventory weight: " + GetWeight() + " " +
			          "(can carry " + PlayerMovement.Statistics.GetCarriableWeight() + ").");

            // Trigger callback
			if (onItemChangedCallback != null)
			{
				Debug.Log("Invoking callback");

				onItemChangedCallback.Invoke();
			}
			else
			{
				Debug.Log("No callback.");
			}
		}

		return true;
	}

	public int GetItemPossessedAmount(Item item)
	{
		if (items.ContainsKey(item))
		{
			return items[item];
		}

		return 0;
	}

	// Remove an item
	public void Remove (Item item)
	{
		Remove(item, 1);
    }

	public void Remove(Item item, int amount)
	{
		
		if (items.ContainsKey(item))
		{
			if (items[item] >= amount)
				items[item] -= amount;
		}

		if (items[item] == 0)
			items.Remove(item);

		// Trigger callback
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
	}

	public float GetWeight()
	{
		float weight = 0f;
		
		foreach (var item in items.Keys)
		{
			weight += item.weight;
		}

		return weight;
	}

    public List<Equipment> getWeapons()
    {
        List<Equipment> resu = new List<Equipment>();
        foreach (Item it in items.Keys) {
            if (it is Equipment)
            {
                Equipment equi = (Equipment)it;
                if (equi.equipSlot.Equals(EquipmentSlot.Weapon)){
                    resu.Add(equi);
                }
            }
        }
        return resu;
    }

	public void TryRecipe(Recipe recipe)
	{
		if (recipe.CanBeAssembled(this))
		{
			recipe.ConsumeComponents(this);
			Add(recipe.targetItem, 1);
			PlayerInfo.DisplayMessage("success", "L'objet " + recipe.targetItem.name + " a été créé avec succès !");
		}
		else
		{
			PlayerInfo.DisplayMessage("error", "Il manque des ingrédients !");
		}
	}
}
