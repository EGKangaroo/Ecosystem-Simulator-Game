using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MapData : MonoBehaviour
{
    public Tile[] tileData;

    public List<LifeFormInstance> lifeForms = new List<LifeFormInstance>();

    public void AddPlant(Tile tile, Plant plantType)
    {
        Tile found = tileData.FirstOrDefault(x => x.Equals(tile));
        if(found != null)
        {
            found.SetPlant(plantType);
        }
        lifeForms.Add(found.GetPlant());
    }
}
