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
public struct KeyValuePairSerializable<K, V>
{
    public KeyValuePairSerializable(K k, V v)
    {
        Key = k;
        Value = v;
    }

    public K Key { get; set; }
    public V Value { get; set; }
}

[System.Serializable]
public struct CellNeighbours
{
    // TOP LAYER
    public KeyValuePairSerializable<Position, GameObject> LeftAbove;
    public bool HasLeftAbove;
    public KeyValuePairSerializable<Position, GameObject> Above;
    public bool HasAbove;
    public KeyValuePairSerializable<Position, GameObject> RightAbove;
    public bool HasRightAbove;
    
    // MIDDLE LAYER
    public KeyValuePairSerializable<Position, GameObject> Left;
    public bool HasLeft;
    public KeyValuePairSerializable<Position, GameObject> Right;
    public bool HasRight;

    // BOTTOM LAYER
    public KeyValuePairSerializable<Position, GameObject> LeftBelow;
    public bool HasLeftBelow;
    public KeyValuePairSerializable<Position, GameObject> Below;
    public bool HasBelow;
    public KeyValuePairSerializable<Position, GameObject> RightBelow;
    public bool HasRightBelow;
}
