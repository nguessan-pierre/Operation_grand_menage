using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {
	public Quaternion rotation;
	void Awake()
	{
		rotation = transform.rotation;
	}

	private void Update()
	{
		transform.rotation = rotation;
	}
}
