using Statistics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class IANormal : NetworkBehaviour
{

    private Animator anim;
    private NavMeshAgent agent;
    private Transform targetTransform; //the zombie's target
    private Transform myTransform; //current transform data of this zombie

    private float raduis = 100;
    private LayerMask raycastLayer;
    CharacterStatistics ZombieStats;
    private float sqrlen;
    public float distanceDetect;
    private float speed = 0.1f;


    // Use this for initialization
    void Start()
    {
        ZombieStats = GetComponent<CharacterStatistics>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        distanceDetect = 4.0f;
        myTransform = transform; //cache transform data for easy access/preformance
        raycastLayer = 1 << LayerMask.NameToLayer("Player"); //target the player
        anim.SetBool("PlayerInView", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveToTarget();
        if (targetTransform != null)
        {
            // and in your Update:
            //anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
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
            //agent.Warp(myTransform.position);
            agent.SetDestination(targetTransform.position);
            //anim.SetBool("PlayerInRange", true);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetTransform.position - myTransform.position), Time.deltaTime * speed);
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
}
