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

    private void Start()
    {
        if (card != null)
        {
            titleText.text = card.title;
            descriptionText.text = card.description;
            cardImage.sprite = card.artwork;
        }
    }
}
