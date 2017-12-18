using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;
using UnityEngine.Networking;

public class BallScript : NetworkBehaviour
{

    // damage per bullet
    public int damagePerShot = 15;

    // Impact Particule
    public GameObject particleExplosion;

    // Puddle
    public GameObject puddle;

    // Get the player
    GameObject player;

    private bool hasbeentouched;


    // Use this for initialization
    void Start()
    {
        // destory the bullet 4 sec after the shoot 
        Destroy(this.gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider other)
    {
        //print(other.tag);
        if (other.tag == "Zombie")
        {
            // don't destroy it when it's the zombie shooting (or the bullet will never go out of the zombie collider...)
        }
        else if (other.tag == "Player" /*&& other.isTrigger*/)
        {
            player = other.gameObject;
            hasbeentouched = true;
            if (hasbeentouched)
            {
                // Player take dommage
                Hurt();
                // Then, we destroy the ball
                Destroy(this.gameObject);
            }
            // don't destroy if it impacts the trigger collider of an enemy (EnemyFOV)
        }
        else if (other.tag == "floor")
        {
            Destroy(this.gameObject);
            Quaternion flaque = new Quaternion(0, 0, 0, 0);
            Vector3 posFlaque = transform.position;
            posFlaque.y = 0;
            GameObject puddleacid = (GameObject)Instantiate(puddle, posFlaque, flaque);

        }

        /*else
        {
            // in all other case destroy object and add particleSystem
            Destroy(this.gameObject);
            GameObject boom = (GameObject)Instantiate(particleExplosion, transform.position, Quaternion.identity);
        }*/
    }

    void Hurt()
    {
        CharacterStatistics playerStatistics = player.GetComponent<CharacterStatistics>();
        playerStatistics.TakeDamage(damagePerShot);
    }
    

}