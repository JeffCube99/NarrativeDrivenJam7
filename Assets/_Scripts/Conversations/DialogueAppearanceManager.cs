using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;
using UnityEngine.UI;
using TMPro;

public class DialogueAppearanceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image characterImage;
    [SerializeField] private StudioEventEmitter dialogueAudioEmitter;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private Animator dialogueAnimator;
    private string currentSentence;
    private bool isTyping;
    private Character currentCharacter;

    public UnityEvent OnWordTyped;
    public UnityEvent<Character> OnCharacterStartsSpeaking;
    public UnityEvent<Character> OnCharacterStopsSpeaking;

    private void Start()
    {
        currentCharacter = null;
        currentSentence = "";
        isTyping = false;
    }

    public void UpdateDialogueAppearance(Dialogue dialogue)
    {
        currentCharacter = dialogue.character;
        characterImage.sprite = dialogue.character.characterSprite;
        nameText.text = dialogue.character.characterName;
        dialogueAudioEmitter.ChangeEvent(dialogue.character.characterSpeakingEvent);
    }

    public void HideDialogueBox()
    {
        dialogueAnimator.SetBool("isOpen", false);
    }

    public void ShowDialogueBox()
    {
        dialogueAnimator.SetBool("isOpen", true);
    }

    public void HideContinueButton()
    {
        continueButton.SetActive(false);
    }

    public void ShowContinueButton()
    {
        continueButton.SetActive(true);
    }

    public void TypeOutSentence(string sentence)
    {
        currentSentence = sentence;
        isTyping = true;
        OnCharacterStartsSpeaking.Invoke(currentCharacter);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    public bool IsTyping()
    {
        return isTyping;
    }

    public void FinishTypingSentence()
    {
        StopAllCoroutines();
        dialogueText.text = currentSentence;
        isTyping = false;
        OnCharacterStopsSpeaking.Invoke(currentCharacter);
    }

    IEnumerator TypeSentence(string sentence)
    {
        // OnSpeakingBegin.Invoke();
        dialogueText.text = "";
        char lastLetter = " "[0];
        char space = " "[0];
        int soundsPerWord = 1;
        int count = 0;
        foreach (char letter in sentence.ToCharArray())
        {
            if (lastLetter == space)
            {
                count += 1;
                if (count % soundsPerWord == 0)
                {
                    OnWordTyped.Invoke();
                }
            }
            dialogueText.text += letter;
            lastLetter = letter;
            yield return new WaitForSeconds(0.02f);
        }
        OnCharacterStopsSpeaking.Invoke(currentCharacter);
        isTyping = false;
    }
}
