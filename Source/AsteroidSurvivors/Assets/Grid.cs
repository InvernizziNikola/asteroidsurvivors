using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public struct posRange
{
    public float range;
    public Vector2 position;
}

[System.Serializable]
public struct position
{
    public position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    int x;
    int y;
}
public class Grid : MonoBehaviour {
    
    private Plane plane = new Plane(Vector3.forward, 0);
    public int GridWidth = 100;
    public int GridHeight = 100;

    private List<List<GameObject>> GridList = new List<List<GameObject>>();

    private List<GameObject> AsteroidList = new List<GameObject>();


    private Dictionary<position, GameObject> CellList = new Dictionary<position, GameObject>();


    void Start ()
    {
        // CREATE GRID WITH NULL OBJECTS
        for (int i = 0; i < GridWidth; i++)
        {
            GridList.Add(new List<GameObject>());
            for (int j = 0; j < GridHeight; j++)
            {
                GridList[i].Add(null);
            }
        }

        CellList.ContainsKey(new position(1,2));
    }

    public void SetAsteroidCells(Dictionary<Vector2, GameObject> cells)
    {
        foreach (KeyValuePair<Vector2, GameObject> cell in cells)
        {
            int x = (int)cell.Key.x;
            int y = (int)cell.Key.y;

            if(x >= 0 && y >= 0 && x <= GridWidth && y <= GridHeight)
                GridList[x][y] = cell.Value;
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
            

            if (mousePos.x >= 0 && mousePos.y >= 0 && mousePos.x <= GridWidth && mousePos.y <= GridHeight)
            {
                GameObject mouseOverCell = GridList[(int)mousePos.x][(int)mousePos.y];
                Debug.Log(mouseOverCell);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(GridWidth/2.0f, GridHeight/2.0f), new Vector2(GridWidth, GridHeight));
    }
}

