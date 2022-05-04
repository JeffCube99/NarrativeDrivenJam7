using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTester : MonoBehaviour
{
    public Dialogue testDialogue;
    public DialogueManager dialogueManager;
    public Conversation testConversation;
    public ConversationManager conversationManager;

    public void StartTestDialogue()
    {
        dialogueManager.StartDialogue(testDialogue);
    }

    public void StartTestConversation()
    {
        conversationManager.StartConversation(testConversation);
    }
}
