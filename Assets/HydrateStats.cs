using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrateStats : MonoBehaviour
{
	public GameObject playerObject;
	public Text elapsedTime;
	public Text totalZombies;
	public Text normalZombies;
	public Text timesFired;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindGameObjectWithTag("Player");
		
		PlayerShooting.getChildGameObject(playerObject, "HUDCanvas").SetActive(false);
		playerObject.GetComponent<PlayerShooting>().enabled = false;
		playerObject.GetComponent<PlayerMovement>().enabled = false;

		elapsedTime.text = PlayerPrefs.GetFloat("ElapsedTime").ToString();
		normalZombies.text = playerObject.GetComponent<Player>().normalZombiesKilled.ToString();
		totalZombies.text = playerObject.GetComponent<Player>().normalZombiesKilled.ToString();
		timesFired.text = playerObject.GetComponent<Player>().timesFired.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
