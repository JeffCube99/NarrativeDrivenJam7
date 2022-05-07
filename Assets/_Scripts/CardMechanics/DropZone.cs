using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform dropTransform;
    private bool isDisabled;

    public UnityEvent<Card> OnCardDropped;
    public UnityEvent<CardComponent> OnCardComponentDropped;
    public UnityEvent<GameObject> OnGameObjectDropped;

    private void Start()
    {
        isDisabled = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isDisabled)
        {
            return;
        }
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable != null)
        {
            draggable.transform.SetParent(dropTransform);
            Debug.Log("Setting transform to drop zone");
            CardComponent cardComponent = eventData.pointerDrag.GetComponent<CardComponent>();
            if (cardComponent != null)
            {
                OnCardDropped.Invoke(cardComponent.card);
                OnCardComponentDropped.Invoke(cardComponent);
                OnGameObjectDropped.Invoke(cardComponent.gameObject);
            }
        }
    }
}
