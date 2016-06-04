using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GameBehaviour : MonoBehaviour {

    public int GameVersion = 2;
    public GameObject prefabBunker;
    private bool StartedNew = false;

    public string FileName = "SaveFile.dat";


    public Dictionary<Vector2, string> DictTest = new Dictionary<Vector2, string>();

    // Use this for initialization
    void Start ()
    {



    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.N) && !StartedNew)
        {
            StartedNew = !StartedNew;
            NewGame();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

    }

    void Save()
    {
        // Create new object to save
        SaveData saveData = new SaveData();

        // Fill in data
        saveData.FileData.FileName = FileName;
        saveData.FileData.GameVersion = GameVersion;
        saveData.FileData.SaveDate = DateTime.UtcNow.ToString();

        // Create the container for all gamedata
        GameData gameData = new GameData();
        
        // Call save on grid to get all the grid data
        //gameData.GridData = Grid.GetInstant.Save();
        // Call save on player to get all the player data
        gameData.PlayerData = Player.GetInstant.Save();
        
        // Make a binary stream of the gamedata
        MemoryStream stream = new MemoryStream();
        IFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gameData);

        // add stream to savedata
        saveData.GameDataStream = stream;
        
        // SAVE
        byte[] key = Convert.FromBase64String(Encryption.CryptoKey);
        using (FileStream file = new FileStream(FileName, FileMode.Create))
        {
            using (CryptoStream cryptoStream = Encryption.CreateEncryptionStream(key, file))
            {
                Encryption.WriteObjectToStream(cryptoStream, saveData);
            }
        }

    }
    void Load()
    {
        try
        {
            if (File.Exists(FileName))
            {
                SaveData loadedData = new SaveData();

                // LOAD
                byte[] key = Convert.FromBase64String(Encryption.CryptoKey);
                using (FileStream file = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    using (CryptoStream cryptoStream = Encryption.CreateDecryptionStream(key, file))
                    {
                        loadedData = (SaveData)Encryption.ReadObjectFromStream(cryptoStream);
                    }
                }


                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //    ONLY DESERIALIZE THIS STREAM IF SAVE IS SAME VERSION
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                if (GameVersion == loadedData.FileData.GameVersion)
                {
                    try
                    {
                        MemoryStream gameDataStream = loadedData.GameDataStream;

                        IFormatter formatter = new BinaryFormatter();
                        gameDataStream.Seek(0, SeekOrigin.Begin);
                        GameData gameData = (GameData)formatter.Deserialize(gameDataStream);

                        if (gameData != null)
                            LoadGameData(gameData);
                        else
                            Debug.Log("Gamedata was empty!");
                    }
                    catch
                    {
                        Debug.Log("Saved gamedata in file isn't valid!");
                    }
                }
                else
                {
                    Debug.Log("Savefile version(" + loadedData.FileData.GameVersion + ") is not the same a the version of the game(" + GameVersion +")");
                }
            }
            else
            {
                Debug.Log("File doesn't exist!");
            }
        }
        catch
        {
            Debug.Log("Couldn't open file! File is probably corrupted!");
        }
    } 

    public void LoadGameData(GameData gameData)
    {
        // load grid data into the grid
        Grid.GetInstant.Load(gameData.GridData);

        // load player data into the player
        Player.GetInstant.Load(gameData.PlayerData);
    }


    void NewGame()
    {
        // Create first Bunker
        
        if (prefabBunker != null)
        {
            // just make the first asteroid the selectedasteroid!
            Grid.GetInstant.SelectedBunker = Grid.GetInstant.CreateBunker(prefabBunker);
        }
        
    }
}
