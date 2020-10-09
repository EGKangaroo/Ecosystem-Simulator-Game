using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTile
{
    public Coords coords;

    public GameTile(Coords coords)
    {
        this.coords = coords;
    }
}
