using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;

public class PlayerInputs : MonoBehaviour {


    public List<Vector3> test = new List<Vector3>();

    // Use this for initialization
    void Start()
    {     
    }
	
	// Update is called once per frame
	void Update () {

       
        if (Input.GetKeyUp(KeyCode.L))
        {
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
            
        }

        if (Input.GetKeyUp(KeyCode.S))
        {

            IFormatter bf = new BinaryFormatter();

            // 1. Construct a SurrogateSelector object
            SurrogateSelector ss = new SurrogateSelector();

            Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();
            ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3ss);

            // 2. Have the formatter use our surrogate selector
            bf.SurrogateSelector = ss;
            
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            bf.Serialize(stream, test);
            stream.Close();

            
           // IFormatter formatter = new BinaryFormatter();
           // Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
           // formatter.Serialize(stream, test);
           // stream.Close();
            
        }
        

    }
    
}
