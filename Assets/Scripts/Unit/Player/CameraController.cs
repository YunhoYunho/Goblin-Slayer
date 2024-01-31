using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private CinemachineFreeLook freeLook;
    [SerializeField]
    private float sensitivity = 1f;

    private Image camControlArea;
    private string mouseX = "Mouse X";
    private string mouseY = "Mouse Y";

    private void Awake()
    {
        camControlArea = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            camControlArea.rectTransform,eventData.position,
            eventData.enterEventCamera, out Vector2 pos))
        {
            freeLook.m_XAxis.m_InputAxisName = mouseX;
            freeLook.m_YAxis.m_InputAxisName = mouseY;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        freeLook.m_XAxis.m_InputAxisName = null;
        freeLook.m_YAxis.m_InputAxisName = null;
        freeLook.m_XAxis.m_InputAxisValue = 0;
        freeLook.m_YAxis.m_InputAxisValue = 0;
    }
}
