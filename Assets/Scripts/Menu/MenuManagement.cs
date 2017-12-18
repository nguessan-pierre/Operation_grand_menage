using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour {

    public GameObject menuSelection;
    public GameObject menuJouer;
    public GameObject menuOptions;
    public GameObject menuRecherchePartie;
    public GameObject menuCreationpartie;

    public AudioSource clickSound;

    public GameObject menu_actif;

    private void Awake()
    {
       /* menuSelection = GameObject.Find("MenuSelection");
        menuJouer = GameObject.Find("MenuJouer");
        menuOptions = GameObject.Find("MenuOptions");
        menuRecherchePartie = GameObject.Find("MenuRecherchePartie"); */

        clickSound = GetComponent<AudioSource>();

        menu_actif = menuSelection;
    }


    public void MenuSelectionActive()
    {
        menu_actif.SetActive(false);

        menuSelection.SetActive(true);

        menu_actif = menuSelection;

        ButtonClicked();
    }


    public void MenuJouerActive()
    {
        //menu_actif.SetActive(false);

        //menuJouer.SetActive(true);

        //menu_actif = menuJouer;
        
        ButtonClicked();
        Invoke("GoToLobby", 1f);
    }

    private void GoToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void MenuOptionActive()
    {
        menu_actif.SetActive(false);

        menuOptions.SetActive(true);

        menu_actif = menuOptions;

        ButtonClicked();
    }

    public void MenuRecherchePartieActive()
    {
        menu_actif.SetActive(false);

        menuRecherchePartie.SetActive(true);

        menu_actif = menuRecherchePartie;

        ButtonClicked();
    }

    public void MenuCreationPartieActive()
    {
        menu_actif.SetActive(false);

        menuCreationpartie.SetActive(true);

        menu_actif = menuCreationpartie;

        ButtonClicked();
    }

    void ButtonClicked()
    {
        clickSound.Play();
    }
    
}
