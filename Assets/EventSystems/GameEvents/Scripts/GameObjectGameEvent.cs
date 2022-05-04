using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameObjectGameEvent", menuName = "ScriptableObjects/Events/GameObjectGameEvent")]
public class GameObjectGameEvent : GameEvent
{
    public void Raise(GameObject data)
    {
        RaiseListeners(data);
    }
}
