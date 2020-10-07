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
        foreach (var item in tiles)
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
        MeshCombine();
    }

    private void MeshCombine()
    {
        transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
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
