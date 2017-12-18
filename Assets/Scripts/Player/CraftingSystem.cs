using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
	public Player PlayerInfo;
    public GameObject CraftingMenu;
    public bool isCraftingMenuActive;
	public GlobalGameVariables GlobalVariables;

    public RecipeSlot RecipeUiSlotModel;
	public ComponentSlot ComponentUiSlotModel;

    public Transform recipesContainer;
    RecipeSlot[] recipesSlots;	// List of all the slots

    public void Awake()
    {
        CraftingMenu = PlayerShooting.getChildGameObject(gameObject, "CraftMenu");
        recipesContainer = PlayerShooting.getChildGameObject(gameObject, "RecipesContainer").transform;
        CraftingMenu.SetActive(false);
        isCraftingMenuActive = false;
        recipesSlots = recipesContainer.GetComponentsInChildren<RecipeSlot>();
	    PlayerInfo = GetComponent<Player>();
	    PlayerInfo.PlayerInventory.onItemChangedCallback += UpdateUI;

    }
	
	public void CreateRecipeUI(Recipe recipe)
	{
	    RecipeSlot recipeSlot = Instantiate(RecipeUiSlotModel);
		Button craftButton = PlayerShooting.getChildGameObject(recipeSlot.gameObject, "CraftingButton").GetComponent<Button>();
		craftButton.onClick.AddListener(delegate { PlayerInfo.PlayerInventory.TryRecipe(recipe); });
	    Debug.LogError("Created for recipe " + recipe);

		foreach (Item component in recipe.Components)
		{
			
			ComponentSlot componentSlot = Instantiate(ComponentUiSlotModel);
			componentSlot.transform.SetParent(recipeSlot.ComponentsContainer,false);
            Image icon = PlayerShooting.getChildGameObject(componentSlot.gameObject, "Icon").GetComponent<Image>();
			icon.sprite = component.icon;

			Text text = componentSlot.GetComponentInChildren<Text>();
			int componentIndex = recipe.Components.IndexOf(component);
			int possessed = PlayerInfo.PlayerInventory.GetItemPossessedAmount(component);
			int needed = recipe.ComponentsAmounts[componentIndex];
			
			text.text = possessed + "/" + needed;

			if (possessed >= needed)
			{
				text.color = Color.green;
			}
			else
			{
				text.color = Color.red;
			}
		}

		if (recipe.targetItem != null)
		{
			Debug.LogError("Recipe target item: " + recipe.targetItem.name);

			Image resultIcon = recipeSlot.Result.GetComponentInChildren<Image>();

			resultIcon.sprite = recipe.targetItem.icon;
		}
	    
	    recipeSlot.transform.SetParent(recipesContainer,false);
	}
    
    void UpdateUI ()
    {
	    var children = new List<GameObject>();
	    foreach (Transform child in recipesContainer) children.Add(child.gameObject);
	    children.ForEach(child => Destroy(child));
	    
	    foreach (Recipe recipe in PlayerInfo.Recipes)
	    {
		    CreateRecipeUI(recipe);
	    }
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isCraftingMenuActive)
            {
                CraftingMenu.SetActive(false);
                isCraftingMenuActive = false;
            } else
            {
                CraftingMenu.SetActive(true);
                isCraftingMenuActive = true;
	            UpdateUI();
            }
        }
    }
}
