using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour {

    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.


    Animator anim;                              // Reference to the animator.
    AudioSource enemyAudio;                     // Reference to the audio source.
    //CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.


    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        //capsuleCollider = GetComponent<CapsuleCollider>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {

    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Play the hurt sound effect.
        enemyAudio.Play();

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

        // Turn the collider into a trigger so shots can pass through it.
        //capsuleCollider.isTrigger = true;

        // Tell the animator that the enemy is dead.
        anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }

}