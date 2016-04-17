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
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            if (!AsteroidList.Contains(asteroid))
                AsteroidList.Add(asteroid);
            

            Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
            if (asteroidScript != null)
            {
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
}

