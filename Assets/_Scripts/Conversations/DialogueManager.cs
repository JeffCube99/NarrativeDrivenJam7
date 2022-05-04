using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using FMODUnity;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueAppearanceManager dialogueAppearanceManager;
    private Queue<string> sentences;

    public UnityEvent OnDialogueEnded;

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        QueueDialogueSentences(dialogue);
        dialogueAppearanceManager.UpdateDialogueAppearance(dialogue);
        dialogueAppearanceManager.ShowContinueButton();
        DisplayNextSentence();
    }

    private void QueueDialogueSentences(Dialogue dialogue)
    {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if (dialogueAppearanceManager.IsTyping())
        {
            dialogueAppearanceManager.FinishTypingSentence();
        }
        else if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            dialogueAppearanceManager.TypeOutSentence(sentences.Dequeue());
        }
    }

    public void EndDialogue()
    {
        dialogueAppearanceManager.HideContinueButton();
        OnDialogueEnded.Invoke();
    }
}
