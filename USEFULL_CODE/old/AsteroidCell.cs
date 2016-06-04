using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidCell : MonoBehaviour{

    public GameObject ParentAsteroid;
    public Position CellPosition;
    public int ID = -1;

    public List<GameObject> neighbours = new List<GameObject>();
    public List<GameObject> Neighbours { get { return neighbours; } }

    public void Start()
    {
        FindNeighbours();

        GetComponent<MeshFilter>().mesh = MeshGenerator.GenerateAsteroidCellMesh(neighbours);
    }


    public void FindNeighbours()
    {
        Dictionary<Position, GameObject> asteroidCells = ParentAsteroid.GetComponent<Asteroid>().AsteroidCells;

        GameObject neighbour;

        for (int y = 1; y >= -1; y--)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (!(x == 0 && y == 0))
                {
                    if (asteroidCells.TryGetValue(CellPosition + new Position(x, y), out neighbour))
                        Neighbours.Add(neighbour);
                    else
                        Neighbours.Add(null);
                }
            }
        }
    }

    public CellData Save()
    {
        CellData cellData = new CellData();

        foreach (GameObject neighbour in neighbours)
        {
            cellData.CellNeighbours.Neighbours.Add(neighbour == null ? false : true);
        }

        return cellData;
    }

    public CellData Load()
    {
        return null;
    }
}
