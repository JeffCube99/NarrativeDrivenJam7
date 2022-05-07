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
    public RewardSystemManager rewardSystemManger;

    public override void StartScene()
    {
        cardChoiceEvent.Raise(this);
    }

    private CardChoiceOutcome? GetOutcome(Card cardChoice)
    {
        foreach (CardChoiceOutcome outcome in possibleOutcomes)
        {
            if (outcome.cardChoices.Contains(cardChoice))
            {
                return outcome;
            }
        }
        return null;
    }

    public List<Card> GenerateCardReward(Card cardChoice)
    {
        CardChoiceOutcome? outcome = GetOutcome(cardChoice);
        if (outcome != null)
        {
            return rewardSystemManger.GenerateRewards(outcome?.cardChoices);
        }
        return new List<Card>();
    }

    public void EndScene(Card cardChoice)
    {
        CardChoiceOutcome? outcome = GetOutcome(cardChoice);
        if (outcome != null)
        {
            outcome?.resultScene.StartScene();
        }
    }
}
