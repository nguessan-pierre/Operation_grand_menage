using UnityEngine;
using UnityEngine.UI;

/* The base item class. All items should derive from this. */


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";	// Name of the item
    public Sprite icon = null;				// Item icon
    public bool isDefaultItem = false;      // Is the item default wear?
    public float weight = 1f;
    public ItemType type;
    public bool carriable = true;

    // Called when the item is pressed in the inventory
    public virtual void Use (Player playerInfo)
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory (Player playerInfo)
    {
        playerInfo.PlayerInventory.Remove(this);
    }



    public string GetTypeName()
    {
        switch (type)
        {
            case ItemType.ELECTRICAL:
                return "Electronique";
                break;
            case ItemType.MARTIAL:
                return "Arts martiaux";
                break;
            case ItemType.BUILDING:
                return "Bâtiment";
                break;
            case ItemType.SPORTS:
                return "Equipement sportif";
                break;
            case ItemType.MECHANICAL:
                return "Mécanique";
                break;
            case ItemType.WEAPON:
                return "Arme";
                break;
            case ItemType.CONSUMABLE:
                return "Consommable";
                break;
            default:
                return "Quelconque";
                break;
        }
    }
    
    public void ToggleToolTip(Player player)
    {
        player.itemInfo.gameObject.SetActive(true);
        string valueColor = "00ffffff";
        foreach (Transform child in player.itemInfo.transform){
            Text text = child.GetComponent<Text>();

            switch (child.gameObject.name)
            {
                case "Name":
                    text.text = name;
                    text.color = GetTypeColor();
                    break;
                case "Carriable":
                    text.text = "Transportable : <b><color=#" + valueColor + ">" + (carriable ? "Oui" : "Non") + "</color></b>";
                    break;
                case "Weight":
                    text.text = "Poids : <b><color=#" + valueColor + ">" + weight + "</color></b>";
                    break;
                case "Icon":
                    Image icon = child.GetComponent<Image>();
                    icon.sprite = this.icon;
                    break;
            }
        }
    }

    public Color GetTypeColor()
    {
        switch (type)
        {
            case ItemType.ELECTRICAL:
                return Color.green;
                break;
            case ItemType.MARTIAL:
                return Color.magenta;
                break;
            case ItemType.BUILDING:
                return Color.yellow;
                break;
            case ItemType.SPORTS:
                return Color.red;
                break;
            case ItemType.MECHANICAL:
                return Color.blue;
                break;
            case ItemType.WEAPON:
                return Color.yellow;
                break;
            case ItemType.CONSUMABLE:
                return Color.white;
                break;
            default:
                return Color.gray;
                break;
        }
    }

    public enum ItemType
    {
        NONE, ELECTRICAL, MARTIAL, BUILDING, SPORTS, MECHANICAL, WEAPON, CONSUMABLE
    }
}