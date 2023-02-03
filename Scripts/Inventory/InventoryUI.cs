using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("InventoryPanelDescription")] [SerializeField]
    private GameObject panelInventoryDescription;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private InventorySlot slotPrefab;

    [SerializeField] private Transform container;

    public int indexSlotToMove { get; set; }
    public InventorySlot selectedSlot { get; private set; }
    private List<InventorySlot> _availableSlots = new List<InventorySlot>();

    void Start()
    {
        InitializeInventory();
        indexSlotToMove = -1;
    }

    private void Update()
    {
        UpdateSelectedSlot();
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < Inventory.Instance.NumberSlots; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            _availableSlots.Add(slot);
        }
    }

    private void UpdateSelectedSlot()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (selected == null)
        {
            return;
        }

        InventorySlot slot = selected.GetComponent<InventorySlot>();
        if (slot != null)
        {
            selectedSlot = slot;
        }
    }

    public void DrawItemInInventory(InventoryItem newItemToDraw, int amountToDraw, int itemIndex)
    {
        InventorySlot slot = _availableSlots[itemIndex];
        if (newItemToDraw != null)
        {
            slot.ActivateSlotUI(true);
            slot.UpdateSlotUI(newItemToDraw, amountToDraw);
        }
        else
        {
            slot.ActivateSlotUI(false);
        }
    }

    private void UpdateInventoryDescription(int index)
    {
        if (Inventory.Instance.ItemsInventory[index] != null)
        {
            itemIcon.sprite = Inventory.Instance.ItemsInventory[index].icon;
            itemName.text = Inventory.Instance.ItemsInventory[index].itemName;
            itemDescription.text = Inventory.Instance.ItemsInventory[index].description;
            panelInventoryDescription.SetActive(true);
        }
        else
        {
            panelInventoryDescription.SetActive(false);
        }
    }

    public void UseItem()
    {
        if (selectedSlot != null)
        {
            selectedSlot.UseSlot();
            selectedSlot.SelectingSlot();
        }
    }

    #region Events

    private void SlotInteractionResponse(TypeOfInteraction type, int index)
    {
        if (type == TypeOfInteraction.Click)
        {
            UpdateInventoryDescription(index);
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