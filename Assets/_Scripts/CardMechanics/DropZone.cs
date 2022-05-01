using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject DropPreview;
    [SerializeField] private Transform dropTransform;

    public UnityEvent<Card> OnCardDropped;
    public UnityEvent<CardComponent> OnCardComponentDropped;

    public void OnDrop(PointerEventData eventData)
    {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable != null)
        {
            draggable.transform.SetParent(dropTransform);
            CardComponent cardComponent = eventData.pointerDrag.GetComponent<CardComponent>();
            if (cardComponent != null)
            {
                OnCardDropped.Invoke(cardComponent.card);
                OnCardComponentDropped.Invoke(cardComponent);
            }
        }
        DropPreview.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DropPreview.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DropPreview.SetActive(false);
    }
}
