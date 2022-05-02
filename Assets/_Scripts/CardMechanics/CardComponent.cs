using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardComponent : MonoBehaviour
{
    public Card card;
    public UnityEvent onCardPlayed;

    public void OnCardPlayed()
    {
        onCardPlayed.Invoke();
    }
}
