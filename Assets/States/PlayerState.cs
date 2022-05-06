using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "ScriptableObjects/State/PlayerState")]

public class PlayerState : SerealizableScriptableObject
{
    public List<Card> cardsInHand;
    public List<Character> partyMembers;
    public StateSaveUtilities stateSaveUtilities;

    [System.Serializable]
    public struct PlayerStateSaveData
    {
        public List<string> cardsInHand;
        public List<string> partyMembers;
    }

    public void RemoveCard(Card card)
    {
        cardsInHand.Remove(card);
    }

    public void AddCard(Card card)
    {
        cardsInHand.Add(card);
    }

    public void AddPartyMember(Character character)
    {
        partyMembers.Add(character);
    }

    public void RemovePartyMember(Character character)
    {
        partyMembers.Remove(character);
    }

    public override object SaveData()
    {
        PlayerStateSaveData saveData = new PlayerStateSaveData();
        saveData.cardsInHand = stateSaveUtilities.ConvertCardsToStrings(cardsInHand);
        saveData.partyMembers = stateSaveUtilities.ConvertCharactersToStrings(partyMembers);
        return saveData;
    }

    public override void LoadData(object data)
    {
        PlayerStateSaveData loadedData = (PlayerStateSaveData)data;
        cardsInHand = stateSaveUtilities.ConvertStringsToCards(loadedData.cardsInHand);
        partyMembers = stateSaveUtilities.ConvertStringsToCharacters(loadedData.cardsInHand);
    }

    public override void ResetForNewGame()
    {
        cardsInHand = new List<Card>();
        partyMembers = new List<Character>();
    }
}
