using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image characterImage;
    private Queue<string> sentences;
    private bool sentenceIsTyping;
    private string sentenceWeAreTyping;

    public UnityEvent OnDialogueEnded;
    public UnityEvent OnTextTyped;

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        QueueDialogueSentences(dialogue);
        DisplayDialogueInformation(dialogue);
        DisplayNextSentence();
    }

    private void DisplayDialogueInformation(Dialogue dialogue)
    {
        characterImage.sprite = dialogue.character.characterSprite;
        nameText.text = dialogue.character.characterName;
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
        if (sentences.Count == 0 && !sentenceIsTyping)
        {
            EndDialogue();
            return;
        }

        if (!sentenceIsTyping)
        {
            sentenceWeAreTyping = sentences.Dequeue();
            sentenceIsTyping = true;
            StartCoroutine(TypeSentence(sentenceWeAreTyping));
        }
        else
        {
            FinishSentence();
        }
    }

    private void FinishSentence()
    {
        StopAllCoroutines();
        sentenceIsTyping = false;
        dialogueText.text = sentenceWeAreTyping;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            OnTextTyped.Invoke();
            yield return new WaitForSeconds(0.05f);
        }
        sentenceIsTyping = false;
    }

    public void EndDialogue()
    {
        OnDialogueEnded.Invoke();
    }
}
