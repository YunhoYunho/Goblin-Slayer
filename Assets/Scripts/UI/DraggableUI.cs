using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Vector2 offsetPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        offsetPos = new Vector2(transform.position.x, transform.position.y) - eventData.position;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + offsetPos;
    }
}
