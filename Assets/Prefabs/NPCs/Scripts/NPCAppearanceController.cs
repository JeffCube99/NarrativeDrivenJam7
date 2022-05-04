using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAppearanceController : MonoBehaviour
{
    public Animator animator;
    public Character character;

    public void OnStartTalking(object data)
    {
        Character speakingCharacter = (Character)data;
        if (speakingCharacter == character)
        {
            animator.SetBool("isTalking", true);
        }
    }

    public void OnEndTalking(object data)
    {
        Character speakingCharacter = (Character)data;
        if (speakingCharacter == character)
        {
            animator.SetBool("isTalking", false);
        }
    }
}
