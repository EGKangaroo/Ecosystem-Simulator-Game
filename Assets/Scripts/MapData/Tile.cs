using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Tile
{
    public Coords coordinateValue;
    [SerializeField]
    private PlantInstance currentPlant;

    public UnityEvent TileInfoChanged = new UnityEvent();

    public PlantInstance GetPlant()
    {
        return currentPlant;
    }

    public void SetPlant(Plant changePlant)
    {
        currentPlant = new PlantInstance(changePlant);
        TileInfoChanged.Invoke();
    }
}
