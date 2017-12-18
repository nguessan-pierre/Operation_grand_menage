using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Countdown : NetworkBehaviour {
	public float timeLeft;
	public float elapsedTime;

	public bool stop = true;
 
	private float minutes;
	private float seconds;
     
	public Text text;

	private void Start()
	{
		timeLeft = GetComponent<GlobalGameVariables>().timeLeft;
	}

	public void startTimer(float from){
		stop = false;
		timeLeft = from;
		Update();
	}
     
	void Update()
	{
		if (!isServer)
			return;
		
		if(stop) return;
		elapsedTime += Time.deltaTime;
		timeLeft -= Time.deltaTime;
         
		minutes = Mathf.Floor(timeLeft / 60);
		seconds = timeLeft % 60;
		if(seconds > 59) seconds = 59;
		if(minutes < 0) {
			stop = true;
			minutes = 0;
			seconds = 0;
		}
		
		text.text = string.Format("{0:0}:{1:00}", minutes, seconds);

		//        fraction = (timeLeft * 100) % 100;
	}
 
}