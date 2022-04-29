using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;

    public void OnObjectPickedUp(object data)
    {
        GameObject grabbedObject = (GameObject)data;
        playerState.grabbedObject = grabbedObject;
    }

    public void OnObjectReleased()
    {
        playerState.grabbedObject = null;
    }
}
