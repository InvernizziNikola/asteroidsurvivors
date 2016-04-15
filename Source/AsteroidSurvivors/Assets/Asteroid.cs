using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public struct posRange
{
    public float range;
    public Vector2 position;
}

public class Asteroid : MonoBehaviour {

    static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);

    public Bounds maxSize = new Bounds(Vector3.zero, new Vector3(20,20, 0));

    public List<posRange> posRangeList = new List<posRange>();

    public List<GridCell> Grid = new List<GridCell>();


    void Start ()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                foreach (posRange posRangeItem in posRangeList)
                {
                    Bounds gridBox = new Bounds(new Vector3(i, j, 0), new Vector3(1,1,0));
                    
                    if (Vector3.Distance(posRangeItem.position, gridBox.ClosestPoint(posRangeItem.position)) < posRangeItem.range)
                    {
                        GridCell tempGridCell = new GridCell(new Vector3(i, j, 0));
                        Grid.Add(tempGridCell);
                        tempGridCell.IsAsteroid = true;
                        break;
                    }
                }
            }
        }
	}

    void Update()
    {
        if (Grid == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if(hit.collider != null && hit.collider.gameObject != null)
                Debug.Log(hit.collider.gameObject);
            /* Vector3 mousePos = GetMousePositionOnXZPlane();

             if (mousePos.x > -0.5f && mousePos.x < 19 && mousePos.z > -0.5f && mousePos.z < 19)
             {
                 mousePos = new Vector3(Mathf.Round(mousePos.x), 0, Mathf.Round(mousePos.z));

                 GridCell mouseOverCell = Grid[(int)mousePos.x][(int)mousePos.z];

                 if (!mouseOverCell.IsSelected)
                     mouseOverCell.IsSelected = true;
                 else
                     mouseOverCell.IsSelected = false;
             }*/
        }
    }
    public static Vector3 GetMousePositionOnXZPlane()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (XZPlane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            //Just double check to ensure the y position is exactly zero
            hitPoint.y = 0;
            return hitPoint;
        }
        return Vector3.zero;
    }

    void OnDrawGizmos()
    {
        foreach (posRange posRangeItem in posRangeList)
        {
            Gizmos.DrawWireSphere(posRangeItem.position, posRangeItem.range);
        }
        if (Grid == null)
            return;

        foreach (GridCell gridCell in Grid)
        {
            gridCell.OnDrawGizmos();
        }
        
    }
}

