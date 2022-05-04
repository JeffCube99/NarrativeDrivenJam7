using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "ScriptableObjects/State/PlayerState")]

public class PlayerState : ScriptableObject
{
    public List<Card> cardsInHand;
}
