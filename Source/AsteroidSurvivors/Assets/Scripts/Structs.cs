using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum AstSize
{
    Small = 5,
    Normal = 7,
    Big = 10,
    Huge = 14
}

[System.Serializable]
public struct PosRange
{
    public PosRange(float range, Vector2 position)
    {
        this.range = range;
        this.position = position;
    }
    public float range;
    public Vector2 position;
}

[System.Serializable]
public struct Position
{
    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public int x;
    public int y;
    public Vector2 GetPosition()
    {
        return new Vector2(x, y);
    }
    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }
    public static Position operator +(Position p1, Position p2)
    {
        return new Position(p1.x + p2.x, p1.y + p2.y);
    }
}

[System.Serializable]
public class CellNeighbours
{
    private List<GameObject> neighbours = new List<GameObject>();

    public List<GameObject> Neighbours { get { return neighbours; } }

    public void AddNeighbour(GameObject go)
    {
        neighbours.Add(go);
    }

    public GameObject GetNeighbour(Neighbour where)
    {
        return neighbours[(int)where]; 
    }
}

public enum Neighbour
{
    AboveLeft = 0,
    Above = 1,
    AboveRight = 2,
    Left = 3,
    Right = 4,
    BelowLeft = 5,
    Below = 6,
    BelowRight = 7
}