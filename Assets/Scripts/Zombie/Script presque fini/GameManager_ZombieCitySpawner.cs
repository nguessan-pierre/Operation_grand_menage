using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager_ZombieCitySpawner : NetworkBehaviour
{

    [SerializeField] GameObject zombieNormalPrefab;
    [SerializeField] GameObject zombieCracheurPrefab;
    private GameObject[] zombieNormalSpawns;
    private GameObject[] zombieCracheurSpawns;
    private int counter;
    public int numberOfZombiesNormal = 2;
    public int numberOfZombiesCracheur = 1;
    private int maxNumberofZombiesNormal = 10;
    private int maxNumberofZombiesCracheur = 10;
    private float waveRateZombieNormal = 2;
    private float waveRateZombieCracheur = 2;
    private bool isSpawnActivated = true;

    public override void OnStartServer()
    {
        zombieNormalSpawns = GameObject.FindGameObjectsWithTag("ZombieNormalSpawnCity");
        zombieCracheurSpawns = GameObject.FindGameObjectsWithTag("ZombieCracheurSpawnCity");
        StartCoroutine(ZombieNormalSpawner());
        StartCoroutine(ZombieCracheurSpawner());
    }

    IEnumerator ZombieNormalSpawner()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(waveRateZombieNormal);
            GameObject[] zombiesNormalCity = GameObject.FindGameObjectsWithTag("ZombieNormalCity");
            if (zombiesNormalCity.Length < maxNumberofZombiesNormal)
            {
                CommenceSpawn();
            }
        }
    }

    IEnumerator ZombieCracheurSpawner()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(waveRateZombieCracheur);
            GameObject[] zombiesCracheurCity = GameObject.FindGameObjectsWithTag("ZombieCracheurCity");
            if (zombiesCracheurCity.Length < maxNumberofZombiesCracheur)
            {
                CommenceSpawn();
            }
        }
    }


    void CommenceSpawn()
    {
        if (isSpawnActivated)
        {
            for (int i = 0; i < numberOfZombiesNormal; i++)
            {
                for (int j = 0; j < zombieNormalSpawns.Length; j++)
                {
                    //int randomindex = Random.Range(0, zombieNormalSpawns.Length);
                    //SpawnZombies(zombieNormalSpawns[randomindex].transform.position, zombieNormalPrefab);
                    SpawnZombies(zombieNormalSpawns[j].transform.position, zombieNormalPrefab);
               }
            }

            for (int i = 0; i < numberOfZombiesCracheur; i++)
            {
                for (int j = 0; j < zombieCracheurSpawns.Length; j++)
                {
                    //int randomindex = Random.Range(0, zombieCracheurSpawns.Length);
                    //SpawnZombies(zombieCracheurSpawns[randomindex].transform.position, zombieCracheurPrefab);
                    SpawnZombies(zombieCracheurSpawns[j].transform.position, zombieCracheurPrefab);
                }
            }
        }
    }

    public void SpawnZombies(Vector3 spawnPos, GameObject prefab)
    {
        counter++;
        GameObject zombie = GameObject.Instantiate(prefab, spawnPos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(zombie);
        zombie.GetComponent<Zombie_ID>().zombieID = "Zombie" + counter;
    }


    // Update is called once per frame
    void Update()
    {
    }

}


