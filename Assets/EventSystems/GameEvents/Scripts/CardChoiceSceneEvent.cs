using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New CardChoiceSceneEvent", menuName = "ScriptableObjects/Events/CardChoiceSceneEvent")]
public class CardChoiceSceneEvent : GameEvent
{
    public void Raise(CardChoiceScene data)
    {
        RaiseListeners(data);
    }
}
