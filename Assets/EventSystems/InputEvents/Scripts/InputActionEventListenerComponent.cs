using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActionEventListenerComponent : MonoBehaviour
{
    public List<InputActionEventListener> listeners;

    private void OnEnable()
    {
        // Registers each listener to the GameEvent so OnEventRaised() is called if the GameEvent is raised
        foreach (InputActionEventListener listener in listeners)
        {
            listener.Event.RegisterListener(listener);
        }
    }

    private void OnDisable()
    {
        // Unregisters each listener from the GameEvent since OnEventRaised() does not need to be invoked when disabled.
        foreach (InputActionEventListener listener in listeners)
        {
            listener.Event.UnregisterListener(listener);
        }
    }
}
