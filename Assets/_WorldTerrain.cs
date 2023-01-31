using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Drawing;

public class _WorldTerrain : MonoBehaviour
{
    //private int[][] bitMap;
    private int X, Z;
    private UnityEngine.Color32[] InterMap, InterMap_t;
    public float MapOriginPosX, MapOriginPosZ;
    public Texture2D TerrainMap;
    public GameObject TerrainTile;
    public int width, height;
    public Tiles Tiles;

    void Start()
    {
        width = TerrainMap.width;
        height = TerrainMap.height;
        InterMap = TerrainMap.GetPixels32(); // 1D array of piexels of loaded map
        for (int i = 0; i< width; ++i)
        {
            if (InterMap[i].r == 0)
            {
                X = i;
                Z = 0;
                //Debug.Log("Tile present at {" + X+";"+Z+"};");
                Instantiate(TerrainTile, new Vector3((X + MapOriginPosX) * 2, 1, ((Z+ MapOriginPosZ) * 2)), new Quaternion(0, 0, 0, 0));
            }
        }
        for (int i = TerrainMap.width; i < InterMap.Length ; ++i)
        {
            if (InterMap[i].r == 0)
            {
                Z = i / width;
                X = i % width;
                //Debug.Log("Tile present at {" + X + ";" + Z + "}");
                Instantiate(TerrainTile, new Vector3((X + MapOriginPosX) * 2, 1, ((Z + MapOriginPosZ) * 2)), new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
