using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour {
    public Image damageImage; 
    public Slider healthSlider;
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    public Text healthText;
    public PlayerMovement PlayerMovement;

    private void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerMovement.Statistics.RpcOnHealthChangedCallback += RpcOnHealthChangedCallback;
        healthSlider.maxValue = PlayerMovement.Statistics.MaximumHealth;
        healthSlider.value = PlayerMovement.Statistics.MaximumHealth;
        healthText.text = PlayerMovement.Statistics.MaximumHealth + " / " + PlayerMovement.Statistics.MaximumHealth;
    }

    void Update ()
    {
        damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
    }

    [ClientRpc]
    public void RpcOnHealthChangedCallback(int currentHealth, int formerHealth)
    {
        healthSlider.value = currentHealth;
        Debug.Log("Updating slider with value: " + currentHealth + "/" + healthSlider.maxValue);
        healthText.text = currentHealth + " / " + PlayerMovement.Statistics.MaximumHealth;

        if (currentHealth < formerHealth)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        
    }
}