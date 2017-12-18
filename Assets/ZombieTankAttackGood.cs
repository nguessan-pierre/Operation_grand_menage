using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;
using UnityEngine.Networking;

public class ZombieTankAttackGood : NetworkBehaviour
{
    // Animator
    Animator anim;
    // Time between attack
    public float timeBetweenAttacks = 2.0f;
    // Dommage attack
    public int attackDamage = 30;
    // Get the player
    GameObject player;
    // Get the player health
    //PlayerStatistics playerStatistics;
    // Zombie health
    CharacterStatistics ZombieStats;
    // Is a player in range
    bool playerInRange = false;
    // set a timer for attack
    float timer = 0.0f;


    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        ZombieStats = GetComponent<CharacterStatistics>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            playerInRange = true;
            Attack();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (playerInRange && (ZombieStats.CurrentHealth > 0))
        {
            anim.SetBool("PlayerInRange", playerInRange);
        }
        else
        {
            anim.SetBool("PlayerInRange", playerInRange);
        }

        if ((timer >= timeBetweenAttacks) && playerInRange && (ZombieStats.CurrentHealth > 0))
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;

        CharacterStatistics playerStatistics = player.GetComponent<CharacterStatistics>();
        playerStatistics.TakeDamage(attackDamage);
    }
}
