using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class NavMeshTest : NetworkBehaviour
{
    GameObject player;
    public Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);
        // Active navMeshAgent
        agent.SetDestination(player.transform.position);

    }

    public void GetPositionPlayer(Transform positionPlayer)
    {
        target = positionPlayer;
    }
}
