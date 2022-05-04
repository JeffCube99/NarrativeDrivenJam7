using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneAppearanceController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject dropZoneHilightedImage;
    [SerializeField] private Animator dropZoneAnimator;
    public void HideDropZone()
    {
        dropZoneAnimator.SetBool("isHidden", true);
    }

    public void ShowDropZone()
    {
        dropZoneAnimator.SetBool("isHidden", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            dropZoneHilightedImage.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dropZoneHilightedImage.SetActive(false);
    }
}
