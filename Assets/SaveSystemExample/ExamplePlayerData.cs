using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExamplePlayerData", menuName = "ScriptableObjects/State/ExamplePlayerData")]

[System.Serializable]
public class ExamplePlayerData : SerealizableScriptableObject
{
    public string name;
    public int level;
    public float experience;
    public List<string> inventory;

    [System.Serializable]
    public struct PlayerData
    {
        public string name;
        public int level;
        public float experience;
        public List<string> inventory;
    }

    public void AddToLevel(int amount)
    {
        level += amount;
    }

    public void AddToExperience(float amount)
    {
        experience += amount;
    }

    public void AddToName(string namepiece)
    {
        name += namepiece;
    }

    public void SubtractFromName()
    {
        if (name.Length > 2)
            name = name.Substring(1);
    }

    public void AddToInventory(string item)
    {
        inventory.Add(item);
    }

    public void RemoveFromInventory()
    {
        if (inventory.Count > 0)
            inventory.RemoveAt(0);
    }

    public override object SaveData()
    {
        PlayerData saveData = new PlayerData();
        saveData.name = name;
        saveData.level = level;
        saveData.experience = experience;
        saveData.inventory = inventory;
        return saveData;
}

    public override void LoadData(object data)
    {
        PlayerData loadedData = (PlayerData)data;
        name = loadedData.name;
        level = loadedData.level ;
        experience = loadedData.experience;
        inventory = loadedData.inventory;

    }

    public override void ResetForNewGame()
    {
        name = "Bob";
        level = 0;
        experience = 0;
        inventory = new List<string>();
    }
}
