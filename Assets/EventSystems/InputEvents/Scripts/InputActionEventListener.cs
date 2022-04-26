using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// We make this class Serializable so that its properties are displayed in the inspector
// when added to the InputActionEventListenerComponent's listeners list.
[System.Serializable]
public class InputActionEventListener
{
    [Tooltip("Event to register with.")]
    public InputActionEvent Event;

    [Tooltip("Response to invoke when event is raised.")]
    public UnityEvent<InputAction.CallbackContext> Response;

    // We invoke the UnityEvent when we the GameEvent is raised
    public void OnEventRaised(InputAction.CallbackContext context)
    {
        Response.Invoke(context);
    }
}
