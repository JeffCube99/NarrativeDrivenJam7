using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector3 mouseOffset;
    private float mouseDepth;

    public UnityEvent<GameObject> OnObjectPickedUp;
    public UnityEvent OnObjectReleased;

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDepth = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mouseWorldPosition = MouseUtilities.GetMouseWorldPosition(mouseDepth);
        mouseOffset = transform.position - mouseWorldPosition;
        OnObjectPickedUp.Invoke(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnObjectReleased.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPosition = MouseUtilities.GetMouseWorldPosition(mouseDepth);
        transform.position = mouseWorldPosition + mouseOffset;
    }
}
