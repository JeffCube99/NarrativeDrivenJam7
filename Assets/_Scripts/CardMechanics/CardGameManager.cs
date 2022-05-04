using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    [SerializeField] private DropZoneAppearanceController dropZoneAppearanceController;
    [SerializeField] private PlayerHandAppearanceController playerHandAppearanceController;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private PlayerCardHandManager playerCardHandManager;
    private CardChoiceScene currentCardChoiceScene;

    private void Start()
    {
        HidePlayArea();
        AddCardsToPlayerHand();
    }

    private void AddCardsToPlayerHand()
    {
        foreach (Card card in playerState.cardsInHand)
        {
            GameObject cardObject = card.cardObjectPool.Instantiate(Vector3.zero, Quaternion.identity);
            playerCardHandManager.AddCardToHand(cardObject);
        }
    }

    public void OnCardPlayed(object data)
    {
        Card card = (Card)data;
        if (currentCardChoiceScene != null)
        {
            HidePlayArea();
            currentCardChoiceScene.EndScene(card);
        }
    }

    public void OnCardChoiceSceneBegin(object data)
    {
        currentCardChoiceScene = (CardChoiceScene)data;
        ShowPlayArea();
    }

    private void HidePlayArea()
    {
        dropZoneAppearanceController.HideDropZone();
        playerHandAppearanceController.HidePlayerHand();
    }

    private void ShowPlayArea()
    {
        dropZoneAppearanceController.ShowDropZone();
        playerHandAppearanceController.ShowPlayerHand();
    }
}
