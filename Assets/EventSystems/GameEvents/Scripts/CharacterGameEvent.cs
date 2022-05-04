using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterGameEvent", menuName = "ScriptableObjects/Events/CharacterGameEvent")]
public class CharacterGameEvent : GameEvent
{
    public void Raise(Character data)
    {
        RaiseListeners(data);
    }
}
