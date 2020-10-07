using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataRenderer : MonoBehaviour
{
    public MapData data;
    public TileInst tilePrefab;

    public List<TileInst> tiles = new List<TileInst>();

    public void CreateTiles()
    {
        foreach(var item in tiles)
        {
            if(item != null)
            {
                DestroyImmediate(item.gameObject);
            }
        }
        tiles.Clear();
        foreach(var item in data.tileData)
        {
            TileInst inst = Instantiate(tilePrefab, new Vector3(item.coordinateValue.x, 0, item.coordinateValue.y), Quaternion.identity, this.transform);
            inst.Initialise(item);
            tiles.Add(inst);
        }
        
    }
}
