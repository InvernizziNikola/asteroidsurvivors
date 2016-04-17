using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour {

    public GameObject AsteroidPrefab;

    public List<posRange> PositionAndRangeList = new List<posRange>();
    
    public Dictionary<Position, GameObject> AsteroidCells = new Dictionary<Position, GameObject>();

    private bool mainAsteroid = false;
    
    public bool MainAsteroid {
        get { return mainAsteroid; }
        set { mainAsteroid = value;}
    }
    

    void Start()
    {
    }

    public void GenerateCellsForAsteroid()
    {

        int minX = int.MaxValue;
        int maxX = int.MinValue;
        int minY = int.MaxValue;
        int maxY = int.MinValue;

        foreach (posRange posRangeItem in PositionAndRangeList)
        {
            Vector2 pos = posRangeItem.position + new Vector2(transform.position.x, transform.position.y);
            float range = posRangeItem.range;

            if (pos.x - range < minX)
                minX = (int)(pos.x - range);

            if (pos.x + range > maxX)
                maxX = (int)(pos.x + range);

            if (pos.y - range < minY)
                minY = (int)(pos.y - range);

            if (pos.y + range > maxY)
                maxY = (int)(pos.y + range);
        }

        for (int x = minX - 2; x < maxX + 2; x++)
        {
            for (int y = minY - 2; y < maxY + 2; y++)
            {
                foreach (posRange posRangeItem in PositionAndRangeList)
                {
                    Bounds cellBounds = new Bounds(new Vector2(x, y), new Vector2(1, 1));

                    Vector2 pos = posRangeItem.position + new Vector2(transform.position.x, transform.position.y);
                    float closestPointDist = Vector2.Distance(pos, cellBounds.ClosestPoint(pos));

                    if (closestPointDist < posRangeItem.range)
                    {
                        GameObject temp = Instantiate(AsteroidPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                        temp.name = gameObject.name + ": " + x + " - " + y;
                        temp.transform.parent = gameObject.transform;
                        AsteroidCells.Add(new Position(x, y), temp);

                        break;
                    }
                }
            }
        }

        foreach (KeyValuePair<Position, GameObject> cell in AsteroidCells)
        {
            CellNeighbours cellNeighbours = new CellNeighbours();

            GameObject neighbour;
            if (AsteroidCells.TryGetValue(cell.Key + new Position(-1, 0), out neighbour))
            {
                cellNeighbours.Left = new KeyValuePair<Position, GameObject>(cell.Key + new Position(-1, 0), neighbour);
                cellNeighbours.HasLeft = true;
            }
            if (AsteroidCells.TryGetValue(cell.Key + new Position(1, 0), out neighbour))
            {
                cellNeighbours.Right = new KeyValuePair<Position, GameObject>(cell.Key + new Position(1, 0), neighbour);
                cellNeighbours.HasRight = true;
            }
            if (AsteroidCells.TryGetValue(cell.Key + new Position(0, 1), out neighbour))
            {
                cellNeighbours.Above = new KeyValuePair<Position, GameObject>(cell.Key + new Position(0, 1), neighbour);
                cellNeighbours.HasAbove = true;
            }
            if (AsteroidCells.TryGetValue(cell.Key + new Position(0, -1), out neighbour))
            {
                cellNeighbours.Below = new KeyValuePair<Position, GameObject>(cell.Key + new Position(0, -1), neighbour);
                cellNeighbours.HasBelow = true;
            }

            cell.Value.GetComponent<AsteroidCell>().cellN = cellNeighbours;
        }



    }
    
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (KeyValuePair<Position, GameObject> cell in AsteroidCells)
        {
            Gizmos.DrawWireCube(cell.Key.GetPosition(), new Vector2(1, 1));
        }
        Gizmos.color = Color.red;
        foreach (posRange posRangeItem in PositionAndRangeList)
        {
            Gizmos.DrawWireSphere(new Vector2(posRangeItem.position.x + transform.position.x, posRangeItem.position.y + transform.position.y), posRangeItem.range);
        }        
    }
}
