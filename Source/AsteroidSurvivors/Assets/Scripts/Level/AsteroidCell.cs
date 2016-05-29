using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class AsteroidCell : MonoBehaviour{

    public CellNeighbours cellN;
    public int ID = -1;


    public void Start()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.transform.localScale = Vector2.one;
        }
    }

    public CellData Save()
    {
        CellData cellData = new CellData();

        cellData.CellNeighbours = new CellNeighboursData(cellN.HasLeftAbove, cellN.HasAbove, cellN.HasRightAbove, 
                                                        cellN.HasLeft, cellN.HasRight, 
                                                        cellN.HasLeftBelow, cellN.HasBelow, cellN.HasRightBelow);

        return cellData;
    }
}
