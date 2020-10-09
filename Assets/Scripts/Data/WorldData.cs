using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WorldData : MonoBehaviour
{
    public UnityEvent mapCreatedEvent = new UnityEvent();

    private GameTile[,] mapData;

    public void CreateMap(GameTile[,] data)
    {
        this.mapData = data;
        Debug.Log("Created");
        mapCreatedEvent.Invoke();
    }

    public GameTile[,] ReadMap()
    {
        return mapData;
    }
}
