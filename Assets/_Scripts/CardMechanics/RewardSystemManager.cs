using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardSystemManager", menuName = "ScriptableObjects/Game/RewardSystemManager")]
public class RewardSystemManager : ScriptableObject
{
    [System.Serializable]
    public struct SingleCardChoiceReward
    {
        public Card choice;
        public List<Card> reward;
    }

    public List<SingleCardChoiceReward> singleCardChoiceRewards;

    private List<Card> GetRewardForSingleCardChoice(Card cardChoice)
    {
        foreach (SingleCardChoiceReward reward in singleCardChoiceRewards)
        {
            if (reward.choice == cardChoice)
            {
                return reward.reward;
            }
        }
        return new List<Card>();
    }

    public List<Card> GenerateRewards(List<Card> cardChoices)
    {
        if (cardChoices.Count == 1)
        {
            return GetRewardForSingleCardChoice(cardChoices[0]);
        }
        return cardChoices;
    }
}
