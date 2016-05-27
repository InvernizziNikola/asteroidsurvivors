using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System;

[Serializable]
public class Building
{
    public int id = 1;
    public string name = "Power Plant";
    public string type = "Power";
    public int generates = 10000;
    public int usage = 0;

    public override string ToString()
    {
        return name;
    }
}

[Serializable]
public class Buildings
{
    public List<Building> buildings = new List<Building>();
}



////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////



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
[Serializable]
public class CellData
{
    public int CellId;
    public int x;
    public int y;
}
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
public class PlayerSettingsData
{
    public int CameraZoom;
    public int CameraLocationX;
    public int CameraLocationY;

    // etc etc
}


[Serializable]
public class GameData
{
    public string fileName;
    public string LastSaveDate;

    public int SecondsPlayTime;

    public List<AsteroidData> AsteroidsData = new List<AsteroidData>();
    public PlayerSettingsData PlayerSettings = new PlayerSettingsData();

    public override string ToString()
    {
        string returnString = fileName;
        returnString += " - Lastsaved: " + LastSaveDate;

        return returnString;
    }
}

////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////


public class NewBehaviourScript : MonoBehaviour
{

    public GameData GameDataLoaded;
    public GameData GameDataTest;

    void Start()
    {


        /*GameDataTest = new GameData();

        GameDataTest.fileName = "SAVE";
        GameDataTest.LastSaveDate = "NOW";
        GameDataTest.SecondsPlayTime = 321321;

        // first asteroid
        AsteroidData AstDataTest = new AsteroidData();
        AstDataTest.AsteroidId = 5;
        AstDataTest.Name = "4W3S0M3";
        AstDataTest.StationName = "NIKOLA I - 4W3S0M3";

        CellData CellData_01 = new CellData();
        CellData_01.CellId = 1;
        CellData_01.x = 1;
        CellData_01.y = 1;

        CellData CellData_02 = new CellData();
        CellData_02.CellId = 2;
        CellData_02.x = 2;
        CellData_02.y = 1;

        AstDataTest.Cells.Add(CellData_01);
        AstDataTest.Cells.Add(CellData_02);


        GameDataTest.AsteroidsData.Add(AstDataTest);
        */

        // SAVE
        byte[] key = Convert.FromBase64String(Encryption.CryptoKey);
        
        using (FileStream file = new FileStream(GameDataTest.fileName + ".dat", FileMode.Create))
        {
            using (CryptoStream cryptoStream = Encryption.CreateEncryptionStream(key, file))
            {
                Encryption.WriteObjectToStream(cryptoStream, GameDataTest);
            }
        }


        // LOAD
        string fileToLoad = "TEST";

        using (FileStream file = new FileStream(fileToLoad + ".dat", FileMode.Open))
        {
            using (CryptoStream cryptoStream = Encryption.CreateDecryptionStream(key, file))
            {
                GameDataLoaded = (GameData)Encryption.ReadObjectFromStream(cryptoStream);

                if (GameDataLoaded.fileName == fileToLoad)
                    Debug.Log(GameDataLoaded.ToString());
                else
                {
                    Debug.Log("ERROR LOADING: SaveFileName: " + fileToLoad + " - SaveFileInternalName: " + GameDataLoaded.fileName);
                    GameDataLoaded = new GameData();
                }
            }
        }        
    }
}