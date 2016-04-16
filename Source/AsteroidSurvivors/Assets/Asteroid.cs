using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour {

    public GameObject GridGameObject;
    private Grid GridScript;

    public List<posRange> posRangeList = new List<posRange>();
    public List<GameObject> AsteroidCells = new List<GameObject>();

    void Start()
    {
        if (GridGameObject != null)
        {
            GridScript = GridGameObject.GetComponent<Grid>();

            if (GridScript != null)
                GridScript.AddAsteroid(gameObject);
        }

        CreateAsteroid();
    }
	
    public void CreateAsteroid()
    {
        int minX = int.MaxValue;
        int maxX = 0;
        int minY = int.MaxValue;
        int maxY = 0;

        foreach (posRange posRangeItem in posRangeList)
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


        Dictionary<Vector2, GameObject> asteroidCellsForGrid = new Dictionary<Vector2, GameObject>();

        for (int x = minX-2; x < maxX+2; x++)
        {
            for (int y = minY-2; y < maxY+2; y++)
            {
                foreach (posRange posRangeItem in posRangeList)
                {
                    Bounds cellBounds = new Bounds(new Vector2(x, y), new Vector2(1, 1));

                    Vector2 pos = posRangeItem.position + new Vector2(transform.position.x, transform.position.y);
                    float closestPointDist = Vector2.Distance(pos, cellBounds.ClosestPoint(pos));

                    if (closestPointDist < posRangeItem.range)
                    {
                        GameObject temp = new GameObject(gameObject.name + ": " + x + " - " + y);
                        temp.transform.position = new Vector2(x, y);
                        AsteroidCells.Add(temp);
                        asteroidCellsForGrid.Add(new Vector2(x, y), temp);

                        break;
                    }
                }
            }
        }

        GridScript.SetAsteroidCells(asteroidCellsForGrid);
    }



	// Update is called once per frame
	void Update () {
	
	}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (GameObject cell in AsteroidCells)
        {
            Gizmos.DrawWireCube(cell.transform.position, new Vector2(1, 1));
        }
        Gizmos.color = Color.red;
        foreach (posRange posRangeItem in posRangeList)
        {
            Gizmos.DrawWireSphere(new Vector2(posRangeItem.position.x + transform.position.x, posRangeItem.position.y + transform.position.y), posRangeItem.range);
        }        
    }
}
