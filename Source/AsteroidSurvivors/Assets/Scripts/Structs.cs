using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum AstSize
{
    Small = 10,
    Normal = 14,
    Big = 20,
    Huge = 28
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