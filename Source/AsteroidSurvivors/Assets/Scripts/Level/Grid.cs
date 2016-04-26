using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Grid : MonoBehaviour {
    
    private Plane plane = new Plane(Vector3.forward, 0);

    public GameObject MainAsteroid;

    public List<GameObject> AsteroidList = new List<GameObject>();

    private Dictionary<Position, GameObject> CellList = new Dictionary<Position, GameObject>();
    


    void Start ()
    {
        int iDCounter = 0;
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            if (!AsteroidList.Contains(asteroid))
                AsteroidList.Add(asteroid);
            

            Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
            if (asteroidScript != null)
            {
                asteroidScript.ID = iDCounter++;
                if (asteroid == MainAsteroid)
                    asteroidScript.MainAsteroid = true;

                asteroidScript.GenerateCellsForAsteroid();
                SetAsteroidCells(asteroidScript.AsteroidCells);
            }
        }
    }

    public void SetAsteroidCells(Dictionary<Position, GameObject> cells)
    {
        foreach (KeyValuePair<Position, GameObject> cell in cells)
        {
            if(!CellList.ContainsKey(cell.Key))
            {
                CellList.Add(cell.Key, cell.Value);
            }
            else
            {
                Position position = cell.Key;
                Debug.LogWarning("Cell" + position + " already exists, still updating it to new value");
                CellList[position] = cell.Value;
            }
        }
    }        

    public void AddAsteroid(GameObject newAsteroid)
    {
        AsteroidList.Add(newAsteroid);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = new Vector2();

            float dist;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out dist))
            {
                mousePos = ray.GetPoint(dist);
            }

            mousePos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            
            GameObject mouseOverCell;
            if(CellList.TryGetValue(new Position((int)mousePos.x, (int)mousePos.y), out mouseOverCell))
            {
                Debug.Log("Selected: " + mouseOverCell.name + " with neighbour cells: " + mouseOverCell.GetComponent<AsteroidCell>().cellN);
            }
        }
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

