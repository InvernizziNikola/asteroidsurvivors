using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour {

    public enum AstSize
    {
        Small = 5, // max cells = 100
        Normal = 8,  // max cells = 256
        Big = 12, // max cells = 576
        Huge = 18 // max cells = 1296
    }



    public AstSize AsteroidSize = AstSize.Small;

    public GameObject AsteroidPrefab;

    public List<PosRange> PositionAndRangeList = new List<PosRange>();
    
    public Dictionary<Position, GameObject> AsteroidCells = new Dictionary<Position, GameObject>();

    private bool mainAsteroid = false;

    public int ID = -1;

    public bool MainAsteroid {
        get { return mainAsteroid; }
        set { mainAsteroid = value;}
    }

    void Awake()
    {
        GeneratePosRangeList();

    }
    void Start()
    {
    }
    public void Regenerate()
    {
        GeneratePosRangeList();
        GenerateCellsForAsteroid();
    }
    public void GeneratePosRangeList()
    {
        PositionAndRangeList.Clear();

        float astSize = (float)AsteroidSize;


        Vector2 tempStartPos = Vector2.zero; //new Vector2(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f));

        float tempStartRange = Random.Range(astSize / 2.0f, astSize / 1.5f);

        PosRange startPosRange = new PosRange(tempStartRange, tempStartPos);
        PositionAndRangeList.Add(startPosRange);


        float posRangeCount = Random.Range((float)AsteroidSize * 0.1f, (float)AsteroidSize * 0.2f);

        for (int i = 0; i < posRangeCount; i++)
        {

            float tempRange = 0;
            Vector2 tempPos = Vector2.zero;

            bool redo = true;
            do
            {
                tempRange = Random.Range(astSize / 4.0f, astSize / 3.0f);
                tempPos = new Vector2(Random.Range(-astSize - tempRange, astSize - tempRange), Random.Range(-astSize - tempRange, astSize - tempRange));

                foreach (PosRange pR in PositionAndRangeList)
                {
                    if (tempPos.x - tempRange > -astSize && tempPos.y - tempRange > -astSize && tempPos.x + tempRange < astSize && tempPos.y + tempRange < astSize)
                    {
                        if (Vector2.Distance(pR.position, tempPos) < tempRange + pR.range)
                        {
                            redo = false;
                        }
                    }
                }
            }
            while (redo);

            PosRange temp = new PosRange(tempRange, tempPos);
            PositionAndRangeList.Add(temp);
        }
    }
    public void GenerateCellsForAsteroid()
    {
        foreach(KeyValuePair<Position, GameObject> cell in AsteroidCells)
        {
            Destroy(cell.Value);
        }
        AsteroidCells.Clear();


        int minX = int.MaxValue;
        int maxX = int.MinValue;
        int minY = int.MaxValue;
        int maxY = int.MinValue;

        foreach (PosRange posRangeItem in PositionAndRangeList)
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
        int count = 0;
        for (int x = minX - 2; x < maxX + 2; x++)
        {
            for (int y = minY - 2; y < maxY + 2; y++)
            {
                foreach (PosRange posRangeItem in PositionAndRangeList)
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

                        AsteroidCell cellScript = temp.GetComponent<AsteroidCell>();
                        if (cellScript != null)
                            cellScript.ID = count++;
                        else
                            Debug.LogWarning("Cell without Cellscript");
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


            AsteroidCell cellScript = cell.Value.GetComponent<AsteroidCell>();
            if (cellScript != null)
                cellScript.cellN = cellNeighbours;
            else
                Debug.LogWarning("Cell without Cellscript");
        }

        gameObject.name = "Asteroid " + ID + " (" + AsteroidCells.Count + ")";
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

        Gizmos.DrawWireSphere(transform.position, (float)AsteroidSize);

        foreach (PosRange posRangeItem in PositionAndRangeList)
        {
            //Gizmos.DrawWireSphere(new Vector2(posRangeItem.position.x + transform.position.x, posRangeItem.position.y + transform.position.y), posRangeItem.range);
        }        
    }
}
