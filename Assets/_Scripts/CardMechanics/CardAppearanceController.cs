using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardAppearanceController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private Card card;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image cardImage;
    [SerializeField] private Animator cardAnimator;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [Range(1, 100f)] [SerializeField] private float maximumTiltVelocity = 1f;	// How fast you drag before you reach maximum card tilt
    private Vector3 lastPosition;
    private Vector3 dampedVelocity;
    private Vector3 acceleration = Vector3.zero;
    [SerializeField] private bool isMouseOver;
    [SerializeField] private bool isBeingDragged;
    [SerializeField] private bool isPlayed;

    private void Start()
    {
        ResetCardAppearance();
    }

    private void OnEnable()
    {
        ResetCardAppearance();
    }

    private void ResetCardAppearance()
    {
        if (card != null)
        {
            titleText.text = card.title;
            descriptionText.text = card.description;
            cardImage.sprite = card.artwork;
        }

        lastPosition = transform.position;
        isMouseOver = false;
        isBeingDragged = false;
        isPlayed = false;

        SetGrowAnimation();
        SetPlayedAnimation();
    }

    public void OnCardPlayed()
    {
        Debug.Log("Card has been played");
        isPlayed = true;
        SetPlayedAnimation();
    }

    public void OnCardPlayedAnimationFinished()
    {
        gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isBeingDragged = true;
        SetGrowAnimation();
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isBeingDragged = false;
        SetGrowAnimation();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        SetGrowAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        SetGrowAnimation();
    }

    private void SetGrowAnimation()
    {
        if (isMouseOver == true && isBeingDragged == false)
        {
            cardAnimator.SetBool("shouldGrow", true);
        }
        else
        {
            cardAnimator.SetBool("shouldGrow", false);
        }
    }

    private void SetPlayedAnimation()
    {
        cardAnimator.SetBool("isPlayed", isPlayed);
    }

    private void Update()
    {
        Vector3 positionDelta = transform.position - lastPosition;
        lastPosition = transform.position;
        Vector3 targetVelocity = positionDelta / Time.deltaTime / maximumTiltVelocity;
        dampedVelocity = Vector3.SmoothDamp(dampedVelocity, targetVelocity, ref acceleration, m_MovementSmoothing);
        cardAnimator.SetFloat("x_velocity", dampedVelocity.x);
        cardAnimator.SetFloat("y_velocity", dampedVelocity.y);
    }


}
