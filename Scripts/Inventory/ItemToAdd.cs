using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToAdd : MonoBehaviour
{
    [Header("Configuration")] [SerializeField]
    private InventoryItem inventoryItem;

    [SerializeField] private int amountToAdd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(inventoryItem, amountToAdd);
            Destroy(gameObject);
        }
    }
}