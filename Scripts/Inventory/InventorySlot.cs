using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum TypeOfInteraction
{
    Click,
    Use,
    Equip,
    Remove
}

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image image;
    public static Action<TypeOfInteraction, int> EventSlotInteraction;

    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject backgroundAmount;
    [SerializeField] private TextMeshProUGUI amountText;
    public int Index { get; set; }

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    public void UpdateSlotUI(InventoryItem item, int amount)
    {
        itemIcon.sprite = item.icon;
        amountText.text = amount.ToString();
    }

    public void ActivateSlotUI(bool state)
    {
        itemIcon.gameObject.SetActive(state);
        backgroundAmount.SetActive(state);
    }

    public void SelectingSlot()
    {
        _button.Select();
    }

    public void ClickSlot()
    {
        EventSlotInteraction?.Invoke(TypeOfInteraction.Click, Index);
    }

    public void UseSlot()
    {
        if (Inventory.Instance.ItemsInventory[Index] != null)
        {
            EventSlotInteraction?.Invoke(TypeOfInteraction.Use, Index);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Inventory.Instance.ItemsInventory[Index] != null)
        {
            InventoryUI.Instance.indexSlotToMove = Index;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Inventory.Instance.ItemsInventory[Index] == null)
        {
            image.raycastTarget = true;
            Inventory.Instance.MoveItem(InventoryUI.Instance.indexSlotToMove, Index);
            InventoryUI.Instance.DrawItemInInventory(null, 0,
                eventData.pointerDrag.GetComponent<InventorySlot>().Index);
            SelectingSlot();
            ClickSlot();
        }
    }
}