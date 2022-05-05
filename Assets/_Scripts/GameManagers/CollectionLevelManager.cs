using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionLevelManager : MonoBehaviour
{
    [SerializeField] LevelState levelState;

    public void OnCardCollectionFinished()
    {
        levelState.ClearCardRewards();
    }
}
