using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private PlayerMover mover;
    [SerializeField]
    private RectTransform lever;
    [SerializeField]
    [Range(10f, 150f)]
    private float leverRange;

    private RectTransform rectTransform;
    private Vector2 inputDir;
    private bool isInput;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isInput)
        {
            ControlPlayer();
        }
    }

    private void ControlPlayer()
    {
        mover.Move(inputDir);
    }

    private void ControlLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVec = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVec;
        inputDir = inputVec / leverRange;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        mover.Move(Vector2.zero);
    }
}
