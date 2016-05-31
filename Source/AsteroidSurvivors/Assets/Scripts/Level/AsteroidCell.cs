using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidCell : MonoBehaviour{

    public GameObject ParentAsteroid;
    public Position CellPosition;
    public CellNeighbours CellNeighbours = new CellNeighbours();
    public int ID = -1;


    public void Start()
    {
        FindNeighbours();

        GetComponent<MeshFilter>().mesh = MeshGenerator.GenerateAsteroidCellMesh(CellNeighbours);
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
                        CellNeighbours.Neighbours.Add(neighbour);
                    else
                        CellNeighbours.Neighbours.Add(null);
                }
            }
        }
    }

    public CellData Save()
    {
        CellData cellData = new CellData();

        foreach (GameObject neighbour in CellNeighbours.Neighbours)
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
