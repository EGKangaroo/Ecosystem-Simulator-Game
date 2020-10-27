using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantData : LifeFormData
{
    public PlantData(Plant species, GameTile tile) : base(species, tile)
    {
        tile.SetPlant(this);
    }

    protected override void ImplHandleDeath()
    {
        occupyingTile.DeletePlant();
    }

    protected override void ImplReproduce(Coords pickedCoord)
    {
        GameTile tile = instance.GetTileByCoord(pickedCoord);

        if(tile.occupyingPlant == null)
        {
            new PlantData((Plant)this.Species, tile);
        }
    }
}
