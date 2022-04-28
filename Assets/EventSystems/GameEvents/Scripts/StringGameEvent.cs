using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StringGameEvent", menuName = "ScriptableObjects/Events/StringGameEvent")]
public class StringGameEvent : GameEvent
{
    public void Raise(string data)
    {
        RaiseListeners(data);
    }
}
