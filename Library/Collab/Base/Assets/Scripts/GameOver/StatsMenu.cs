using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsMenu : MonoBehaviour {

    public GameObject textGameOver;
    public GameObject Quit;
    public GameObject Menu;
    public GameObject Stats;

    public GameObject StatsPlayer;

    public void StatsMenuActive()
    {
        textGameOver.SetActive(false);
        Quit.SetActive(false);
        Menu.SetActive(false);
        Stats.SetActive(false);

        StatsPlayer.SetActive(true);
    }

}
