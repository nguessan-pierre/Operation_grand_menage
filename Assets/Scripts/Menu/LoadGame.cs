using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour {


    //General parameter
    public float f_d;


    //Dropdown difficulty
    public int valueDifficulty;
    public Dropdown dropDifficulty;

    private void Awake()
    {
        f_d = 0.75f;
    }

    public void onValueChangeDifficulty()
    {
        valueDifficulty = dropDifficulty.value;
        // 0 facile; 1 normal; 2 difficle; 3 hardcore; 4 impossible

        switch (valueDifficulty)
        {
            case 0:
                f_d = 0.75f;
                break;

            case 1:
                f_d = 1.00f;
                break;
            case 2:
                f_d = 1.10f;
                break;
            case 3:
                f_d = 1.25f;
                break;
            case 4:
                f_d = 1.50f;
                break;
        }


    }




    public void onClickCreate()
    {
        GlobalGameVariables.f_d = f_d; //put f_d
        Application.LoadLevel("CharacterSelection");
    }


}
