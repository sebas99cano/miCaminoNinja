using UnityEngine;


public enum ItemType
{
    Weapons,
    Potions,
    Justsus,
    Materials
}

public class InventoryItem : ScriptableObject
{
    public Transform parentAfterDrag;

    [Header("Params")] public string id;
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;

    [Header("Information")] public ItemType type;
    public bool isConsumable;
    public bool isStackable;
    public int maxStack;

    [HideInInspector] public int amount;

    public InventoryItem CopyItem()
    {
        InventoryItem newItem = Instantiate(this);
        return newItem;
    }

    public virtual bool UseItem()
    {
        return true;
    }

    public virtual bool EquipItem()
    {
        return true;
    }

    public virtual bool RemoveItem()
    {
        return true;
    }
}