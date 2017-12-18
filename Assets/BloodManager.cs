using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BloodManager : MonoBehaviour
{
	public ParticleSystem blood;
	public ParticleSystem smoke;
	public ParticleSystem deathBlood;
	public ParticleSystem deathBloodSub;
	public List<AudioSource> sounds;
	public AudioSource deathSound;
	public static Random Random = new Random();
	
	public void Play()
	{
		Debug.Log("Playing bleeding animation...");
		blood.Play();
		smoke.Play();
		
		sounds[Random.Next(sounds.Count)].Play();
	}

	public void PlayDeath()
	{
		deathSound.Play();
		deathBlood.Play();
		deathBloodSub.Play();
	}
}
