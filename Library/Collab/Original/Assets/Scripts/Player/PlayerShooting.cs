using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

    public IWeapon currentWeapon;
    private bool isShooting;
    //bool isAnimationWorking;
    public Animator anim;
    float timer;                                    // A timer to determine when to fire.
    private EquipmentManager _equipmentManager;
    private Player playerInfo;
    public int number_of_layer;
    public CharacterStatistics Statistics;
    public PlayerMovement PlayerMovement;
	public bool canFire;

    public GameObject craftmenu;



    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true); 
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    private void Awake()
    {
        Statistics = this.GetComponent<CharacterStatistics>();
        PlayerMovement = this.GetComponent<PlayerMovement>();
        craftmenu = getChildGameObject(gameObject, "CraftMenu");
        playerInfo = GetComponent<Player>();
        _equipmentManager = GetComponent<EquipmentManager>();
        isShooting = false;
        currentWeapon = (IWeapon)getChildGameObject(gameObject, "akm_end").GetComponent<AKMShooting>();
        number_of_layer = 3;
        //isAnimationWorking = false;
		canFire=false;
    }

    // Update is called once per frame
    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        bool attack = Input.GetButton("Fire1") && (!craftmenu.activeSelf);
        bool canFire = (_equipmentManager.CanFire() && currentWeapon.IsAmmoNeeded());
        
        if(!attack || !canFire)
        anim.SetBool("attack", false);

        // If the Fire1 button is being press and it's time to fire...
        if (isLocalPlayer && canFire && attack && timer >= currentWeapon.getTimeBetweenBullet())
        {
            // ... shoot the gun  and start animation
            anim.SetBool("attack", true);
            //isAnimationWorking = true;
            if (anim.GetLayerWeight(currentWeapon.GetLayerNumber()) != 1.0f){ // if not active yet

                Debug.LogError("Je passe ici");
                resetWeight();
                anim.SetLayerWeight(currentWeapon.GetLayerNumber(), 1.0f);

            }
            isShooting = true;
            currentWeapon.SetCanHit(isShooting);
        } else if (attack && !canFire)
        {
            Debug.LogError("Not enough ammo!");
            playerInfo.DisplayMessage("error", "Pas assez de munitions.");
        }

        if (anim.GetCurrentAnimatorStateInfo(currentWeapon.GetLayerNumber()).IsName(currentWeapon.GetLayerName()) && anim.GetCurrentAnimatorStateInfo(currentWeapon.GetLayerNumber()).normalizedTime > currentWeapon.GetFiringTime() && isShooting && canFire) // Firing during the animation
        {
            timer = 0f;
            float angle = AimCone.GetAngle(Statistics, PlayerMovement.GetSpeed() / 3) / 2;
            currentWeapon.Fire(angle);
            isShooting = false;
            _equipmentManager.RemoveAmmo();
        }

       /* if(anim.GetCurrentAnimatorStateInfo(currentWeapon.GetLayerNumber()).IsName(currentWeapon.GetLayerName()) && anim.GetCurrentAnimatorStateInfo(currentWeapon.GetLayerNumber()).normalizedTime > .99f && isAnimationWorking ) // reset timer when animation is finished
        {
            isAnimationWorking = false;
            timer = 0f;
        }*/

        if (timer >= currentWeapon.getTimeBetweenBullet() * currentWeapon.getEffectsDisplayTime())
        {
            // ... disable the effects.
            currentWeapon.DisableEffects();
        }
    }

    public void resetWeight()
    {
        Debug.LogError("Je passe ici 2");
        int i;
        for (i = 1; i <= number_of_layer; i++)
        {
            if (i != currentWeapon.GetLayerNumber())
            {
                anim.SetLayerWeight(i, 0f);
                Debug.LogError("Layer" + i + "Weight : 0");
            }
        }
    }

    public void UpdateLayer()
    {
        if (anim.GetLayerWeight(currentWeapon.GetLayerNumber()) != 1.0f)
        { // if not active yet

            Debug.LogError("Je passe ici");
            resetWeight();
            anim.SetLayerWeight(currentWeapon.GetLayerNumber(), 1.0f);

        }
    }


}

