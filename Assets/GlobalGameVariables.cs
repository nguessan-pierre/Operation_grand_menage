using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameVariables : MonoBehaviour
{
	public List<Recipe> Recipes = new List<Recipe>();
	public float DeadBodyLifetime = 3f;
	public float timeLeft = 900.0f;
    public static float f_d;
	public GameObject ClearedAreaUI;
	
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public void Update()
    {
        //Debug.LogError("f_d = " + f_d);
    }
}
