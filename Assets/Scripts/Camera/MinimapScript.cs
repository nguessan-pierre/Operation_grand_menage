using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MinimapScript : NetworkBehaviour {

	public Transform player;

	void LateUpdate ()
	{
		Vector3 newPosition = player.position;
		newPosition.y = 10f;
		transform.position = Vector3.Lerp(transform.position, newPosition, 3 * Time.deltaTime);  
	}
}
