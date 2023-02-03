using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Items")] [SerializeField] private InventoryItem[] itemsInventory;
    [SerializeField] private Character character;
    [SerializeField] private int numberSlots;

    public InventoryItem[] ItemsInventory => itemsInventory;
    public int NumberSlots => numberSlots;
    public Character Character => character;


    private void Start()
    {
        itemsInventory = new InventoryItem[numberSlots];
    }

    public void AddItem(InventoryItem itemToAdd, int amount)
    {
        if (itemToAdd == null)
        {
            return;
        }

        List<int> indexesItem = VerifyIfExist(itemToAdd.id);

        if (itemToAdd.isStackable)
        {
            if (indexesItem.Count > 0)
            {
                for (int i = 0; i < indexesItem.Count; i++)
                {
                    if (itemsInventory[indexesItem[i]].amount < itemToAdd.maxStack)
                    {
                        itemsInventory[indexesItem[i]].amount += amount;
                        if (itemsInventory[indexesItem[i]].amount > itemToAdd.maxStack)
                        {
                            int diff = itemsInventory[indexesItem[i]].amount - itemToAdd.maxStack;
                            itemsInventory[indexesItem[i]].amount = itemToAdd.maxStack;
                            AddItem(itemToAdd, diff);
                        }

                        InventoryUI.Instance.DrawItemInInventory(itemToAdd, itemsInventory[indexesItem[i]].amount,
                            indexesItem[i]);
                    }
                }
            }
        }

        if (amount <= 0)
        {
            return;
        }

        if (amount > itemToAdd.maxStack)
        {
            AddItemInNewSlot(itemToAdd, itemToAdd.maxStack);
            amount -= itemToAdd.maxStack;
            AddItem(itemToAdd, amount);
        }
        else
        {
            AddItemInNewSlot(itemToAdd, amount);
        }
    }

    private List<int> VerifyIfExist(string itemId)
    {
        List<int> indexesItem = new List<int>();
        for (int i = 0; i < itemsInventory.Length; i++)
        {
            if (itemsInventory[i] != null)
            {
                if (itemsInventory[i].id == itemId)
                {
                    indexesItem.Add(i);
                }
            }
        }

        return indexesItem;
    }

    private void AddItemInNewSlot(InventoryItem itemToAdd, int amount)
    {
        for (int i = 0; i < itemsInventory.Length; i++)
        {
            if (itemsInventory[i] == null)
            {
                itemsInventory[i] = itemToAdd.CopyItem();
                itemsInventory[i].amount = amount;
                InventoryUI.Instance.DrawItemInInventory(itemToAdd, amount, i);
                return;
            }
        }
    }

    private void RemoveItem(int index)
    {
        ItemsInventory[index].amount--;
        if (ItemsInventory[index].amount <= 0)
        {
            ItemsInventory[index].amount = 0;
            ItemsInventory[index] = null;
            InventoryUI.Instance.DrawItemInInventory(null, 0, index);
        }
        else
        {
            InventoryUI.Instance.DrawItemInInventory(itemsInventory[index], ItemsInventory[index].amount, index);
        }
    }

    public void MoveItem(int initialIndex, int finalIndex)
    {
        if (itemsInventory[initialIndex] == null || itemsInventory[finalIndex] != null)
        {
            return;
        }
        
        //copiamos y movemos el slot
        InventoryItem itemToMove = itemsInventory[initialIndex].CopyItem();
        itemsInventory[finalIndex] = itemToMove;
        InventoryUI.Instance.DrawItemInInventory(itemToMove, itemToMove.amount, finalIndex);

        //borramos item del slot inicial
        itemsInventory[initialIndex] = null;
        InventoryUI.Instance.DrawItemInInventory(null, 0, initialIndex);
    }

    private void UseItem(int index)
    {
        if (ItemsInventory[index] == null)
        {
            return;
        }

        if (itemsInventory[index].UseItem())
        {
            RemoveItem(index);
        }
    }

    #region Events

    private void SlotInteractionResponse(TypeOfInteraction type, int index)
    {
        switch (type)
        {
            case TypeOfInteraction.Use:
                UseItem(index);
                break;
            case TypeOfInteraction.Equip:
                break;
            case TypeOfInteraction.Remove:
                break;
        }
    }

    private void OnEnable()
    {
        InventorySlot.EventSlotInteraction += SlotInteractionResponse;
    }


    private void OnDisable()
    {
        InventorySlot.EventSlotInteraction -= SlotInteractionResponse;
    }

    #endregion
}