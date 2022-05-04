using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New ActionScene", menuName = "ScriptableObjects/Scenes/ActionScene")]
public class ActionScene : BaseScene
{
    public string actionCode;
    public BaseScene nextScene;
    public bool startNextSceneImmediately;
    public UnityEvent<string> OnSceneStart;
    public override void StartScene()
    {
        OnSceneStart.Invoke(actionCode);
        if (startNextSceneImmediately)
        {
            if (nextScene != null)
            {
                nextScene.StartScene();
            }
        }
    }

    public void EndScene()
    {
        if (!startNextSceneImmediately)
        {
            if (nextScene != null)
            {
                nextScene.StartScene();
            }
        }
    }
}
