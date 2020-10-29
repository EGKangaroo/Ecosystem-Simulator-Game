using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameTile
{
    public List<Coords> neighbouringCoords;

    public UnityEvent PlantChanged = new UnityEvent();

    public Coords coords;

    public PlantInstance occupyingPlant;

    public List<LifeFormInstance> GetAllLifeForms()
    {
        List<LifeFormInstance> lifeForms = new List<LifeFormInstance>();
        if(occupyingPlant != null)
        {
            lifeForms.Add(occupyingPlant);
        }
        return lifeForms;
    }

    public void SetPlant(PlantInstance data)
    {
        occupyingPlant = data;
        PlantChanged.Invoke();
    }

    public void DeletePlant()
    {
        occupyingPlant = null;
        PlantChanged.Invoke();
    }

    public PlantInstance GetPlant()
    {
        return occupyingPlant;
    }

    public GameTile(Coords coords)
    {
        this.coords = coords;
    }

    public void SetNeighbouringTiles(List<GameTile> neighbouringTiles)
    {
        List<Coords> coordsList = new List<Coords>();
        foreach(var item in neighbouringTiles)
        {
            coordsList.Add(item.coords);
        }
        neighbouringCoords = coordsList;
    }
}
