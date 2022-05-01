using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    public void OnCardPlayed(object data)
    {
        Card card = (Card)data;
        Debug.Log($"Processed card {card.title}");
    }
}
