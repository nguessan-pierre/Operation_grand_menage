using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
	public Recipe Recipe;
	public Transform ComponentsContainer;
	public Transform Result;
	public Button CraftButton;

	private void Awake()
	{
		CraftButton = GetComponent<Button>();
	}
}
