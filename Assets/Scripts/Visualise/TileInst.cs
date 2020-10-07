using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInst : MonoBehaviour
{
    [SerializeField]
    Tile tile = default;
    [SerializeField]
    Renderer rend = default;

    public void Initialise(Tile tile)
    {
        this.tile = tile;
        UpdateColor();
    }

    void UpdateColor()
    {
        if(tile.GetPlant() != null)
        {
            rend.sharedMaterial.color = new Color(0.5f, 0.8f, 0.6f);
        }
        else
        {
            rend.sharedMaterial.color = new Color(0.8f, 0.6f, 0.5f);
        }
    }
}
