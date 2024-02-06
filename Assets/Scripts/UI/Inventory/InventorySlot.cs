using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public Image image;
    public bool isSelected = false;
    public int detectNum;

    private void Awake()
    {
        NotSelected();
    }

    public void Selected()
    {
        Color color = image.color;
        color.a = 1.0f;
        image.color = color;
        isSelected = true;
    }

    public void NotSelected()
    {
        Color color = image.color;
        color.a = 0.8f;
        image.color = color;
        isSelected = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem droppedItem = dropped.GetComponent<InventoryItem>();

        if (transform.childCount > 0)
        {
            Transform itemInSlot = transform.GetChild(0);
            itemInSlot.SetParent(droppedItem.parentAfterDrag);
            itemInSlot.position = droppedItem.parentAfterDrag.position;
        }

        droppedItem.parentAfterDrag = transform;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        detectNum = InventoryManager.Instance.inventoryPanel.isCursorOnQuickSlot == true ? 2 : 0;

        if (isSelected)
        {
            NotSelected();
        }
        else
        {
            InventoryManager.Instance.ChangeSelectedSlot(transform.GetSiblingIndex() + detectNum);
        }

        detectNum = 0;
    }
}
