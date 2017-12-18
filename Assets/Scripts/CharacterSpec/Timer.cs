using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
        public float timeLeft;

        public bool stop = true;

        private float minutes;
        private float seconds;

        public Text text;


        public void startTimer(float from)
        {
            stop = false;
            timeLeft = from;
            Update();
            StartCoroutine(UpdateCoroutine());
        }

        void Update()
        {

            if (stop) return;
            timeLeft -= Time.deltaTime;

            minutes = Mathf.Floor(timeLeft / 60);
            seconds = timeLeft % 60;
            if (seconds > 59) seconds = 59;
            if (minutes < 0)
            {
                stop = true;
                minutes = 0;
                seconds = 0;
            }

            text.text = string.Format("{0:0}:{1:00}", minutes, seconds);

            //        fraction = (timeLeft * 100) % 100;
        }

        private IEnumerator UpdateCoroutine()
        {
            while (!stop)
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
}
