using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct posRange
{
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
    int x;
    int y;
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
    public bool HasRight;
    public bool HasLeft;
    public bool HasAbove;
    public bool HasBelow;

    public KeyValuePair<Position, GameObject> Left;

    public KeyValuePair<Position, GameObject> Above;

    public KeyValuePair<Position, GameObject> Right;

    public KeyValuePair<Position, GameObject> Below;

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
