using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TestSale : MonoBehaviour {


    public GameObject gatlingworld;
    public GameObject gatlinghuman;
	// Use this for initialization
	void Start () {
        Debug.LogError("Scale world" + gatlingworld.transform);
        Debug.LogError("Scale human" + gatlinghuman.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
