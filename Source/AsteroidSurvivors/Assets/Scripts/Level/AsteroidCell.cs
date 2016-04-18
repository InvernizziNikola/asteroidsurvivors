using UnityEngine;
using System.Collections;

public class AsteroidCell : MonoBehaviour{


    public Sprite AsteroidSprite;

    public CellNeighbours cellN;
    public int ID = -1;


    public void Start()
    {
    }
    public void SetSpriteRenderer()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = AsteroidSprite;
            sr.transform.localScale = Vector2.one;
        }
    }
}
