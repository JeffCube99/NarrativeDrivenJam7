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
            GameObject npcPrefab = playerState.partyMembers[i].npcPrefab;
            if (npcPrefab != null)
            {
                Instantiate(npcPrefab, npcTransforms[i]);
            }
        }
    }
}
