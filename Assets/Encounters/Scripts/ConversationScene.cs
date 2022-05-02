using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New ConversationScene", menuName = "ScriptableObjects/Scenes/ConversationScene")]
public class ConversationScene : BaseScene
{
    public Conversation conversation;
    public ConversationSceneEvent conversationEvent;
    public BaseScene nextScene;

    public override void StartScene()
    {
        conversationEvent.Raise(this);
    }

    public void EndScene()
    {
        if (nextScene != null)
        {
            nextScene.StartScene();
        }
    }
}
