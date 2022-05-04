using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandAppearanceController : MonoBehaviour
{
    [SerializeField] private Animator playerHandAnimator;

    public void HidePlayerHand()
    {
        playerHandAnimator.SetBool("isHidden", true);
    }

    public void ShowPlayerHand()
    {
        playerHandAnimator.SetBool("isHidden", false);
    }
}
