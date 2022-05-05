using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticScriptableObjectTest", menuName = "ScriptableObjects/Test/StaticScriptableObjectTest")]
public class StaticScriptableObjectTest : ScriptableObject
{
    public static string _coolName;

    public string nickName;
}
