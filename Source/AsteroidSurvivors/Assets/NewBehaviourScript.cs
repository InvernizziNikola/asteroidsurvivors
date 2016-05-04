using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System;

[System.Serializable]
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
[System.Serializable]
public class Buildings
{
    public List<Building> buildings = new List<Building>(); 
}

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        


        /*
        IFormatter bf2 = new BinaryFormatter();

        // 1. Construct a SurrogateSelector object
        SurrogateSelector ss2 = new SurrogateSelector();

        Vector3SerializationSurrogate v3ss2 = new Vector3SerializationSurrogate();
        ss2.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3ss2);

        // 2. Have the formatter use our surrogate selector
        bf2.SurrogateSelector = ss2;

        Stream stream2 = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
        bf.Serialize(stream, test2);
        stream.Close();


        // IFormatter formatter = new BinaryFormatter();
        // Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
        // formatter.Serialize(stream, test);
        // stream.Close();

        IFormatter bf = new BinaryFormatter();

        // 1. Construct a SurrogateSelector object
        SurrogateSelector ss = new SurrogateSelector();

        // create surrogate
        Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();

        // add surrogate
        ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3ss);

        // 2. Have the formatter use our surrogate selector
        bf.SurrogateSelector = ss;

        // normal deserialization
        Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        test = (List<Vector3>)bf.Deserialize(stream);
        stream.Close();

        // IFormatter formatter = new BinaryFormatter();
        // Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        // test = (List<Position>)formatter.Deserialize(stream);
        // stream.Close();
        */

        Buildings test = new Buildings();

        test.buildings.Add(new Building());
        test.buildings.Add(new Building());
        test.buildings.Add(new Building());
        test.buildings.Add(new Building());
        test.buildings.Add(new Building());


        byte[] key = Convert.FromBase64String(Encryption.CryptoKey);
        
        using (FileStream file = new FileStream("SAVE.dat", FileMode.Create))
        {
            using (CryptoStream cryptoStream = Encryption.CreateEncryptionStream(key, file))
            {
                Encryption.WriteObjectToStream(cryptoStream, test);
            }
        }


        Buildings newMyVarClass;
        using (FileStream file = new FileStream("SAVE.dat", FileMode.Open))
        using (CryptoStream cryptoStream = Encryption.CreateDecryptionStream(key, file))
        {
            newMyVarClass = (Buildings)Encryption.ReadObjectFromStream(cryptoStream);
        }


        foreach (Building b in newMyVarClass.buildings)
        {
            Debug.Log(b.name);
        }


        /*
        //SAVE
        BinaryFormatter bf = new BinaryFormatter();
        Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
        bf.Serialize(stream, test);
        stream.Close();

        // LOAD
        IFormatter formatter = new BinaryFormatter();
        Stream stream2 = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
        Buildings test2 = (Buildings)formatter.Deserialize(stream2);
        stream2.Close();

        //PRINT
        foreach (Building b in test2.buildings)
        {
            Debug.Log(b.name);
        }
        */
        
    }
}