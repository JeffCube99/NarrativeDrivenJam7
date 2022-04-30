using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// This class assumes that it is being dragged from its origin point. Once it has been released it will return back to the origin 
/// (AKA transform.localPosition = Vector3.zero);.
/// </summary>
public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Vector3 mouseOffset;
    private float mouseDepth;
    private int defaultLayer;

    [Range(0.1f, 1f)] [SerializeField] private float lerpSpeed;
    public UnityEvent<GameObject> OnObjectPickedUp;
    public UnityEvent OnObjectReleased;

    private void Start()
    {
        defaultLayer = gameObject.layer;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(RotateToUpPosition());
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
        StopAllCoroutines();
        StartCoroutine(MoveToReturnPosition());
    }

    IEnumerator RotateToUpPosition()
    {
        while (Quaternion.Angle(transform.rotation, Quaternion.identity) > 0.1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, lerpSpeed);
            yield return null;
        }
        transform.rotation = Quaternion.identity;
    }

    IEnumerator MoveToReturnPosition()
    {
        while (transform.localPosition.magnitude > 0.1 || Quaternion.Angle(transform.localRotation, Quaternion.identity) > 0.1)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, lerpSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, lerpSpeed);
            yield return null;
        }
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
