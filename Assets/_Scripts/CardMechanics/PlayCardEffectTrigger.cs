using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardEffectTrigger : MonoBehaviour
{
    public void TriggerPlayCardEffect(CardComponent cardComponent)
    {
        cardComponent.OnCardPlayed();
    }
}
