using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    private Queue<Dialogue> dialogues;
    private ConversationScene currentConversationScene;

    void Awake()
    {
        dialogues = new Queue<Dialogue>();
    }

    public void OnConversationSceneBegin(object data)
    {
        currentConversationScene = (ConversationScene)data;
        StartConversation(currentConversationScene.conversation);
    }

    public void StartConversation(Conversation conversation)
    {
        QueueConversationDialogues(conversation);
        StartNextDialogue();
    }

    private void QueueConversationDialogues(Conversation conversation)
    {
        dialogues.Clear();
        foreach (Dialogue dialogue in conversation.dialogues)
        {
            dialogues.Enqueue(dialogue);
        }
    }

    public void StartNextDialogue()
    {
        if (dialogues.Count == 0)
        {
            EndConversation();
            return;
        }

        Dialogue dialogue = dialogues.Dequeue();
        dialogueManager.StartDialogue(dialogue);
    }

    public void EndConversation()
    {
        if (currentConversationScene != null)
        {
            currentConversationScene.EndScene();
        }
    }
}
