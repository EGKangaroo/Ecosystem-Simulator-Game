using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[ExecuteAlways]
public class WorldData : MonoBehaviour
{
    public UnityEvent mapCreatedEvent = new UnityEvent();

    private GameTile[,] mapData;

    public void CreateMap(GameTile[,] data)
    {
        this.mapData = data;

        //TO DO: Set neighbouring tile data
        Debug.Log("Created");
        mapCreatedEvent.Invoke();
    }

    public GameTile[,] ReadMap()
    {
        return mapData;
    }

    public List<LifeFormData> GetAllLifeForms()
    {
        List<LifeFormData> lifeforms = new List<LifeFormData>();

        foreach(var mapTile in mapData)
        {
            PlantData plant = mapTile.GetPlant();
            if(plant != null)
            {
                lifeforms.Add(plant);
            }
        }

        return lifeforms;
    }

    public void AddNewLifeForm(GameTile tile, Plant lifeform)
    {
        Debug.Log("hello we are here");
        PlantData plant = new PlantData(lifeform);
        tile.SetPlant(plant);
    }
}
