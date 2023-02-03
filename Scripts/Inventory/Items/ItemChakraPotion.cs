using UnityEngine;

[CreateAssetMenu(menuName = "Items/ChakraPotion")]
public class ItemChakraPotion : InventoryItem
{
    [Header("Potion Information")] public float chakraRestoration;

    public override bool UseItem()
    {
        if (Inventory.Instance.Character.CharacterChakra.CanBeRestoreChakra)
        {
            Inventory.Instance.Character.CharacterChakra.RestoreChakra(chakraRestoration);
            return true;
        }

        return false;
    }
}