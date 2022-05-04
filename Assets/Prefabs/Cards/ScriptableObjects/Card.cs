using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects/Object/Card")]

public class Card : ScriptableObject
{
    public string title;
    public string description;
    public Sprite artwork;
    public ObjectPool cardObjectPool;
}
