using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Coords
{
    public int x;
    public int y;

    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(object obj)
    {
        if(obj is Coords)
        {
            Coords coord = (Coords)obj;
            return x == coord.x && y == coord.y;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }
}
