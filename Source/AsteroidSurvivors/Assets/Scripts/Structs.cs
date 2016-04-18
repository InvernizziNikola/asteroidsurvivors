using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
public struct CellNeighbours
{
    // TOP LAYER
    public KeyValuePair<Position, GameObject> LeftAbove;
    public bool HasLeftAbove;
    public KeyValuePair<Position, GameObject> Above;
    public bool HasAbove;
    public KeyValuePair<Position, GameObject> RightAbove;
    public bool HasRightAbove;
    
    // MIDDLE LAYER
    public KeyValuePair<Position, GameObject> Left;
    public bool HasLeft;
    public KeyValuePair<Position, GameObject> Right;
    public bool HasRight;

    // BOTTOM LAYER
    public KeyValuePair<Position, GameObject> LeftBelow;
    public bool HasLeftBelow;
    public KeyValuePair<Position, GameObject> Below;
    public bool HasBelow;
    public KeyValuePair<Position, GameObject> RightBelow;
    public bool HasRightBelow;







    public override string ToString()
    {
        string output = "";
        if (HasAbove)
            output += " Above:" + Above.Key;
        if (HasRight)
            output += " Right:" + Right.Key;
        if (HasBelow)
            output += " Below:" + Below.Key;
        if (HasLeft)
            output += " Left:" + Left.Key;

        return output;
    }
}


[System.Serializable]
public struct SerializableAsteroid
{

}
