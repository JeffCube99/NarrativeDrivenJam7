using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour
{
    public UnityEvent OnStart;
    public UnityEvent OnDelayedStart;
    public float delay;
    void Start()
    {
        OnStart.Invoke();
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(delay);
        OnDelayedStart.Invoke();
    }
}
