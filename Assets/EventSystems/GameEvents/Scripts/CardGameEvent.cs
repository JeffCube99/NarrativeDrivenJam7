using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardGameEvent", menuName = "ScriptableObjects/Events/CardGameEvent")]
public class CardGameEvent : GameEvent
{
    public void Raise(Card data)
    {
        RaiseListeners(data);
    }
}
