using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Statistics;

public class SoundManager : MonoBehaviour {

    public List<AudioSource> sounds;
    public AudioSource damage_sound;
    public AudioSource current_sound;
    public static Random Random = new Random();
    public ZombieStatistics stats;

    private void Awake()
    {
        stats = GetComponent<ZombieStatistics>();
        current_sound = null;
    }

    public void play_running()
    {
        if (!damage_sound.isPlaying && !stats.IsDead)
        {
            if(current_sound != null)
            {
                if (!current_sound.isPlaying)
                {
                    current_sound = sounds[Random.Next(sounds.Count)];
                    current_sound.Play();
                }

            } else
            {
                current_sound = sounds[Random.Next(sounds.Count)];
                current_sound.Play();
               // Debug.LogError("Im still alive");
            }

        }
    }

    public void play_hitting()
    {
        if (current_sound.isPlaying)
        {
            current_sound.Stop();
            damage_sound.Play();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        play_running();
	}
}
