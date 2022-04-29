using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject DropPreview;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.position = transform.position;
            eventData.pointerDrag.transform.rotation = transform.rotation;
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
