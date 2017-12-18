using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Areas : MonoBehaviour
{
	public string name = "New Area";
	public float timeToFree;
	public int numberOfZombiesToKill = 2;
	public int numberOfZombies;
	public int zombiesKilled = 0;
	public List<GameObject> players;
	public bool isAreaClear = false;
	public GameObject AreaClearedUI;
	public GameObject GameManager;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Collision with area");
		
		if (other.gameObject.tag.Equals("Player"))
		{
			players.Add(other.gameObject);
		} else if (other.gameObject.tag.Equals("Zombie"))
		{
			other.gameObject.GetComponent<ZombieArea>().SetArea(this);
		}
		
		UpdatePlayersUi();
	}

	public void ZombieKilled()
	{
		zombiesKilled++;
		numberOfZombies--;
		isAreaClear = zombiesKilled >= numberOfZombiesToKill;

		if (isAreaClear)
		{
			AreaClearedUI.SetActive(true);

			foreach (GameObject player in players)
			{
				Player playerInfo = player.GetComponent<Player>();
				PlayerPrefs.SetFloat("ElapsedTime", GameManager.GetComponent<Countdown>().elapsedTime);
				playerInfo.SavePlayerObject();
			}

			Invoke("SwitchToCreditsScreen", 5f);
		}
		
		UpdatePlayersUi();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			players.Remove(other.gameObject);
		} else if (other.gameObject.tag.Equals("Zombie"))
		{
			other.gameObject.GetComponent<ZombieArea>().Area = null;
			numberOfZombies--;
		}

		UpdatePlayersUi();
	}

	void UpdatePlayersUi()
	{		
		foreach (var player in players)
		{
			player.GetComponent<Player>().normalZombiesKilled = zombiesKilled;
			player.GetComponent<Player>().AreaName.text = isAreaClear ? name + " <color=#00ff00>(libérée)</color>" : name;
			player.GetComponent<Player>().AreaZombies.text = "<color=#ffffff>" + zombiesKilled + "</color>/" + numberOfZombiesToKill + " zombies";
		}
	}

	// Use this for initialization
	void Start ()
	{
		players = new List<GameObject>();
	}

	void SwitchToCreditsScreen()
	{
		
		
		SceneManager.LoadScene("Credits");
	}
}
