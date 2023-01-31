using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public int mapWidth, mapLength, mapHeight, seed, faceOctaves;
    public float deviation;
    public float density;
    public /*Vector3[,,] displacements*/ Vector3[] octavesOffset;
    public GameObject prefab;
    public List<GameObject> tileList;
    public float[,,] TilesMap;
    public float scale, lacunarity, persistance, faceScale, faceLacunarity, facePersistance;
    private GameObject curObj;

    /*public Vector3[,,] GetDisplacements(float x, float y, float z, float tileScale)
    {
        float[,,] X = Noise.GenerateNoisePerl(mapWidth, mapLength, mapHeight, scale);
        float[,,] Y = Noise.GenerateNoisePerl(mapWidth, mapLength, mapHeight, scale);
        float[,,] Z = Noise.GenerateNoisePerl(mapWidth, mapLength, mapHeight, scale);

    }*/
    void Start()

    {
        System.Random rand = new System.Random();

        Random.InitState(seed);

        octavesOffset = new Vector3[faceOctaves];

        for (int i = 0; i < octavesOffset.Length; ++i)
        {
            octavesOffset[i].x = rand.Next(-5000, 5000);
            octavesOffset[i].y = rand.Next(-5000, 5000);
            octavesOffset[i].z = rand.Next(-5000, 5000);

            //Debug.Log(octavesOffset);
        }

        //deviation = Mathf.Clamp(deviation, 0, 100f);  //old way of storing deviation data 
        //deviation = detail * 0.001f*deviation;

        tileList = new List<GameObject>(mapWidth * mapLength * mapHeight);

        int X = mapWidth + 1;
        int Y = mapLength + 1;
        int Z = mapHeight + 1;
        /*
        float[,,] disX = new float[X, Y, Z];
        disX = Noise.GenerateNoiseRand(X, Y, Z);
        Debug.Log(disX[1, 1, 1]);

        float[,,] disY = new float[X, Y, Z];
        disY = Noise.GenerateNoiseRand(X, Y, Z);

        float[,,] disZ = new float[X, Y, Z];
        disZ = Noise.GenerateNoiseRand(X, Y, Z);
        */
        //displacements = new Vector3[X, Y, Z];

        float[,,] TilesMap = Noise.GenerateNoisePerl(mapWidth, mapLength, mapHeight, scale);

        /*for (int x =0; x<displacements.GetLength(0); ++x)
        {
            for (int y = 0; y < displacements.GetLength(1); ++y)
            {
                for (int z = 0; z < displacements.GetLength(2); ++z)
                {
                    displacements[x, y, z] = new Vector3(disX[x,y,z]* deviation, disY[x, y, z] * deviation, disZ[x, y, z] * deviation);
                }
            }
        }*/
        int j = 0;

        for (int x =0; x<mapWidth; ++x)
        {
            for (int y = 0; y < mapLength; ++y)
            {
                for (int z = 0; z < mapHeight; ++z)
                {
                    if (TilesMap[x,y,z] >density)
                    {
                        ++j;
                        curObj = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
                        tileList.Add(curObj);
                        curObj.name = "Tile "+j;
                        curObj.transform.parent = transform;
                        Tiles what = curObj.GetComponent(typeof(Tiles)) as Tiles;
                        what.TileSet();
                    }
                }
            }
        }

        

    }
}
