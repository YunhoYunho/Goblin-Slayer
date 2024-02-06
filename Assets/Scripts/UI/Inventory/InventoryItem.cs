using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;
    [HideInInspector]
    public ItemData item;
    [HideInInspector]
    public int count = 1;
    [HideInInspector]
    public Transform parentAfterDrag;

    public void InitItem(ItemData newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        UpdateCount();
    }

    public void UpdateCount()
    {
        countText.text = count.ToString();
        bool isTextActive = count > 1;
        countText.gameObject.SetActive(isTextActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
