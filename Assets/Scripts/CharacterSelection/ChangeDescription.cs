using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDescription : MonoBehaviour {

    //private int selectionIndex = 0;

    public GameObject swayne;
    public GameObject gaara;
    public GameObject tyler;
    public GameObject eduardo;
    public GameObject alberto;
    

    public void ChangeDescriptionFunction(int selectionIndex)
    {
        switch (selectionIndex)
        {
            case 0:
                swayne.SetActive(true);
                gaara.SetActive(false);
                tyler.SetActive(false);
                eduardo.SetActive(false);
                alberto.SetActive(false);
                break;
            case 1:
                swayne.SetActive(false);
                gaara.SetActive(true);
                tyler.SetActive(false);
                eduardo.SetActive(false);
                alberto.SetActive(false);
                break;
            case 2:
                swayne.SetActive(false);
                gaara.SetActive(false);
                tyler.SetActive(true);
                eduardo.SetActive(false);
                alberto.SetActive(false);
                break;
            case 3:
                swayne.SetActive(false);
                gaara.SetActive(false);
                tyler.SetActive(false);
                eduardo.SetActive(true);
                alberto.SetActive(false);
                break;
            case 4:
                swayne.SetActive(false);
                gaara.SetActive(false);
                tyler.SetActive(false);
                eduardo.SetActive(true);
                alberto.SetActive(false);
                break;
        }
        
    }
}
