using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "ScriptableObjects/State/PlayerState")]

public class PlayerState : ScriptableObject
{
    public List<Card> cardsInHand;
    public List<Character> partyMembers;

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
}
