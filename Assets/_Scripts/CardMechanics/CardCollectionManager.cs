using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectionManager : MonoBehaviour
{
    [SerializeField] private PlayerCardHandManager playerCardHandManager;
    [SerializeField] private PlayerCardHandManager earnedCardHandManager;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private LevelState levelState;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private DropZoneAppearanceController dropZoneAppearanceController;
    [SerializeField] private PlayerHandAppearanceController playerHandAppearanceController;
    [SerializeField] private PlayerHandAppearanceController earnedHandAppearanceController;
    [SerializeField] private int maximumCardOverride = -1;
    private List<GameObject> earnedCards;

    private void Start()
    {
        earnedCards = new List<GameObject>();
        AddCardsToPlayerHand();
        AddCardsToEarnedCardHandArea();
        ShowPlayArea();
        CheckIfHandIsFull();
    }

    private void ShowPlayArea()
    {
        dropZoneAppearanceController.ShowDropZone();
        playerHandAppearanceController.ShowPlayerHand();
    }

    private void OnHandFull()
    {
        foreach (GameObject cardObject in earnedCards)
        {
            Draggable draggable = cardObject.GetComponent<Draggable>();
            if (draggable != null)
            {
                Destroy(draggable);
            }
        }
        earnedHandAppearanceController.HidePlayerHand();
    }

    private void CheckIfHandIsFull()
    {
        int maxCards = gameSettings.maximumCards;
        if (maximumCardOverride > 0)
        {
            maxCards = maximumCardOverride;
        }
        if (playerState.cardsInHand.Count >= maxCards)
        {
            OnHandFull();
        }
    }

    private void AddCardsToEarnedCardHandArea()
    {
        foreach (Card card in levelState.cardsEarnedDuringEncounter)
        {
            GameObject cardObject = card.cardObjectPool.Instantiate(Vector3.zero, Quaternion.identity);
            earnedCards.Add(cardObject);
            earnedCardHandManager.AddCardToHand(cardObject);
        }
    }

    private void AddCardsToPlayerHand()
    {
        foreach (Card card in playerState.cardsInHand)
        {
            GameObject cardObject = card.cardObjectPool.Instantiate(Vector3.zero, Quaternion.identity);
            DisableCardDrag(cardObject);
            playerCardHandManager.AddCardToHand(cardObject);
        }
    }

    public void CollectCard(GameObject cardObject)
    {
        DisableCardDrag(cardObject);
        playerCardHandManager.AddCardToHand(cardObject);
        CardComponent cardComponennt = cardObject.GetComponent<CardComponent>();
        if (cardComponennt != null)
        {
            playerState.AddCard(cardComponennt.card);
        }
        CheckIfHandIsFull();
    }

    public void DisableCardDrag(GameObject cardObject)
    {
        Draggable dragController = cardObject.GetComponent<Draggable>();
        if (dragController != null)
        {
            dragController.DisableDrag();
        }
    }
}
