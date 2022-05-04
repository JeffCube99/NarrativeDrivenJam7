using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionEventTrigger : MonoBehaviour
{
    [System.Serializable]
    public struct KeyEventPair
    {
        public string key;
        public UnityEvent unityEvent;
    }

    public List<KeyEventPair> keyEventPairs;

    public void OnActionEventRecieved(object data)
    {
        string targetKey = (string)data;
        foreach (KeyEventPair pair in keyEventPairs)
        {
            if (pair.key == targetKey)
            {
                pair.unityEvent.Invoke();
            }
        }
    }
}
