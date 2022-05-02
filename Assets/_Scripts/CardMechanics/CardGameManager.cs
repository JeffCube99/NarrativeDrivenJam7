using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    [SerializeField] private DropZone cardDropZone;
    private CardChoiceScene currentCardChoiceScene;

    private void Start()
    {
        DisablePlayArea();
    }

    public void OnCardPlayed(object data)
    {
        Card card = (Card)data;
        DisablePlayArea();
        if (currentCardChoiceScene != null)
        {
            currentCardChoiceScene.EndScene(card);
        }
    }

    public void OnCardChoiceSceneBegin(object data)
    {
        currentCardChoiceScene = (CardChoiceScene)data;
        EnablePlayArea();
    }

    private void EnablePlayArea()
    {
        cardDropZone.EnableDropZone();
    }

    private void DisablePlayArea()
    {
        cardDropZone.DisableDropZone();
    }
}
