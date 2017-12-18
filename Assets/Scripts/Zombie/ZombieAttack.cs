using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttack : MonoBehaviour {

    public AudioClip attackClip;
    AudioSource zombieAudio;                     // Reference to the audio source.

    // Time between attack
    public float timeBetweenAttacks = 0.5f;
	
	// Dommage attack
    public int attackDamage = 10;

	GameObject player;
	PlayerHealth playerHealth;
	ZombieHealth enemyHealth;
	Animator anim;
	bool playerInRange=false;

    // Timer for Attacking
	float timer=0.0f;
    // Is the player detected
	bool detect;

	void Awake()
	{
        // To-Do faire une boucle pour tous les player crée
        zombieAudio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<ZombieHealth>();
		anim = gameObject.GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerInRange = true;
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerInRange = false;
		}
	}

	void Update()
	{
		timer += Time.deltaTime;

		anim.SetBool ("PlayerInRange", playerInRange);

		if ((timer >= timeBetweenAttacks) && playerInRange && (enemyHealth.currentHealth > 0))
		{
			Attack();

            // Change the audio clip of the audio source to the attack clip and play it (this will stop the hurt clip playing).
            zombieAudio.clip = attackClip;
            zombieAudio.Play();
        }
	}

	void Attack()
	{
		/*timer = 0f;

		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage);
		}*/
	}


}
