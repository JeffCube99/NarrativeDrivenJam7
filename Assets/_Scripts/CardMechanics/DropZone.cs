using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject DropPreview;
    [SerializeField] private GameObject DropAppearance;
    [SerializeField] private Transform dropTransform;
    private bool isDisabled;

    public UnityEvent<Card> OnCardDropped;
    public UnityEvent<CardComponent> OnCardComponentDropped;

    private void Start()
    {
        isDisabled = false;
    }

    public void DisableDropZone()
    {
        isDisabled = true;
        DropPreview.SetActive(false);
        DropAppearance.SetActive(false);
    }

    public void EnableDropZone()
    {
        isDisabled = false;
        DropAppearance.SetActive(true);
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
        if (isDisabled)
        {
            return;
        }
        if (eventData.pointerDrag != null)
        {
            DropPreview.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDisabled)
        {
            return;
        }
        DropPreview.SetActive(false);
    }
}
