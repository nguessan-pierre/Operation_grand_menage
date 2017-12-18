using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class PlayersManager : NetworkManager
{
	public GameObject[] playerSpawns;
	public List<Transform> playersList;
	public Text playersText;
	
	public int characterID;
	
	void Start() {
		playersList = new List<Transform>();
		playerSpawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");
	}
     
	//Called on client when connect
	public override void OnClientConnect(NetworkConnection conn) {       
 
		// Create message to set the player
		IntegerMessage msg = new IntegerMessage(characterID);      
  
		// Call Add player and pass the message
		ClientScene.AddPlayer(conn,0, msg);
	}
  
	// Server
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader ) { 
		// Read client message and receive index
		if (extraMessageReader != null) {
			var stream = extraMessageReader.ReadMessage<IntegerMessage> ();
			characterID = stream.value;
		}
		
		//Select the prefab from the spawnable objects list
		var playerPrefab = spawnPrefabs[characterID];       
  
		// Create player object with prefab
		var player = Instantiate(playerPrefab, GetSpawnPosition(), Quaternion.identity) as GameObject;        
         
		// Add player object for connection
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

	private Vector3 GetSpawnPosition()
	{
		if (playerSpawns.Length > 0)
		{
			// try to spawn at a random start location
			int index = Random.Range(0, playerSpawns.Length);
			return playerSpawns[index].transform.position;
		}
		
		return new Vector3();
	}
     
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Debug.Log("OnPlayerDisconnected guid " + player.guid);
		Transform playerTransform = GameObject.Find("Player_" + player.guid).transform;

		playersList.Remove(playerTransform);
		
		if (playerTransform != null)
			Destroy(playerTransform.gameObject);        
   
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
}
