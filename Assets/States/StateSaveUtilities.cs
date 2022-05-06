using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateSaveUtilities", menuName = "ScriptableObjects/Utilities/StateSaveUtilities")]
public class StateSaveUtilities : ScriptableObject
{
    public List<Card> cards;
    public List<Character> characters;

    public List<string> ConvertCardsToStrings(List<Card> cardList)
    {
        List<string> stringList = new List<string>();
        foreach (Card card in cardList)
        {
            stringList.Add(card.title);
        }
        return stringList;
    }
    
    public List<Card> ConvertStringsToCards(List<string> stringList)
    {
        List<Card> cardList = new List<Card>();
        foreach(string cardTitle in stringList)
        {
            foreach(Card card in cards)
            {
                if (card.title == cardTitle)
                {
                    cardList.Add(card);
                    break;
                }
            }
        }
        return cardList;
    }

    public List<string> ConvertCharactersToStrings(List<Character> characterList)
    {
        List<string> stringList = new List<string>();
        foreach (Character character in characterList)
        {
            stringList.Add(character.characterName);
        }
        return stringList;
    }

    public List<Character> ConvertStringsToCharacters(List<string> stringList)
    {
        List<Character> characterList = new List<Character>();
        foreach (string characterName in stringList)
        {
            foreach (Character character in characters)
            {
                if (characterName == character.characterName)
                {
                    characterList.Add(character);
                    break;
                }
            }
        }
        return characterList;
    }
}
