using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardAppearanceController : MonoBehaviour
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

    private void Start()
    {
        if (card != null)
        {
            titleText.text = card.title;
            descriptionText.text = card.description;
            cardImage.sprite = card.artwork;
        }

        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 positionDelta = transform.position - lastPosition;
        lastPosition = transform.position;
        Vector3 targetVelocity = positionDelta / Time.deltaTime / maximumTiltVelocity;
        dampedVelocity = Vector3.SmoothDamp(dampedVelocity, targetVelocity, ref acceleration, m_MovementSmoothing);
        Debug.Log(dampedVelocity);
        cardAnimator.SetFloat("x_velocity", dampedVelocity.x);
        cardAnimator.SetFloat("y_velocity", dampedVelocity.y);
    }


}
