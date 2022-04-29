using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Vector3 mouseOffset;
    private float mouseDepth;
    private int defaultLayer;

    public UnityEvent<GameObject> OnObjectPickedUp;
    public UnityEvent OnObjectReleased;

    private void Start()
    {
        defaultLayer = gameObject.layer;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("OnBeginDrag");
        mouseDepth = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mouseWorldPosition = MouseUtilities.GetMouseWorldPosition(mouseDepth);
        mouseOffset = transform.position - mouseWorldPosition;
        OnObjectPickedUp.Invoke(gameObject);

        // Object should ignore raycast when dragged so raycasts can hit things behind the card
        int layerNumber = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.layer = layerNumber;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPosition = MouseUtilities.GetMouseWorldPosition(mouseDepth);
        transform.position = mouseWorldPosition + mouseOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("OnEndDrag");
        gameObject.layer = defaultLayer;
        OnObjectReleased.Invoke();
    }
}
