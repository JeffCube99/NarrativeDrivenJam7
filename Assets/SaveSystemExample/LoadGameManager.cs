using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Events;

public class LoadGameManager : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private GameObject loadGameTogglePrefab;
    [SerializeField] private Transform loadGameToggles;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private TextMeshProUGUI loadErrorText;

    public UnityEvent OnSuccessfulStartGame;

    private void DisplaySavedGames()
    {
        // remove old list of saved games
        foreach (Transform button in loadGameToggles)
        {
            Destroy(button.gameObject);
        }

        // create new list of saved games
        List<string> saveFileNames = saveSystem.GetSaveFileNames();
        foreach (string name in saveFileNames)
        {
            GameObject toggleObject = Instantiate(loadGameTogglePrefab);
            toggleObject.transform.SetParent(loadGameToggles, false);
            toggleObject.GetComponent<Toggle>().group = toggleGroup;
            toggleObject.GetComponentInChildren<TextMeshProUGUI>().text = name;
        }
    }

    public void OnStartClicked()
    {
        Toggle activeToggle = toggleGroup.ActiveToggles().First();
        string saveFileName = activeToggle.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        bool loadWasSuccessful = saveSystem.LoadGame(saveFileName);
        if (loadWasSuccessful)
        {
            OnSuccessfulStartGame.Invoke();
        }
    }

    public void OnDeleteClicked()
    {
        Toggle activeToggle = toggleGroup.ActiveToggles().First();
        string saveFileName = activeToggle.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        saveSystem.DeleteGame(saveFileName);
        DisplaySavedGames();
    }

    public void ShowLoadErrorText(object errorData)
    {
        string errorMessage = (string)errorData;
        loadErrorText.text = errorMessage;
    }

    private void OnEnable()
    {
        DisplaySavedGames();
        ShowLoadErrorText("");
    }
}
