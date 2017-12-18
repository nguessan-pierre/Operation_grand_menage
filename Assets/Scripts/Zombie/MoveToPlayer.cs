using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class MoveToPlayer : NetworkBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private Transform targetTransform; //the zombie's target
    private Transform myTransform; //current transform data of this zombie

    private float raduis = 100;
    private LayerMask raycastLayer;
    CharacterStatistics ZombieStats;
	private float sqrlen;
	public float distanceDetect = 4.0f;
	private float speed = 0.1f;


    // Use this for initialization
    void Start()
    {
        ZombieStats = GetComponent<CharacterStatistics>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        myTransform = transform; //cache transform data for easy access/preformance
        raycastLayer = 1 << LayerMask.NameToLayer("Player"); //target the player
		anim.SetBool("PlayerInView", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveToTarget();
        if(targetTransform != null)
        {
            // and in your Update:
            //anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
        }
    }

    void moveToTarget()
    {
		/*if (targetTransform) {
			sqrlen = (targetTransform.position - myTransform.position).sqrMagnitude;
			if (sqrlen < distanceDetect * distanceDetect) {
				anim.SetBool ("PlayerInView", true);
			} else {
				anim.SetBool ("PlayerInView", false);
			}
		}*/
		if (!isServer || ZombieStats.IsDead)
			return;

        DetectPlayer();

        if (targetTransform == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, raduis, raycastLayer);

            if (hitColliders.Length > 0)
            {
                int randomint = Random.Range(0, hitColliders.Length);
                targetTransform = hitColliders[randomint].transform;
            }
        }

        if(targetTransform != null && isServer)
        {
            //agent.Warp(myTransform.position);
            agent.SetDestination(targetTransform.position);
            anim.SetBool("PlayerInView", true);
            //anim.SetBool("PlayerInRange", true);
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetTransform.position-myTransform.position),Time.deltaTime*speed);
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