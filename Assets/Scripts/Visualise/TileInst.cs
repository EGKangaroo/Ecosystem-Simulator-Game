using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInst : MonoBehaviour
{
    [SerializeField]
    Tile tile = default;

    public void Initialise(Tile tile)
    {
        this.tile = tile;
    }
}
