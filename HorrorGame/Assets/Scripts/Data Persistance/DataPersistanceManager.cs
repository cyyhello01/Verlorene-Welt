using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    private GameData gameData;
    
    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene.");
        }

        instance = this;
    }

    private void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load any saved data from a file using the data handler
        
        // If no data can be loaded, initialized to a new game
        if (this.gameData == null)
        {
            Debug.Log("No saved data was found. Initializing data to defaults.");
            NewGame();
        }
    }

    public void SaveGame()
    {
        
    }

    private void OnApplicationQuit()
    {
        SaveGame(); //?
    }
}
