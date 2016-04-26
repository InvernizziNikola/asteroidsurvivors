using UnityEngine;
using System.Collections;

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
}
