using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class ZombieTankHealth : NetworkBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    Animator anim;                              // Reference to the animator.
    public bool isDead;                                // Whether the enemy is dead.

    // Use this for initialization
    void Start()
    {
        // Setting up the animator.
        anim = GetComponent<Animator>();
        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void DeductHealth(int amount)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }

    void Death()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;
        // The enemy is dead.
        isDead = true;
        // Tell the animator that the enemy is dead.
        anim.SetTrigger("Dead");
        // After 2 seconds destory the enemy.
        Destroy(gameObject, 4f);
    }
}