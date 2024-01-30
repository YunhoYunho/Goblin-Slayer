using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isCursorOnQuickSlot = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isCursorOnQuickSlot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isCursorOnQuickSlot = false;
    }
}
