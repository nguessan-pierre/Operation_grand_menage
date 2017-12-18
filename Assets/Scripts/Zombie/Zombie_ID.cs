using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Zombie_ID : NetworkBehaviour
{

    [SyncVar] public string zombieID;
    private Transform myTransform;

	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        SetIdentity();
	}

    void SetIdentity()
    {
        if(myTransform.name == "" || myTransform.name == "ZombieNormal(clone)")
        {
            myTransform.name = zombieID;
        }
    }
}
