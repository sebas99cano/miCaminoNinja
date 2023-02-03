using UnityEngine;

[CreateAssetMenu(menuName = "Items/LifePotion")]
public class ItemLifePotion : InventoryItem
{
    [Header("Potion Information")] public float healthRestoration;

    public override bool UseItem()
    {
        if (Inventory.Instance.Character.CharacterLife.CanBeHealed)
        {
            Inventory.Instance.Character.CharacterLife.RestoreLife(healthRestoration);
            return true;
        }

        return false;
    }
}