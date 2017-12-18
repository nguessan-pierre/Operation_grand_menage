using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Statistics;
using Random = System.Random;

public class GatlingShooting : MonoBehaviour, IWeapon {

    // Use this for initialization
    public int damage;
    public float timeBetweenBullets;       // depend de l'animation;


    private bool isShooting;

    public float range;                      // The distance the gun can fire.

    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    public ParticleSystem gunParticles;                    // Reference to the particle system.
    public LineRenderer gunLine;                           // Reference to the line renderer.
    public AudioSource gunAudio;                           // Reference to the audio source.
    public Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

    public static Random Random = new Random();


    // Animation
    Animation anim_weapon;
    string layer_name = "Shooting_gatling_layer.Shoot";
    int layer_number = 2;
    float firing_time;

    void Awake()
    {
        damage = 50;
        range = 100f;
        timeBetweenBullets = 0.0534f;
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        isShooting = false;
        anim_weapon = gameObject.transform.parent.gameObject.GetComponent<Animation>();
        firing_time = .99f;
    }




    public void DisableEffects(bool t)
    {
        if (t)
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
            gunLight.enabled = false;
        }
    }

    public void Fire(float angle)
    {
        Debug.LogError("Time between bullet" + timeBetweenBullets);
        anim_weapon.Play();

        Debug.LogError("Je tire Gatling");
        gunAudio.Play();

        // Enable the light.
        //gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        //TO DO : A modifier en fonction de langle
        //shootRay.direction = transform.forward;

        float random = (float)(Random.NextDouble() * (2) - 1);

        Vector3 point = Quaternion.AngleAxis(random * angle, Vector3.up) * transform.forward;
        point.y = 0f;

        shootRay.direction = point;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Enemmy interaction
            // Try and find an EnemyHealth script on the gameobject hit.
            if (shootHit.collider.CompareTag("Zombie"))
            {
                CharacterStatistics zombieStatistics = shootHit.collider.GetComponent<CharacterStatistics>();
                zombieStatistics.TakeDamage(damage);

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootHit.point);
            }
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    public float getEffectsDisplayTime()
    {
        return effectsDisplayTime;
    }

    public float getTimeBetweenBullet()
    {
        return timeBetweenBullets;
    }

    public bool IsAmmoNoNeeded()
    {
        return false; 
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetLayerName()
    {
        return layer_name;
    }

    public int GetLayerNumber()
    {
        return layer_number;
    }

    public float GetFiringTime()
    {
        return firing_time;
    }

    public  void SetCanHit(bool can)
    {

    }
}

