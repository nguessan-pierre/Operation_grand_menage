using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStatistics : CharacterStatistics {


    public override void Die()
	{
		Debug.Log(name + " is dead.");
		GetComponent<ZombieArea>().Kill();
		// Find and disable the Nav Mesh Agent.
		GetComponent<NavMeshAgent>().enabled = false;
		// The enemy is dead.
		IsDead = true;
		// Tell the animator that the enemy is dead.
		anim.SetTrigger("Dead");
        GetComponent<CapsuleCollider>().isTrigger = true;
		BloodManager.PlayDeath();
		// After 2 seconds destory the enemy.
		Destroy(gameObject, GlobalVariables.DeadBodyLifetime);
	}
}
