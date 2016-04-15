using UnityEngine;
using System.Collections;


[System.Serializable]
public class GridCell {

    public Vector3 Position
    {
        get;
        set;
    }
    public bool IsAsteroid = false;
    public bool IsSelected = false;

    public GameObject AsteroidCellObject;

    public GridCell(Vector2 pos)
    {
        Position = pos;

        if(AsteroidCellObject == null)
        {
            AsteroidCellObject = new GameObject(pos.ToString());
            AsteroidCellObject.AddComponent<BoxCollider2D>();
            AsteroidCellObject.transform.position = Position;
        }

    }

    public void OnDrawGizmos()
    {
        if (IsSelected)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(Position, new Vector3(0.9f, 0.9f, 0));
        }
        else {

            if (!IsAsteroid)
                return;

            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(Position, new Vector3(0.9f, 0.9f, 0));
        }
    }
}
