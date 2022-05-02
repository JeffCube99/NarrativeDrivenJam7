using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConversationSceneEvent", menuName = "ScriptableObjects/Events/ConversationSceneEvent")]
public class ConversationSceneEvent : GameEvent
{
    public void Raise(ConversationScene data)
    {
        RaiseListeners(data);
    }
}
