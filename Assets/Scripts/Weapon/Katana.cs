using Statistics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour, IWeapon {

    public int damage;
    public float time_between_hit;
    public int layer_number;
    float effectsDisplayTime;
    string layer_name;
    AudioSource katana;
    float firingtime;
    string name_player;

    bool canHit;

    GameObject owner;


    void Awake()
    {
        damage = 50;
        time_between_hit = 2.267f / 2.0f;
        layer_number = 3;
        effectsDisplayTime = 0;
        layer_name = "Melee_layer.Shoot";
        katana = GetComponent<AudioSource>();
        firingtime = .4f;
        canHit = false;
        owner = gameObject.transform.root.gameObject;
        name_player = owner.GetComponent<CharacterStatistics>().name;
    }

    public void DisableEffects(bool t )
    {
        
    }

    public void Fire(float angle)
    {
        katana.Play();


    }

    public void OnTriggerEnter(Collider other)
    {
        if (canHit) 
        {
            if (other.gameObject.tag == "Zombie")
            {
                CharacterStatistics zombieStatistics = other.GetComponent<CharacterStatistics>();
                zombieStatistics.TakeDamage(damage);
                if(name_player == "Gaara")
                {
                    Debug.LogError("Je passe ici");
                    owner.GetComponent<Gaara>().AddFureur(40);
                }
                Debug.LogError("J'attaque au Katana");
                canHit = false;
            }
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
}
