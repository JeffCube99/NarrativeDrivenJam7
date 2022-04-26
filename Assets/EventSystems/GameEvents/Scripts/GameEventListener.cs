using UnityEngine;
using UnityEngine.Events;

// We make this class Serializable so that its properties are displayed in the inspector
// when added to the GameEventListenerComponent's listeners list.
[System.Serializable]
public class GameEventListener
{
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when event is raised.")]
    public UnityEvent<object> Response;

    // We invoke the UnityEvent when we the GameEvent is raised
    public void OnEventRaised(object data)
    {
        Response.Invoke(data);
    }
}
