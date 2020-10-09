using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TerrainGen : MonoBehaviour
{
    public WorldData data;
    [Min(1)]
    public int tilesPerRow;

    public void Generate()
    {
        GameTile[,] map = new GameTile[tilesPerRow, tilesPerRow];
        for (int i = 0; i < tilesPerRow; i++)
        {
            for (int j = 0; j < tilesPerRow; j++)
            {
                map[j, i] = new GameTile(new Coords(j, i));
            }
        }
        data.CreateMap(map);
    }
}
