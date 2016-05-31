using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour
{
    public AstSize AsteroidSize = AstSize.Small;

    public GameObject AsteroidCellPrefab;

    public List<PosRange> PositionAndRangeList = new List<PosRange>();
    
    public Dictionary<Position, GameObject> AsteroidCells = new Dictionary<Position, GameObject>();

    public string AsteroidName = "Aurora Base Delta Pi";
    public string AsteroidShortName = "Aur. B-DP";
    
    public Rect rect;

    void Awake()
    {
    }
    void Start()
    {
        GenerateAsteroid();
    }

    public void GenerateAsteroid()
    {
        PositionAndRangeList.Clear();
        GeneratePosRangeList();
        
        foreach (KeyValuePair<Position, GameObject> cell in AsteroidCells)
        {
            Destroy(cell.Value);
        }
        AsteroidCells.Clear();
        GenerateCellsForAsteroid();
    }
    public void GeneratePosRangeList()
    {
        float astSize = (float)AsteroidSize;

        Vector2 tempStartPos = Vector2.zero;
        float tempStartRange = Random.Range(astSize / 4.0f, astSize / 2.0f);

        PosRange startPosRange = new PosRange(tempStartRange, tempStartPos);
        PositionAndRangeList.Add(startPosRange);


        float posRangeCount = (float)AsteroidSize / 4;//Random.Range((float)AsteroidSize * 0.1f, (float)AsteroidSize * 0.2f);

        for (int i = 0; i < posRangeCount; i++)
        {
            float tempRange = 0;
            Vector2 tempPos = Vector2.zero;

            int count = 2000;
            bool redo = true;

            tempRange = Random.Range(astSize / 5.0f, astSize / 2.0f);

            do
            {
                tempPos = new Vector2(Random.Range(-astSize, astSize), Random.Range(-astSize, astSize));

                // check if close enought to 'center' of asteroid (square check to make asteroid have nicer shape)
                // check left
                if (tempPos.x > -astSize)
                {
                    // check bottom
                    if (tempPos.y > -astSize)
                    {
                        // check right
                        if (tempPos.x < astSize)
                        {
                            // check top
                            if (tempPos.y < astSize)
                            {
                                // check if new position is close enought
                                foreach (PosRange pR in PositionAndRangeList)
                                {
                                    float dist = Vector2.Distance(pR.position, tempPos);
                                    float dist2 = tempRange + pR.range;
                                    
                                    if (Mathf.Abs(dist - dist2) < 1)
                                        redo = false;

                                    if (dist < dist2 - 0.75f)
                                    {
                                        redo = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            while (redo  && count-->0);
            

            PosRange temp = new PosRange(tempRange, tempPos);
            PositionAndRangeList.Add(temp);
        }

        rect = new Rect();
        foreach (PosRange pR in PositionAndRangeList)
        {
            if (pR.position.x - pR.range < rect.xMin)
                rect.xMin = pR.position.x - pR.range;

            if (pR.position.y - pR.range < rect.yMin)
                rect.yMin = pR.position.y - pR.range;

            if (pR.position.x + pR.range > rect.xMax)
                rect.xMax = pR.position.x + pR.range;

            if (pR.position.y + pR.range > rect.yMax)
                rect.yMax = pR.position.y + pR.range;
        }
       

    }
    public void GenerateCellsForAsteroid()
    {
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
                        GameObject temp = Instantiate(AsteroidCellPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                        temp.name = "AsteroidCell (" + x + ", " + y + ")";

                        Position tempPosition = new Position(x, y);
                        AsteroidCells.Add(tempPosition, temp);

                        AsteroidCell cellScript = temp.GetComponent<AsteroidCell>();
                        if (cellScript != null)
                        {
                            cellScript.CellPosition = tempPosition;
                            cellScript.ID = count++;
                            cellScript.ParentAsteroid = gameObject;
                        }
                        else
                            Debug.LogWarning("Cell without Cellscript");

                        break;
                    }
                }
            }
        }
        gameObject.name = "Asteroid (" + AsteroidCells.Count + ")";

    }
    
	void Update ()
    {
	    if(Input.GetKey(KeyCode.R))
        {
            GenerateAsteroid();
        }
	}

    public GameObject GetCellFromCoordinates(Position coordinates)
    {
        GameObject cell;
        if (AsteroidCells.TryGetValue(coordinates, out cell))
            return cell;

        return null;
    }


    public AsteroidData Save()
    {
        AsteroidData astData = new AsteroidData();

        // save the cells
        foreach(KeyValuePair<Position,GameObject> astCellGO in AsteroidCells)
        {
            CellData tempCellData = astCellGO.Value.GetComponent<AsteroidCell>().Save();

            tempCellData.X = astCellGO.Key.x;
            tempCellData.Y = astCellGO.Key.y;

            astData.Cells.Add(tempCellData);
        }

        return astData;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (KeyValuePair<Position, GameObject> cell in AsteroidCells)
        {
            Gizmos.DrawWireCube(cell.Key.GetPosition(), new Vector2(1, 1));
        }
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, new Vector2((float)AsteroidSize*2, (float)AsteroidSize*2));

        foreach (PosRange posRangeItem in PositionAndRangeList)
        {
            Gizmos.DrawWireSphere(new Vector2(posRangeItem.position.x + transform.position.x, posRangeItem.position.y + transform.position.y), posRangeItem.range);
        }


        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(new Vector2(rect.center.x + transform.position.x, rect.center.y + transform.position.y), rect.size);
            
    }
}
