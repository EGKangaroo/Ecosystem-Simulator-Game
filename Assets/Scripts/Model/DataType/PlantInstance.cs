using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantInstance : LifeFormInstance
{
    public PlantInstance(Plant species, GameTile tile) : base(species, tile)
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
            new PlantInstance((Plant)this.Species, tile);
        }
    }
}
