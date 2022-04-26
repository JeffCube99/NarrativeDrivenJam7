using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventListenerComponent : MonoBehaviour
{
    public List<GameEventListener> listeners;

    private void OnEnable()
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.Event.RegisterListener(listener);
        }
    }

    private void OnDisable()
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.Event.UnregisterListener(listener);
        }
    }
}
