using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Keep track of equipment. Has functions for adding and removing items. */

public class EquipmentManager : MonoBehaviour {

    public enum MeshBlendShape {Torso, Arms, Legs };
    public Equipment[] defaultEquipment;

	public SkinnedMeshRenderer targetMesh;

    SkinnedMeshRenderer[] currentMeshes;

	Equipment[] currentEquipment;   // Items we currently have equipped

	// Callback for when an item is equipped/unequipped
	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;

	private int ammo = 0;
	public Text ammoText;

	private Player playerInfo;

    PlayerShooting PSscript;
    IWeapon current_weapon;

    private void Awake()
    {
        PSscript = GetComponent<PlayerShooting>();
        current_weapon = PSscript.currentWeapon;
        if (current_weapon.IsAmmoNoNeeded())
        {
            ammoText.text = "\u221e";
        }
    }

    void Start ()
	{
		playerInfo = GetComponent<Player>();

		// Initialize currentEquipment based on number of equipment slots
		int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaults();


	}

	// Equip a new item
	public void Equip (Equipment newItem)
	{
		// Find out what slot the item fits in
		int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = Unequip(slotIndex);

		// An item has been equipped so we trigger the callback
		if (onEquipmentChanged != null)
		{
			onEquipmentChanged.Invoke(newItem, oldItem);
		}
		
		playerInfo.PlayerInventory.Remove(newItem);

		Debug.Log(newItem + " equipped");

		// Insert the item into the slot
		currentEquipment[slotIndex] = newItem;
        AttachToMesh(newItem, slotIndex);
	}

	// Unequip an item with a particular index
	public Equipment Unequip (int slotIndex)
	{
        Equipment oldItem = null;
		// Only do this if an item is there
		if (currentEquipment[slotIndex] != null)
		{
			// Add the item to the inventory
			oldItem = currentEquipment[slotIndex];
			playerInfo.PlayerInventory.Add(oldItem, 1);

            SetBlendShapeWeight(oldItem, 0);
            // Destroy the mesh
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

			// Remove the item from the equipment array
			currentEquipment[slotIndex] = null;

			// Equipment has been removed so we trigger the callback
			if (onEquipmentChanged != null)
			{
				onEquipmentChanged.Invoke(null, oldItem);
			}
		}
        return oldItem;
	}

	// Unequip all items
	public void UnequipAll ()
	{
		for (int i = 0; i < currentEquipment.Length; i++)
		{
			Unequip(i);
		}

        EquipDefaults();
	}

    void AttachToMesh(Equipment item, int slotIndex)
	{
		if (item.mesh != null)
		{
			SkinnedMeshRenderer newMesh = Instantiate(item.mesh) as SkinnedMeshRenderer;
			newMesh.transform.parent = targetMesh.transform.parent;

			newMesh.rootBone = targetMesh.rootBone;
			newMesh.bones = targetMesh.bones;
		
			currentMeshes[slotIndex] = newMesh;


			SetBlendShapeWeight(item, 100);
		}
        
       
	}

    void SetBlendShapeWeight(Equipment item, int weight)
    {
		foreach (MeshBlendShape blendshape in item.coveredMeshRegions)
		{
			int shapeIndex = (int)blendshape;
            targetMesh.SetBlendShapeWeight(shapeIndex, weight);
		}
    }

    void EquipDefaults()
    {
		foreach (Equipment e in defaultEquipment)
		{
			Equip(e);
		}
    }

	void Update ()
	{
        if (!current_weapon.IsAmmoNoNeeded())
        {
            ammoText.text = ammo.ToString();
        } else
        {
            ammoText.text = "\u221e";
        }
		// Unequip all items if we press U
		if (Input.GetKeyDown(KeyCode.U))
			UnequipAll();
	}

	public void AddAmmo(int amount)
	{
		ammo += amount;
		Debug.Log("Added ammo: " + amount);
	}

	public void RemoveAmmo()
	{
		ammo--;
	}

	public bool CanFire()
	{
		return ammo > 0;
	}

    public void updateWeapon(IWeapon wep)
    {
        current_weapon = wep;
    }
}
