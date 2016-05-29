using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class GameBehaviour : MonoBehaviour {

    public int GameVersion = 2;
    public GameObject prefabAsteroid;
    private bool StartedNew = false;

    public string FileName = "SaveFile.dat";

    public GameData LoadedGameData;
    public GameData SavedGameData;

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

        gameData.TimePlayed = 32452345;

        // Call save on grid to get all the grid data
        gameData.GridData = Grid.GetInstant.Save();
        // Call save on player to get all the player data
        gameData.PlayerData = Player.GetInstant.Save();

        SavedGameData = gameData;

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

        SaveData loadedData = new SaveData();

        System.Diagnostics.Stopwatch watch01 = new System.Diagnostics.Stopwatch();
        watch01.Start();

        try
        {
            // LOAD
            byte[] key = Convert.FromBase64String(Encryption.CryptoKey);
            using (FileStream file = new FileStream(FileName, FileMode.Open))
            {
                using (CryptoStream cryptoStream = Encryption.CreateDecryptionStream(key, file))
                {
                    loadedData = (SaveData)Encryption.ReadObjectFromStream(cryptoStream);
                }
            }

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // ONLY DESERIALIZE THIS STREAM IF SAVE IS SAME VERSION OR ETC
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (GameVersion == loadedData.FileData.GameVersion)
            {

                try
                {
                    MemoryStream gameDataStream = loadedData.GameDataStream;

                    IFormatter formatter = new BinaryFormatter();
                    gameDataStream.Seek(0, SeekOrigin.Begin);
                    GameData loadedGameData = (GameData)formatter.Deserialize(gameDataStream);

                    LoadedGameData = loadedGameData;
                }
                catch
                {
                    Debug.Log("SAVEFILE VALID, BUT GAMEDATA CORRUPTED!");
                }
            }
        }
        catch
        {
            Debug.Log("SAVEFILE CORRUPTED!");
        }

        watch01.Stop();
        Debug.Log("Loaded in " + watch01.ElapsedMilliseconds + " milliseconds.");
    } 
    void NewGame()
    {
        if (prefabAsteroid != null)
        {
            GameObject tempAsteroid = MonoBehaviour.Instantiate(prefabAsteroid) as GameObject;
            tempAsteroid.transform.position = new Vector3(10, 0);

            // just make the first asteroid the selectedasteroid!
            Grid.GetInstant.SelectedAsteroid = tempAsteroid;
        }
    }
}
