using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Statistics;
using UnityEngine.AI;
using UnityEngine.Networking;

public class IACracheur : NetworkBehaviour
{

    /// get spherecollider
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

    private Transform targetTransform; //the zombie's target
    private Transform myTransform; //current transform data of this zombie
    private float raduis = 100;
    private LayerMask raycastLayer;
    private float speed = 0.1f;

    // Use this for initialization
    void Awake()
    {
        myTransform = transform;
        agent = GetComponent<NavMeshAgent>();
        //sc = GetComponent<SphereCollider>();
        ZombieStats = GetComponent<CharacterStatistics>();
        anim = gameObject.GetComponent<Animator>();
        raycastLayer = 1 << LayerMask.NameToLayer("Player"); //target the player
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
            moveToTarget();

        }

        if ((timer >= timeBetweenBullet) && playerInRange && (ZombieStats.CurrentHealth > 0))
        {
            ThrowBall();
        }

    }

    void moveToTarget()
    {

        if (!isServer || ZombieStats.IsDead)
            return;

        //Looking for closest player
        DetectPlayer();

        if (targetTransform != null && isServer)
        {
            agent.SetDestination(targetTransform.position);
            anim.SetBool("PlayerInView", true);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetTransform.position - myTransform.position), Time.deltaTime * speed);
        }
        else
        {
            anim.SetBool("PlayerInView", false);
            agent.SetDestination(myTransform.position);
        }

    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, raduis, raycastLayer);
        if (hitColliders.Length != 0)
        {
            Transform joueur = hitColliders[0].gameObject.transform;
            if (hitColliders.Length > 1)
            {
                float distance_min = Vector3.Distance(myTransform.position, joueur.position);
                for (int i = 1; i < hitColliders.Length; i++)
                {
                    float current_distance = Vector3.Distance(myTransform.position, hitColliders[i].gameObject.transform.position);
                    if (current_distance < distance_min)
                    {
                        distance_min = current_distance;
                        joueur = hitColliders[i].gameObject.transform;
                    }
                }
                targetTransform = joueur;
            }
        }
        else
        {
            targetTransform = null;
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
