using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Grid : MonoBehaviour {

    public GameObject prefabAsteroid;

    public List<GameObject> AsteroidList = new List<GameObject>();
    
    public GameObject selectedAsteroid;

    public GameObject SelectedAsteroid
    {
        get
        {
            return selectedAsteroid;
        }
        set
        {
            selectedAsteroid = value;
        }
    }

    void Start ()
    {
        if (prefabAsteroid != null)
        {
            GameObject tempAsteroid = Instantiate(prefabAsteroid) as GameObject;
            tempAsteroid.transform.position = new Vector3(0, 0);
            tempAsteroid.GetComponent<Asteroid>().GenerateCellsForAsteroid();


            AddAsteroid(tempAsteroid);

            // just make the first asteroid the selectedasteroid!
            selectedAsteroid = tempAsteroid;
        }
    }    

    public void AddAsteroid(GameObject newAsteroid)
    {
        AsteroidList.Add(newAsteroid);
    }
   

    void Update()
    {
        
    }

    public void OnDrawGizmos()
    {
    }


    public void Load()
    {

    }
    public void Save()
    {

        /*
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
        foreach (GameObject asteroid in AsteroidList)
        {
            Asteroid asteroidData = asteroid.GetComponent<Asteroid>();
            if (asteroidData != null)
            {



            }
        }
        */
    }
}

