using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System;
using System.Diagnostics;

////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////


// GRID
[Serializable]
public class GridData
{
    public List<AsteroidData> AsteroidsData = new List<AsteroidData>();
}

// ASTEROIDS, ASTEROIDCELLS & BUILDINGS
[Serializable]
public class AsteroidData
{
    public int AsteroidId;
    public string Name;
    public string StationName;
    public List<CellData> Cells = new List<CellData>();
    public List<BuildingData> Buildings = new List<BuildingData>();

    public PlayerControlledObjectsData PlayerControlledObjects = new PlayerControlledObjectsData();
}

// CELLDATA
[Serializable]
public class CellData
{
    public CellNeighboursData CellNeighbours;
    public int X;
    public int Y;
}
[Serializable]
public struct CellNeighboursData
{
    public CellNeighboursData(bool leftAbove, bool middleAbove, bool rightAbove, bool leftMiddle, bool rightMiddle, bool leftBottom, bool middleBottom, bool rightBottom)
    {
        LeftAbove = leftAbove;
        MiddleAbove = middleAbove;
        RightAbove = rightAbove;

        LeftMiddle = leftMiddle;
        RightMiddle = rightMiddle;

        LeftBottom = leftBottom;
        MiddleBottom = middleBottom;
        RightBottom = rightBottom;
    }

    public bool LeftAbove;
    public bool MiddleAbove;
    public bool RightAbove;
    public bool LeftMiddle;
    public bool RightMiddle;
    public bool LeftBottom;
    public bool MiddleBottom;
    public bool RightBottom;
}

// BUILDING DATA
[Serializable]
public class BuildingData
{
    public int BuildingId;
    public string Type;
}


// CHARACTERS, DRONES & SHIPS
[Serializable]
public class PlayerControlledObjectsData
{
    public List<CharacterData> Characters = new List<CharacterData>();
    public List<ShipData> Ships = new List<ShipData>();
    public List<DroneData> Drones = new List<DroneData>();
}
[Serializable]
public class CharacterData
{
    public int CharacterId;
    public int AsteroidLocatedId;
    public int CellLocatedId;

    public string FirstName;
    public string Lastname;
    public int Level;

    public int HealthPoints;
    public int Happiness;
}
[Serializable]
public class ShipData
{
    public int ShipId;
    public int AsteroidLocatedId;
    public int LocationX;
    public int LocationY;

    public string Name;

    public int Hull;
    public int Energy;
}
[Serializable]
public class DroneData
{
    public int DroneID;
    public int AsteroidLocatedId;
    public int LocationX;
    public int LocationY;

    public string Name;

    public int Energy;
}

// PlayerSettings
[Serializable]
public class PlayerData
{
    public int CameraZoom;
    public int CameraLocationX;
    public int CameraLocationY;
    
}

// CONTAINER FOR DATA ABOUT FILE IT SELF
[Serializable]
public class FileData
{
    public int GameVersion = -1;
    public string FileName;
    public string SaveDate;
}

// CONTAINER WITH ALL THE GAME DATA
[Serializable]
public class GameData
{
    public int TimePlayed;

    public GridData GridData = new GridData();
    public PlayerData PlayerData = new PlayerData();
}

// CONTAINER FOR ALL DATA THAT HAS TO BE SAVED
[Serializable]
public class SaveData
{
    public FileData FileData = new FileData();
    // contains all data from the saved game
    public MemoryStream GameDataStream;
}

////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////

/*

public class NewBehaviourScript : MonoBehaviour
{

public GameData GameDataLoaded;

void Start()
{


    GameData GameDataTest = new GameData();

    GameDataTest.fileName = "SAVE";
    GameDataTest.LastSaveDate = "NOW";
    GameDataTest.SecondsPlayTime = 321321;

    // first asteroid
    AsteroidData AstDataTest = new AsteroidData();
    AstDataTest.AsteroidId = 5;
    AstDataTest.Name = "4W3S0M3";
    AstDataTest.StationName = "NIKOLA I - 4W3S0M3";

    for (int i = 0; i < 1000; i++)
    {

        CellData CellData_02 = new CellData();
        CellData_02.CellId = 2 + i;
        CellData_02.x = 2 + i;
        CellData_02.y = 1 + i;

        AstDataTest.Cells.Add(CellData_02);
    }


    GameDataTest.GridData.AsteroidsData.Add(AstDataTest);


    Stopwatch watch01 = new Stopwatch();
    watch01.Start();
    // SAVE
    byte[] key = Convert.FromBase64String(Encryption.CryptoKey);

    using (FileStream file = new FileStream(GameDataTest.fileName + ".dat", FileMode.Create))
    {
        using (CryptoStream cryptoStream = Encryption.CreateEncryptionStream(key, file))
        {
            Encryption.WriteObjectToStream(cryptoStream, GameDataTest);
        }
    }
    watch01.Stop();
    //UnityEngine.Debug.Log("SAVING TOOK(in milli): " + watch01.ElapsedMilliseconds);



    watch01.Reset();
    watch01.Start();
    // LOAD
    string fileToLoad = "SAVE";

    using (FileStream file = new FileStream(fileToLoad + ".dat", FileMode.Open))
    {
        using (CryptoStream cryptoStream = Encryption.CreateDecryptionStream(key, file))
        {
            GameDataLoaded = (GameData)Encryption.ReadObjectFromStream(cryptoStream);

            if (GameDataLoaded.fileName == fileToLoad)
                UnityEngine.Debug.Log(GameDataLoaded.ToString());
            else
            {
                UnityEngine.Debug.Log("ERROR LOADING: SaveFileName: " + fileToLoad + " - SaveFileInternalName: " + GameDataLoaded.fileName);
                GameDataLoaded = new GameData();
            }
        }
    }
    watch01.Stop();
    //UnityEngine.Debug.Log("LOADING TOOK(in milli): " + watch01.ElapsedMilliseconds);
}
}
*/
