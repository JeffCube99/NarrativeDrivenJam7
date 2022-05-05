using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelState", menuName = "ScriptableObjects/State/LevelState")]
public class LevelState : ScriptableObject
{
    public string sceneName;
    public List<Card> cardsEarnedDuringEncounter;

    public void AddCardRewards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            cardsEarnedDuringEncounter.Add(card);
        }
    }

    public void ClearCardRewards()
    {
        cardsEarnedDuringEncounter = new List<Card>();
    }
}
