using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Tile
{
    public Coords coordinateValue;
    [SerializeField]
    private Plant currentPlant;

    public UnityEvent TileInfoChanged = new UnityEvent();

    public Plant GetPlant()
    {
        return currentPlant;
    }

    public void SetPlant(Plant changePlant)
    {
        currentPlant = changePlant;
        TileInfoChanged.Invoke();
    }
}
