using System;
using Statistics;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerMovement : NetworkBehaviour
{
    public Interactable FocusObject;
    public float Speed = 3f;
    public float UnitSpeed = 10f;

    public GameObject craftmenu;

    public bool canMove = true;

    Vector3 movment;
    public Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    Vector3 playerToMouse;
    public CharacterStatistics Statistics;
    public Camera playerCamera;
    public Player playerInfo;

    int direction;
    int old_direction;

    readonly float camRaylenght = 100f;
    
    public Color c2 = Color.yellow;
    public Color c1 = Color.red;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 1f;
        lineRenderer.positionCount = 3;
        lineRenderer.receiveShadows = false;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 0.6f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
        Statistics = this.GetComponent<CharacterStatistics>();
    }

    void Awake()
    {
        craftmenu = PlayerShooting.getChildGameObject(gameObject, "CraftMenu");
        playerInfo = GetComponent<Player>();
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    public override void OnStartLocalPlayer()
    {
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        CameraFollow cameraFollow = playerCamera.GetComponent<CameraFollow>();
        cameraFollow.target = playerRigidbody.transform;
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            old_direction = direction;
            float h = 0f;
            float v = 0f;
            if (!craftmenu.activeSelf)
            {
                h = Input.GetAxisRaw("Horizontal");
                v = Input.GetAxisRaw("Vertical");
                Turning();
            }
            Move(h, v);           
            CalculatingDirection();
            Animating(h, v);
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        
        // Upon mouse right click.
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, camRaylenght))
            {
                // Check if we hit an interactible object.
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    SetFocus(interactable);
                }
                else
                {
                    RemoveFocus();
                }
            }
        }
        // use left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, camRaylenght))
            {
                // Check if we hit an interactible object.
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactable.ToggleToolTip(playerInfo);
                }
                else
                {
                    playerInfo.itemInfo.gameObject.SetActive(false);
                }
            }
        }
    }

    void Move(float h, float v)
    {
        if (!canMove)
            return;
        
        movment.Set(h, 0f, v);
        movment = movment.normalized * GetSpeed() * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movment);
        
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(1, transform.position);
    }

    void Turning()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRaylenght, floorMask)) {
            playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);

            float angle = AimCone.GetAngle(Statistics, GetSpeed() / 3) / 2;
            //Debug.Log("Cone angle: " + angle + ", player speed: " + GetSpeed());
            
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            Vector3 point1 = Quaternion.AngleAxis(angle, Vector3.up) * playerToMouse;
            point1.y = 0f;
            Vector3 point2 = Quaternion.AngleAxis(-angle, Vector3.up) * playerToMouse;
            point2.y = 0f;
            lineRenderer.SetPosition(0, transform.position + AimCone.lineLength * point1.normalized);
            lineRenderer.SetPosition(2, transform.position + AimCone.lineLength * point2.normalized);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
        anim.SetInteger("direction", direction);
        if (!walking)
        {
            anim.SetInteger("direction", 0);
        }
    }

    void CalculatingDirection()
    {
        Vector3 force = movment.normalized;
        Vector3 deplacement = playerToMouse.normalized;
        float produit_sclaire = Vector3.Dot(force, deplacement);
        Vector3 produit_vectoriel = Vector3.Cross(force, deplacement);
        bool sens = (produit_vectoriel.y < 0); //vraie si droite
        direction = Direction_calcule(produit_sclaire,sens);
        //Debug.Log("Direction : " + direction);
    }

    int Direction_calcule(float ps,bool sens)
    {
        /*  1 = forward
            2 = backward
            3 = left
            4 = right
        */
        if (Math.Cos(Math.PI/3) <= ps && ps <= 1)
        {
            direction = 1;
        }
        else
        {
            if (Math.Cos((Math.PI*2) / 3) <= ps && ps <= Math.Cos(Math.PI / 3))
            {
                if (sens)
                {
                    direction = 4;
                }else
                {
                    direction = 3;
                }              
            } else
            {
                direction = 2;
            }
        }
        return direction;
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != FocusObject)
        {
            if(FocusObject != null)
                FocusObject.OnDefocused();
            
            FocusObject = newFocus;
        }
        
        FocusObject.OnFocused(transform, playerInfo);
    }

    void RemoveFocus()
    {            
        if(FocusObject != null)
            FocusObject.OnDefocused();
        
        FocusObject = null;
    }

    public float GetSpeed()
    {
        return (float) ((1.025 * Statistics.Agility.GetValue() * 
                         (Statistics.Weight - playerInfo.PlayerInventory.GetWeight())) * UnitSpeed);
    }
}
