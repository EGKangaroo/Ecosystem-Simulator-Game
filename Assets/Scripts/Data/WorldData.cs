using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Events;


public class WorldData : MonoBehaviour
{
    public UnityEvent mapCreatedEvent = new UnityEvent();

    private GameTile[,] mapData;

    private static WorldData instance;

    public static WorldData GetInstance()
    {
        return instance;
    }

    public void Awake()
    {
        if(instance == null && instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateMap(GameTile[,] data)
    {
        this.mapData = data;
        foreach(var item in mapData)
        {
            SetNeighbouringTiles(item);
        }

        Debug.Log("Created");
        mapCreatedEvent.Invoke();
    }

    private void SetNeighbouringTiles(GameTile tile)
    {
        List<GameTile> neighbouringTiles = new List<GameTile>();

        int[] xNeighbours = new int[]
        {
            tile.coords.x-1,
            tile.coords.x,
            tile.coords.x+1
        };
        int[] yNeighbours = new int[]
        {
            tile.coords.y-1,
            tile.coords.y,
            tile.coords.y+1
        };

        foreach(var itemX in xNeighbours)
        {
            foreach(var itemY in yNeighbours)
            {
                if(itemX >= 0 && itemX < mapData.GetLength(0))
                {
                    if(itemY >= 0 && itemY < mapData.GetLength(1))
                    {
                        if(mapData[itemX, itemY] != tile)
                        {
                            neighbouringTiles.Add(mapData[itemX, itemY]);
                        }
                    }
                }
            }
        }

        tile.SetNeighbouringTiles(neighbouringTiles);
    }

    public GameTile[,] ReadMap()
    {
        return mapData;
    }

    public GameTile GetTileByCoord(Coords coord)
    {
        foreach(var item in mapData)
        {
            if (item.coords.Equals(coord))
            {
                return item;
            }
        }
        return null;
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

    public void AddNewLifeForm(GameTile tile, LifeForm lifeform)
    {
        lifeform.InitiateLifeForm(tile);
    }
}
