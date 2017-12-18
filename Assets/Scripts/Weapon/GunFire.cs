using Statistics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour, IWeapon {

    public int damage;
    public float time_between_hit;
    public int layer_number;
    float effectsDisplayTime;
    string layer_name;
    float firingtime;
    string name_player;
    GameObject owner;

    ParticleSystem flamme;
    AudioSource gunfire_song;

    bool canHit;





    public void Awake()
    {
        damage = 30;
        time_between_hit = 0.2f;
        layer_number = 1;
        effectsDisplayTime = 0;
        layer_name = "Shooting_akm_layer.Shoot";
        firingtime = .99f;
        canHit = false;
        owner = gameObject.transform.root.gameObject;
        flamme = GetComponent<ParticleSystem>();
        gunfire_song = GetComponent<AudioSource>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (canHit)
        {
            if (other.gameObject.tag == "Zombie")
            {
                CharacterStatistics zombieStatistics = other.GetComponent<CharacterStatistics>();
                zombieStatistics.TakeDamage(damage);
                Debug.LogError("J'attaque au Lance Flamme");
                canHit = false;
            }
        }
    }

    public void DisableEffects(bool t)
    {
        if (!t)
        {
            flamme.Stop();
            gunfire_song.Stop();
        }
    }

    public void Fire(float angle)
    {
        if (!flamme.isPlaying)
        {
            flamme.Play();
        }
        if (!gunfire_song.isPlaying)
        {
            gunfire_song.Play();
        }
    }

    public float getEffectsDisplayTime()
    {
        return effectsDisplayTime;
    }

    public string GetLayerName()
    {
        return layer_name;
    }

    public int GetLayerNumber()
    {
        return layer_number;
    }

    public float getTimeBetweenBullet()
    {
        return time_between_hit;
    }

    public bool IsAmmoNoNeeded()
    {
        return true;
    }

    public float GetFiringTime()
    {
        return firingtime;
    }

    public void SetCanHit(bool can)
    {
        canHit = can;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
