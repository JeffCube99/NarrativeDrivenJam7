using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

// The CreateAssetMenu attribute allows us to create scriptable object assets in the editor
// In the Editor: Right Click > Create > ScriptableObjects > RuntimeSets > GameObjectRuntimeSet
[CreateAssetMenu(fileName = "SaveSystem", menuName = "ScriptableObjects/SaveLoadGame/SaveSystem")]
public class SaveSystem : ScriptableObject
{
    public string currentSaveFileName;
    public List<SerealizableScriptableObject> gameDataObjects;

    [SerializeField] private string SAVE_DIRECTORY_PATH;
    [SerializeField] private string SAVE_FILE_EXTENSION;

    public UnityEvent<string> OnSaveError;
    public UnityEvent<string> OnLoadError;

    [System.Serializable]
    public struct SaveData
    {
        public List<object> data;
    }

    private void OnEnable()
    {
        SAVE_DIRECTORY_PATH = Path.Combine(Application.persistentDataPath, "saves");
        SAVE_FILE_EXTENSION = ".save";
        CreateSaveDirectoryIfNecessary();
    }

    public bool SaveFileNameExists(string name)
    {
        List<string> saveFileNames = GetSaveFileNames();
        return saveFileNames.Contains(name);
    }

    public string GenerateNewSaveFileName()
    {
        string defaultName = "SaveFile";
        string newName;
        int count = 0;

        do
        {
            newName = defaultName + count;
            count += 1;
        }
        while (SaveFileNameExists(newName));

        return newName;
    }

    public List<string> GetSaveFileNames()
    {
        List<string> saveFileNames = new List<string>();
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_DIRECTORY_PATH);
        FileInfo[] files = directoryInfo.GetFiles("*" + SAVE_FILE_EXTENSION);
        foreach (FileInfo file in files)
        {
            saveFileNames.Add(file.Name.Substring(0, file.Name.Length - SAVE_FILE_EXTENSION.Length));
        }

        return saveFileNames;
    }

    private void CreateSaveDirectoryIfNecessary()
    {
        if (!Directory.Exists(SAVE_DIRECTORY_PATH))
        {
            Directory.CreateDirectory(SAVE_DIRECTORY_PATH);
        }
    }

    private string GeneratePathToSaveFile(string saveFileName)
    {
        string path = Path.Combine(SAVE_DIRECTORY_PATH, saveFileName + SAVE_FILE_EXTENSION);
        return path;
    }

    private SaveData CollectDataFromGameObjects()
    {
        SaveData saveData = new SaveData();
        saveData.data = new List<object>();
        foreach (SerealizableScriptableObject gameDataObject in gameDataObjects)
        {
            object newData = gameDataObject.SaveData();
            saveData.data.Add(newData);
        }
        return saveData;
    }

    public void SaveGame()
    {
        SaveData saveData = CollectDataFromGameObjects();
        string saveFilePath = GeneratePathToSaveFile(currentSaveFileName);
        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Create(saveFilePath);
        formatter.Serialize(file, saveData);
        file.Close();
    }

    private SaveData? GetSaveDataFromSaveFile(string saveFileName)
    {
        string saveFilePath = GeneratePathToSaveFile(saveFileName);
        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(saveFilePath, FileMode.Open);
        try
        {
            SaveData data = (SaveData)formatter.Deserialize(file);
            file.Close();
            return data;
        }
        catch
        {
            OnLoadError.Invoke($"Failed to load save data from file {saveFilePath}. The save file may be corrupted or out of date.");
            throw;
        }
    }

    // Returns true if load is successful
    private bool LoadSaveDataIntoGameObjects(SaveData saveData)
    {
        if (saveData.data.Count == gameDataObjects.Count)
        {
            for (int i = 0; i < saveData.data.Count; i++)
            {
                try
                {
                    gameDataObjects[i].LoadData(saveData.data[i]);
                }
                catch
                {
                    // Stop loading data! 
                    OnLoadError.Invoke($"Game object {i} failed to load from saved data");
                    return false;
                }
            }
        }
        else
        {
            OnLoadError.Invoke($"Mismatch between amount of saved data ({saveData.data.Count})" +
                $" and amount of game objects{gameDataObjects.Count}");
            return false;
        }
        return true;
    }

    // Returns true if load was successful
    public bool LoadGame(string saveFileName)
    {
        if (SaveFileNameExists(saveFileName))
        {
            SaveData? saveData = GetSaveDataFromSaveFile(saveFileName);
            if (saveData != null)
            {
                return LoadSaveDataIntoGameObjects((SaveData)saveData);
            }
        }
        else
        {
            OnLoadError.Invoke($"The save file {saveFileName} does not exist");
        }
        return false;
    }

    public void SetupNewGame(string saveFileName)
    {
        // Establish the current save file name
        currentSaveFileName = saveFileName;

        // Reset all scriptable objects and then save the game
        foreach (SerealizableScriptableObject gameDataObject in gameDataObjects)
        {
            gameDataObject.ResetForNewGame();
        }
        SaveGame();
    }

    public void DeleteGame(string saveFileName)
    {
        if (SaveFileNameExists(saveFileName))
        {
            string saveFilePath = GeneratePathToSaveFile(saveFileName);
            File.Delete(saveFilePath);
        }
    }

    private BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter;
    }
}
