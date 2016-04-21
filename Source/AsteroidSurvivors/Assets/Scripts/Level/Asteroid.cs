using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour {

    public enum AstSize
    {
        Small = 5,
        Normal = 7, 
        Big = 10,
        Huge = 14
    }

    public Texture2D AsteroidTexture;

    private List<Sprite> AsteroidSpritePieces = new List<Sprite>();

    public AstSize AsteroidSize = AstSize.Small;

    public GameObject AsteroidCellPrefab;

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

        GenerateSpritesList();
    }
    void Start()
    {
    }

    private void GenerateSpritesList()
    {
        float rows = 3;
        float columns = 3;
        for (float y = 0; y < columns; y++)
        {
            for (float x = 0; x < rows; x++)
            {
                Sprite tempSprite = Sprite.Create(AsteroidTexture, 
                    new Rect(
                        x * ((float)AsteroidTexture.width / columns), 
                        y * ((float)AsteroidTexture.height / rows),
                        ((float)AsteroidTexture.width / columns), 
                        ((float)AsteroidTexture.height / rows)), 
                    new Vector2(0.5f,0.5f),
                    512); 
                
                tempSprite.name = x + " - " + y;
                AsteroidSpritePieces.Add(tempSprite);
            }
        }
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

        float tempStartRange = Random.Range(astSize / 4.0f, astSize / 2.0f);

        PosRange startPosRange = new PosRange(tempStartRange, tempStartPos);
        PositionAndRangeList.Add(startPosRange);


        float posRangeCount = (float)AsteroidSize / 4;//Random.Range((float)AsteroidSize * 0.1f, (float)AsteroidSize * 0.2f);

        for (int i = 0; i < posRangeCount; i++)
        {

            float tempRange = 0;
            Vector2 tempPos = Vector2.zero;

            bool redo = true;
            do
            {
                tempRange = Random.Range(astSize / 5.0f, astSize / 2.0f);
                tempPos = new Vector2(Random.Range(-astSize - tempRange, astSize - tempRange), Random.Range(-astSize - tempRange, astSize - tempRange));

                foreach (PosRange pR in PositionAndRangeList)
                {
                    if (tempPos.x - tempRange > -astSize && tempPos.y - tempRange > -astSize && tempPos.x + tempRange < astSize && tempPos.y + tempRange < astSize)
                    {
                        if (Vector2.Distance(pR.position, tempPos) < tempRange + pR.range + 1)
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
                        GameObject temp = Instantiate(AsteroidCellPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                        temp.name = gameObject.name + ": " + x + " - " + y;
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

            // top layer cell neighbours
            if (AsteroidCells.TryGetValue(cell.Key + new Position(-1, 1), out neighbour))
            {
                cellNeighbours.LeftAbove = new KeyValuePair<Position, GameObject>(cell.Key + new Position(-1, 1), neighbour);
                cellNeighbours.HasLeftAbove = true;
            }
            if (AsteroidCells.TryGetValue(cell.Key + new Position(0, 1), out neighbour))
            {
                cellNeighbours.Above = new KeyValuePair<Position, GameObject>(cell.Key + new Position(0, 1), neighbour);
                cellNeighbours.HasAbove = true;
            }

            if (AsteroidCells.TryGetValue(cell.Key + new Position(1, 1), out neighbour))
            {
                cellNeighbours.RightAbove = new KeyValuePair<Position, GameObject>(cell.Key + new Position(1, 1), neighbour);
                cellNeighbours.HasRightAbove = true;
            }

            // middle layer cell neighbours
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
            
            // bottom layer cell neighbours
            if (AsteroidCells.TryGetValue(cell.Key + new Position(-1, -1), out neighbour))
            {
                cellNeighbours.LeftBelow = new KeyValuePair<Position, GameObject>(cell.Key + new Position(-1, -1), neighbour);
                cellNeighbours.HasLeftBelow = true;
            }
            if (AsteroidCells.TryGetValue(cell.Key + new Position(0, -1), out neighbour))
            {
                cellNeighbours.Below = new KeyValuePair<Position, GameObject>(cell.Key + new Position(0, -1), neighbour);
                cellNeighbours.HasBelow = true;
            }
            if (AsteroidCells.TryGetValue(cell.Key + new Position(1, -1), out neighbour))
            {
                cellNeighbours.RightBelow = new KeyValuePair<Position, GameObject>(cell.Key + new Position(1, -1), neighbour);
                cellNeighbours.HasRightBelow = true;
            }

            AsteroidCell cellScript = cell.Value.GetComponent<AsteroidCell>();
            if (cellScript != null)
            {
                cellScript.cellN = cellNeighbours;

                // TODO ADD ALL OPTIONS!
                if (cellNeighbours.HasAbove == false
                    && cellNeighbours.HasLeft == false && cellNeighbours.HasRight == true
                    && cellNeighbours.HasBelow == true)
                {
                    cellScript.AsteroidSprite = AsteroidSpritePieces[0];
                }
                else if (cellNeighbours.HasAbove == false
                    && cellNeighbours.HasLeft == true && cellNeighbours.HasRight == true
                    && cellNeighbours.HasBelow == true)
                {
                    cellScript.AsteroidSprite = AsteroidSpritePieces[1];
                }
                else if (cellNeighbours.HasAbove == false
                    && cellNeighbours.HasLeft == true && cellNeighbours.HasRight == false
                    && cellNeighbours.HasBelow == true)
                {
                    cellScript.AsteroidSprite = AsteroidSpritePieces[2];
                }
                else if (cellNeighbours.HasAbove == true
                   && cellNeighbours.HasLeft == false && cellNeighbours.HasRight == true
                       && cellNeighbours.HasBelow == true)
                {
                    cellScript.AsteroidSprite = AsteroidSpritePieces[3];
                }
                else if (cellNeighbours.HasAbove == true
                   && cellNeighbours.HasLeft == true && cellNeighbours.HasRight == true
                       && cellNeighbours.HasBelow == true)
                {
                    cellScript.AsteroidSprite = AsteroidSpritePieces[4];
                }
                else if (cellNeighbours.HasAbove == true
                   && cellNeighbours.HasLeft == true && cellNeighbours.HasRight == false
                       && cellNeighbours.HasBelow == true)
                {
                    cellScript.AsteroidSprite = AsteroidSpritePieces[5];
                }
                else if (cellNeighbours.HasAbove == true
                   && cellNeighbours.HasLeft == false && cellNeighbours.HasRight == true
                       && cellNeighbours.HasBelow == false)
                {
                    cellScript.AsteroidSprite = AsteroidSpritePieces[6];
                }
                else if (cellNeighbours.HasAbove == true
                   && cellNeighbours.HasLeft == true && cellNeighbours.HasRight == true
                       && cellNeighbours.HasBelow == false)
                {
                    cellScript.AsteroidSprite = AsteroidSpritePieces[7];
                }
                else if (cellNeighbours.HasAbove == true
                   && cellNeighbours.HasLeft == true && cellNeighbours.HasRight == false
                       && cellNeighbours.HasBelow == false)
                {
                    cellScript.AsteroidSprite = AsteroidSpritePieces[8];
                }
                else
                {
                    // put middle piece if none is selected
                    cellScript.AsteroidSprite = AsteroidSpritePieces[4];
                }


                cellScript.SetSpriteRenderer();

            }
            else
                Debug.LogWarning("Cell without Cellscript");
        }
        gameObject.name = "Asteroid " + ID + " (" + AsteroidCells.Count + ")";
        
    }
    
	void Update () {
	    if(Input.GetKeyDown(KeyCode.R))
        {
            Regenerate();
        }
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
    }
}
