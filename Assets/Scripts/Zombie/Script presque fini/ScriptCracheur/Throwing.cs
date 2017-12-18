using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class Throwing : NetworkBehaviour
{
    // get spherecollider
    //private SphereCollider sc;

    // get NavMesh
    private NavMeshAgent agent;

    // general timer to know the time between two update
    float timer;

    // timer to shoot
    public float timeBetweenBullet = 5.0f;

    // ball shoot by zombie cracheur
    public GameObject sphere;

    // point where the ball come from
    public GameObject head;

    // animator to control marvin
    Animator anim;

    // speed of the bullet
    public int bulletSpeed = 1000;

    // Zombie health
    CharacterStatistics ZombieStats;

    GameObject player;

    PlayerHealth playerHealth;

    bool playerInRange = false;

    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //sc = GetComponent<SphereCollider>();
        ZombieStats = GetComponent<CharacterStatistics>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // we update the timer
        timer += Time.deltaTime;

        if (playerInRange && (ZombieStats.CurrentHealth > 0))
        {
            anim.SetBool("PlayerInRange", true);
            agent.enabled = false;

            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;

        }
        else
        {
            anim.SetBool("PlayerInRange", false);
            agent.enabled = true;
        }

        if ((timer >= timeBetweenBullet) && playerInRange && (ZombieStats.CurrentHealth > 0))
        {
            ThrowBall();
        }
        
    }

    public void ThrowBall()
    {

        // we reset the timer
        timer = 0f;

        // we launch the bullet
        GameObject bullet = (GameObject)Instantiate(sphere, head.transform.position, Quaternion.identity);
        bullet.gameObject.name = "Ball";
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
}
