using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSelectionTextAppearanceManager : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private TextMeshProUGUI cardSelectionText;
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        continueButton.SetActive(false);
    }

    private void Update()
    {
        int cardNumber = gameSettings.maximumCards - playerState.cardsInHand.Count;
        if (cardNumber > 1)
        {
            cardSelectionText.text = $"Select {cardNumber} cards to add to your deck";
        }
        else if (cardNumber == 1)
        {
            cardSelectionText.text = $"Select 1 card to add to your deck";
        }
        else
        {
            cardSelectionText.text = "Continue on your journey.";
            continueButton.SetActive(true);
            enabled = false;
        }
    }
}
