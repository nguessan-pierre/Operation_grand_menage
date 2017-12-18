using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager_ZombieSpawner : NetworkBehaviour
{

    [SerializeField]GameObject zombiePrefab;
    [SerializeField] GameObject zombieSpawn;
    private int counter;
    public int numberOfZombies = 2;

    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfZombies; i++)
        {
            SpawnZombies();
        }

    }

    public void SpawnZombies()
    {
        counter++;
        GameObject zombieNormal = GameObject.Instantiate(zombiePrefab, zombieSpawn.transform.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(zombieNormal);
        zombieNormal.GetComponent<Zombie_ID>().zombieID = "Zombie" + counter;
    }


    // Update is called once per frame
    void Update () {

    }

}
