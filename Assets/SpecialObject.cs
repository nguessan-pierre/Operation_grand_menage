using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpecialObject : NetworkBehaviour
{
    public Item ResultItem;
    public ItemPickup[] SideItems;
    public float RequiredTime;
    public TextMesh Timer;
    public GameObject player;

    public MeshRenderer RedCircle;
    public MeshRenderer BlueCircle;
    public MeshRenderer GreenCircle;
    
    public bool stop = true;
    public bool used = false;
 
    private float minutes;
    private float seconds;

    private void Start()
    {
        Timer.text = ResultItem.name;
    }

    public void startTimer(float from){
        stop = false;
        RequiredTime = from;
        Update();
    }
     
    void Update()
    {
        if(stop) return;
        RequiredTime -= Time.deltaTime;
         
        minutes = Mathf.Floor(RequiredTime / 60);
        seconds = RequiredTime % 60;
        if(seconds > 59) seconds = 59;
        if(minutes < 0) {
            stop = true;
            minutes = 0;
            seconds = 0;
        }

        if (!used && stop)
        {
            BlueCircle.enabled = false;
            GreenCircle.enabled = true;

            EnablePlayer();
            GiveItem();
            used = true;
        }
		
        Timer.text = ResultItem.name + "\n" + string.Format("{0:0}:{1:00}", minutes, seconds);

        //        fraction = (timeLeft * 100) % 100;
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (used)
            return;
        
        if (other.gameObject.tag.Equals("Player"))
        {
            player = other.gameObject;
            DisablePlayer();
            RedCircle.enabled = false;
            BlueCircle.enabled = true;
            Timer.color = Color.green;
            stop = false;
            startTimer(RequiredTime);
        }
    }

    private void DisablePlayer()
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.canMove = false;
        
        PlayerShooting shooting = player.GetComponent<PlayerShooting>();
        shooting.enabled = false;
    }

    private void GiveItem()
    {
        Inventory inventory = player.GetComponent<Inventory>();
            inventory.Add(ResultItem, 1);

        foreach (ItemPickup item in SideItems)
        {
            inventory.Add(item.item, item.amount);
        }
        
        player.GetComponent<Player>().DisplayMessage("success", "Vous avez reçu l'objet " + ResultItem.name + " !");
    }

    private void EnablePlayer()
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.canMove = true;


        PlayerShooting shooting = player.GetComponent<PlayerShooting>();
        shooting.enabled = true;
    }
}
