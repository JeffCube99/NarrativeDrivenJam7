using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SerealizableScriptableObject : ScriptableObject
{
    public abstract object SaveData();

    public abstract void LoadData(object data);

    public abstract void ResetForNewGame();
}
