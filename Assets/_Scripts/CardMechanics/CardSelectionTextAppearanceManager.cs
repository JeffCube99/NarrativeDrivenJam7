using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSelectionTextAppearanceManager : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private PlayerCardHandManager earnedHandManager;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private TextMeshProUGUI cardSelectionText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private int maximumCardOverride = -1;

    private void Start()
    {
        continueButton.SetActive(false);
    }

    private void Update()
    {
        int cardNumber = 0;
        if (maximumCardOverride <= 0)
        {
            int numberOfSlotsAvaliable = gameSettings.maximumCards - playerState.cardsInHand.Count;
            cardNumber = Mathf.Min(earnedHandManager.GetNumberOfCardsInHand(), numberOfSlotsAvaliable);
        }
        else
        {
            int numberOfSlotsAvaliable = maximumCardOverride - playerState.cardsInHand.Count;
            cardNumber = Mathf.Min(earnedHandManager.GetNumberOfCardsInHand(), numberOfSlotsAvaliable);
        }
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
