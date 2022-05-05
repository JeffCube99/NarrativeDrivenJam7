using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public PlayerState playerState;
    public List<Transform> npcTransforms;

    private void Start()
    {
        for (int i = 0; i < playerState.partyMembers.Count; i++)
        {
            Instantiate(playerState.partyMembers[i].npcPrefab, npcTransforms[i]);
        }
    }
}
