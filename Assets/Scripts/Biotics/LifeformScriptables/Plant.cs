using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "lifeforms/plant")]
public class Plant : LifeForm
{
    public override void InitiateLifeForm(GameTile tile)
    {
        new PlantData(this, tile);
    }
}
