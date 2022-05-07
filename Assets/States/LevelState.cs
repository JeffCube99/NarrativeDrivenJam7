using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelState", menuName = "ScriptableObjects/State/LevelState")]
public class LevelState : SerealizableScriptableObject
{
    public string sceneName;
    public List<Card> cardsEarnedDuringEncounter;

    [System.Serializable]
    public struct LevelStateSaveData
    {
        public string sceneName;
    }

    public void AddCardRewards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            cardsEarnedDuringEncounter.Add(card);
        }
    }

    public void AddCardReward(Card card)
    {
        cardsEarnedDuringEncounter.Add(card);
    }

    public void ClearCardRewards()
    {
        cardsEarnedDuringEncounter = new List<Card>();
    }

    public override object SaveData()
    {
        LevelStateSaveData saveData = new LevelStateSaveData();
        saveData.sceneName = sceneName;
        return saveData;
    }

    public override void LoadData(object data)
    {
        LevelStateSaveData saveData = (LevelStateSaveData)data;
        sceneName = saveData.sceneName;
        cardsEarnedDuringEncounter = new List<Card>();
    }

    public override void ResetForNewGame()
    {
        sceneName = "Introduction";
        cardsEarnedDuringEncounter = new List<Card>();
    }
}
