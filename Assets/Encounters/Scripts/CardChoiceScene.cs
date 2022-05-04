using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardChoiceScene", menuName = "ScriptableObjects/Scenes/CardChoiceScene")]
public class CardChoiceScene : BaseScene
{
    [System.Serializable]
    public struct CardChoiceOutcome
    {
        public List<Card> cardChoices;
        public BaseScene resultScene;
    }

    public List<CardChoiceOutcome> possibleOutcomes;
    public CardChoiceSceneEvent cardChoiceEvent;

    public override void StartScene()
    {
        cardChoiceEvent.Raise(this);
    }

    public void EndScene(Card cardChoice)
    {
        foreach (CardChoiceOutcome outcome in possibleOutcomes)
        {
            if (outcome.cardChoices.Contains(cardChoice))
            {
                outcome.resultScene.StartScene();
                return;
            }
        }
    }
}
