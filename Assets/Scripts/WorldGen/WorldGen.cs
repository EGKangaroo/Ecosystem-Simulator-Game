using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    [SerializeField]
    MapData mapData = default;
    [SerializeField]
    MapDataRenderer mapRenderer = default;
    public int numberOfTiles;

    [ContextMenu("GenerateMap")]
    private void RegenerateMap()
    {
        Generate();
    }

    public void Generate()
    {
        mapData.tileData = new Tile[numberOfTiles * numberOfTiles];
        if(mapData != null)
        {
            int tileNo = 0;
            for (int i = 0; i < numberOfTiles; i++)
            {
                for (int j = 0; j < numberOfTiles; j++)
                {
                    Tile tile = new Tile();
                    tile.coordinateValue = new Coords(j, i);
                    mapData.tileData[tileNo] = tile;
                    tileNo++;
                }
            }
        }
        mapRenderer.CreateTiles();
    }
}
