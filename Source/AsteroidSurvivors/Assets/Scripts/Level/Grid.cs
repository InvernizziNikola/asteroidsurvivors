using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Grid : MonoBehaviour {
    
    private Plane plane = new Plane(Vector3.forward, 0);

    public GameObject MainAsteroid;

    public List<GameObject> AsteroidList = new List<GameObject>();

    private Dictionary<Position, GameObject> CellList = new Dictionary<Position, GameObject>();

    public List<int> HugeList = new List<int>();
    public List<int> SmallList = new List<int>();
    public List<int> BigList = new List<int>();
    public List<int> NormalList = new List<int>();

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


        if(Input.GetKey(KeyCode.R))
        {
            CellList.Clear();
            foreach (GameObject asteroid in AsteroidList)
            {
                Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
                if (asteroidScript != null)
                {
                    if (asteroid == MainAsteroid)
                        asteroidScript.MainAsteroid = true;

                    asteroidScript.Regenerate();
                    SetAsteroidCells(asteroidScript.AsteroidCells);

                    if (asteroidScript.AsteroidSize == Asteroid.AstSize.Small)
                    {
                        SmallList.Add(asteroidScript.AsteroidCells.Count);
                    }
                    if (asteroidScript.AsteroidSize == Asteroid.AstSize.Normal)
                    {
                        NormalList.Add(asteroidScript.AsteroidCells.Count);
                    }
                    if (asteroidScript.AsteroidSize == Asteroid.AstSize.Big)
                    {
                        BigList.Add(asteroidScript.AsteroidCells.Count);
                    }
                    if (asteroidScript.AsteroidSize == Asteroid.AstSize.Huge)
                    {
                        HugeList.Add(asteroidScript.AsteroidCells.Count);
                    }
                }
            }


            Debug.ClearDeveloperConsole();

            int total = 0;
            foreach (int count in SmallList)
            {
                total += count;
            }
            Debug.Log("Average cells small ast.: " + ((float)total / (float)SmallList.Count));


            total = 0;
            foreach (int count in NormalList)
            {
                total += count;
            }
            Debug.Log("Average cells normal ast.: " + ((float)total / (float)NormalList.Count));

            total = 0;
            foreach (int count in BigList)
            {
                total += count;
            }
            Debug.Log("Average cells big ast.: " + ((float)total / (float)BigList.Count));


            total = 0;
            foreach (int count in HugeList)
            {
                total += count;
            }
            Debug.Log("Average cells Huge ast.: " + ((float)total / (float)HugeList.Count));
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

    }
}

