using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Interactable : NetworkBehaviour
{
    public float Radius = 15f;
    public bool IsFocus = false;
    [SyncVar] public bool HasInteracted = false;
    public Transform Player;
    public Transform InteractionTransform;
    private Player playerInfo;

    private void Awake()
    {
        InteractionTransform = transform;
    }

    public virtual bool Interact(Player playerInfo)
    {
        Debug.Log("Interacting with " + transform.name);
        return true;
    }

    public virtual void ToggleToolTip(Player player)
    {
        
    }
    
    private void Update()
    {
        if (IsFocus && !HasInteracted)
        {
            float distance = Vector3.Distance(Player.position, transform.position);

            if (distance <= Radius)
            {
                HasInteracted = Interact(playerInfo);
            }
        }

        if (HasInteracted)
        {
            DestroyObject(gameObject);
        }
    }

    public void OnFocused(Transform player, Player playerInfo)
    {        
        Player = player;
        IsFocus = true;
        HasInteracted = false;
        this.playerInfo = playerInfo;
    }

    public void OnDefocused()
    {        
        IsFocus = false;
        Player = null;
        HasInteracted = false;
        playerInfo = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

}
