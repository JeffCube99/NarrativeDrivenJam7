using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class StartNewGameManager : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TextMeshProUGUI errorText;

    public UnityEvent OnSuccessfulStartGame;

    public void OnEnable()
    {
        nameInputField.text = saveSystem.GenerateNewSaveFileName();
    }

    public void OnStartClicked()
    {
        string proposedName = nameInputField.text;
        if (!saveSystem.SaveFileNameExists(proposedName))
        {
            saveSystem.SetupNewGame(proposedName);
            OnSuccessfulStartGame.Invoke();
        }
    }

    public void CheckSaveFileName(string proposedName)
    {
        if (saveSystem.SaveFileNameExists(proposedName))
        {
            errorText.text = "Save File Name Already Exists!";
        }
        else
        {
            errorText.text = "";
        }
    }
}
