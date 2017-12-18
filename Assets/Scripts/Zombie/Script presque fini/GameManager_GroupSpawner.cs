using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager_GroupSpawner : NetworkBehaviour
{

    [SerializeField] GameObject zombiePrefab;
    private GameObject[] zombieSpawns;
    private int counter;
    public int numberOfZombies = 2;
    private int maxNumberofZombies = 15;
    private float waveRate = 10;
    private bool isSpawnActivated = true;

    public override void OnStartServer()
    {
        zombieSpawns = GameObject.FindGameObjectsWithTag("ZombieSpawn");

        StartCoroutine(ZombieSpawner());

    }

    IEnumerator ZombieSpawner()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(waveRate);
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            if (zombies.Length < maxNumberofZombies)
            {
                CommenceSpawn();
            }
        }
    }

    void CommenceSpawn()
    {
        if (isSpawnActivated)
        {
            for(int i = 0; i < numberOfZombies; i++)
            {
                for (int j = 0; j < zombieSpawns.Length; j++)
                {
                    SpawnZombies(zombieSpawns[j].transform.position);
                }
                //int randomindex = Random.Range(0, zombieSpawns.Length);
                //SpawnZombies(zombieSpawns[randomindex].transform.position);
            }
        }
    }

    public void SpawnZombies(Vector3 spawnPos)
    {
        counter++;
        GameObject zombieNormal = GameObject.Instantiate(zombiePrefab, spawnPos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(zombieNormal);
        zombieNormal.GetComponent<Zombie_ID>().zombieID = "Zombie" + counter;
    }


    // Update is called once per frame
    void Update()
    {

    }

}

