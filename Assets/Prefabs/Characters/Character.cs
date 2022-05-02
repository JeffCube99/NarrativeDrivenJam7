using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Object/Character")]
public class Character : ScriptableObject
{
    public Sprite characterSprite;
    public string characterName;
}
