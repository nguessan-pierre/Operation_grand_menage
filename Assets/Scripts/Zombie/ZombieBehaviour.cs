using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour {

	// Set the player
    GameObject player;
	
	// Navigation to the player
    NavMeshAgent nav;
	
	// Animator for changing animation
    Animator anim;

	// Distance between a zombie and a player before detecting
    float distanceDetect = 4.0f;
	
	// Is the player detected ?
    bool detect;

    void Awake()
    {
        // To-Do faire une boucle pour tous les player crée

        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        CalculDist();
    }

    private void CalculDist()
    {
		float sqrLen = (player.transform.position - transform.position).sqrMagnitude;
        if(sqrLen < distanceDetect * distanceDetect)
        {

			// Active navMeshAgent
            nav.SetDestination(player.transform.position);
			//Change animation
            anim.SetBool("PlayerInView", true);
        }
           
    }

}
