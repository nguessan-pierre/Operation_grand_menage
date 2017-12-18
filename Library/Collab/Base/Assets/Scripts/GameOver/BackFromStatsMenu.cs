using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFromStatsMenu : MonoBehaviour {
    public GameObject textGameOver;
    public GameObject Quit;
    public GameObject Menu;
    public GameObject Stats;

    public GameObject StatsPlayer;

    public void StatsMenuDesactive()
    {
        textGameOver.SetActive(true);
        Quit.SetActive(true);
        Menu.SetActive(true);
        Stats.SetActive(true);

        StatsPlayer.SetActive(false);
    }

}
