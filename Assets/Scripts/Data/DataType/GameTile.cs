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

    public PlantData occupyingPlant;

    public List<LifeFormData> GetAllLifeForms()
    {
        List<LifeFormData> lifeForms = new List<LifeFormData>();
        if(occupyingPlant != null)
        {
            lifeForms.Add(occupyingPlant);
        }
        return lifeForms;
    }

    public void SetPlant(PlantData data)
    {
        occupyingPlant = data;
        PlantChanged.Invoke();
    }

    public void DeletePlant()
    {
        occupyingPlant = null;
        PlantChanged.Invoke();
    }

    public PlantData GetPlant()
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
