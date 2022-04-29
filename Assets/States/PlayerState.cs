using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "ScriptableObjects/State/PlayerState")]

public class PlayerState : ScriptableObject
{
    public GameObject grabbedObject;

    private void OnEnable()
    {
        grabbedObject = null;
    }

    private void OnDisable()
    {
        grabbedObject = null;
    }
}
