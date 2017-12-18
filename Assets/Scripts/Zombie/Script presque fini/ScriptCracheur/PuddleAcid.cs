using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;

public class PuddleAcid : MonoBehaviour {

    // damage per time
    public int damagePerTime = 1;
    // time between hit
    public float timeBetweenHit = 0.2f;
    // Is the player in the puddle ?
    bool playerInPuddle = false;

    // Get the player
    GameObject player;

    // general timer to know the time between two update
    float timer;

    // Use this for initialization
    void Start () {
        // destory the bullet 2 sec after the shoot 
        Destroy(this.gameObject, 2);
    }
	
	// Update is called once per frame
	void Update () {

        // we update the timer
        timer += Time.deltaTime;

        if (playerInPuddle && (timer >= timeBetweenHit))
        {
            // then we shoot
            HurtPuddle();
        }

    }

    void HurtPuddle()
    {
        CharacterStatistics playerStatistics = player.GetComponent<CharacterStatistics>();
        playerStatistics.TakeDamage(damagePerTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            playerInPuddle = true;
        }
        
    }
}
