using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameTile
{
    public UnityEvent PlantChanged = new UnityEvent();

    public Coords coords;

    public PlantData occupyingPlant;

    public void SetPlant(PlantData data)
    {
        occupyingPlant = data;
        Debug.Log("hello we have set plant");
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
}
