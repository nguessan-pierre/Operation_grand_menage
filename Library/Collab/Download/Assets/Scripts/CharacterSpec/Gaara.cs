using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gaara : MonoBehaviour {

    public int current_fureur;
    public int fureur_max;
    public Slider Slider_fureur;
    public Text Furreur_text;

    bool isUtilamteactive;
    float timer;
    float ultimate_timing;

    Animator anim;

    int speedChange;
    int healing;

    AudioSource ultimate_song;

    Katana ScriptKata;

    GameObject Timer_ultimate;

    private void Awake()
    {
        current_fureur = 0;
        fureur_max = 40;
        isUtilamteactive = false;
        ultimate_timing = 20;
        anim = GetComponent<Animator>();

        speedChange = 2;
        healing = 200;

        Slider_fureur.maxValue = fureur_max;
        Slider_fureur.minValue = current_fureur;
        Slider_fureur.value = current_fureur;

        ultimate_song = GetComponent<AudioSource>();

        ScriptKata = PlayerShooting.getChildGameObject(gameObject, "Katana").GetComponent<Katana>();

        Timer_ultimate = PlayerShooting.getChildGameObject(gameObject, "Timer");
        Timer_ultimate.SetActive(false);
    }


    public void AddFureur(int fur)
    {
        if (!isUtilamteactive && (current_fureur<fureur_max))
        {
            current_fureur += fur;
            UpdateUiFUrreur();
        }
    }



    void DisableUltimate()
    {
        ChangeSpeed(-speedChange);

        Timer_ultimate.SetActive(false);

        ultimate_song.Stop();
    }

    public void ActivateUltimate()
    {
        ChangeSpeed(speedChange);
        AddHealth(healing);

        Timer_ultimate.SetActive(true);
        Timer_ultimate.GetComponent<Timer>().startTimer(ultimate_timing);

        ultimate_song.Play();
        
    }


    public void AddHealth(int healt)
    {
        GetComponent<PlayerMovement>().Statistics.Heal(healt);
    }

    public void TimerState(bool active)
    {
        if (active)
        {

        }
    }

    public void ChangeSpeed(int speed)
    {

        if(speed> 0)
        {
            //Change speed of anmation 
            anim.speed = speed;

            //Change speed of player
            GetComponent<PlayerMovement>().UnitSpeed = GetComponent<PlayerMovement>().UnitSpeed * speed;

            ScriptKata.time_between_hit = ScriptKata.time_between_hit / speed;
        } else
        {
            //Change speed of anmation 
            anim.speed = 1;

            //Change speed of player
            GetComponent<PlayerMovement>().UnitSpeed = GetComponent<PlayerMovement>().UnitSpeed / (-speed);

            ScriptKata.time_between_hit = ScriptKata.time_between_hit * speed;
        }
    }

    void UpdateUiFUrreur()
    {
        Slider_fureur.value = current_fureur;
        Debug.Log("Updating slider with value: " + current_fureur + "/" + Slider_fureur.maxValue);
        Furreur_text.text = current_fureur + " / " + fureur_max;
    }
	
	// Update is called once per frame
	void Update () {
        if (isUtilamteactive)
        {
            timer += Time.deltaTime;


            if(timer >= ultimate_timing)
            {
                DisableUltimate();
                isUtilamteactive = false;
                timer = 0f;
            }
        }

        if(!isUtilamteactive && Input.GetKeyDown(KeyCode.R) && (current_fureur>= fureur_max))
        {
            ActivateUltimate();
            isUtilamteactive = true;
            current_fureur = 0;
            UpdateUiFUrreur();
        }

    }
}
