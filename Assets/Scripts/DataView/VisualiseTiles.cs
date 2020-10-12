using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualiseTiles : MonoBehaviour
{
    public PlayerController controller;
    public WorldData data;
    public VisualTile tilePrefab;

    public void GenerateVisualMap()
    {

        //delete all visual tile child objects first
        VisualTile[] tiles = GetComponentsInChildren<VisualTile>();
        foreach(var item in tiles)
        {
            DestroyImmediate(item.gameObject);
        }

        //regenerate
        GameTile[,] map = data.ReadMap();

        for(int x = 0; x < map.GetLength(0); x++)
        {
            for(int y = 0; y < map.GetLength(1); y++)
            {
                VisualTile tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity, this.transform);
                tile.Init(data, map[x, y], controller);
            }
        }

        MeshCombine();
    }

    private void MeshCombine()
    {
        transform.GetComponent<MeshFilter>().sharedMesh = null;
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        transform.GetComponent<MeshFilter>().sharedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
    }
}
