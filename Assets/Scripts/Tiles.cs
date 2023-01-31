using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    //private _WorldTerrain referenceMap;
    //public Faces Face;
    // private Vector3[] 

    //GameObject GetTile(byte neighbours)
    private WorldGen source;

   /* private Vector3[] GetVertices ()
    {
        source = GetComponentInParent(typeof(WorldGen)) as WorldGen;

        float X = transform.localPosition.x;
        float Y = transform.localPosition.y;
        float Z = transform.localPosition.z;

        float lacunarity = source.faceLacunarity;
        float persistance = source.facePersistance;
        float scale = source.faceScale;
        int octaves = source.faceOctaves;
        int detail = source.detail;

        for (int x = 0; x< detail; ++x)
        {
            for (int y = 0; y < detail; ++y)
            {
                for (int z = 0; z < detail; ++z)
                {

                }
            }
        }




    }*/
    public void TileSet()
    {
        source = GetComponentInParent(typeof(WorldGen)) as WorldGen;

        //transform.localScale = new Vector3(100, 100, 100);

        float lacunarity = source.faceLacunarity;
        float persistance = source.facePersistance;
        int octaves = source.faceOctaves;
        float scale = source.faceScale;
        Vector3[] octavesOffset = source.octavesOffset;

        Mesh myMesh = GetComponent<MeshFilter>().mesh;

        //Debug.Log(source.ToString());

        int X = (int)transform.localPosition.x;
        int Y = (int)transform.localPosition.y;
        int Z = (int)transform.localPosition.z;

        Vector3[] vertices = myMesh.vertices;

        int x, y, z;
        float devX, devY, devZ;
        float deviation = Mathf.Clamp(source.deviation, 0, 100f);  //old way of storing deviation data 
        deviation = 0.0005f*deviation;


        for (int i = 0; i < vertices.Length; ++i)
        {
            
            x = (int)(vertices[i].x*4f + X*4);
            y = (int)(vertices[i].y*4f + Y*4);
            z = (int)(vertices[i].z*4f + Z*4);

            Debug.Log(string.Format("\n Global coords: {0}; {1}; {2}", X, Y, Z));
            Debug.Log(string.Format("\n Local coords: {0}; {1}; {2}", vertices[i].x, vertices[i].x, vertices[i].x));
            Debug.Log(string.Format("\n Sum: {0}; {1}; {2}", x, y, z));
            devX = deviation*Noise.GenerateNoisePerl_single(x, y, z, scale, lacunarity, persistance, octaves, octavesOffset, new Vector3(3.1f, 311f, 133f));
            devY = deviation*Noise.GenerateNoisePerl_single(x, y, z, scale, lacunarity, persistance, octaves, octavesOffset, new Vector3(3.1f, 311f, 133f));
            devZ = deviation*Noise.GenerateNoisePerl_single(x, y, z, scale, lacunarity, persistance, octaves, octavesOffset, new Vector3(3.1f, 311f, 133f));

            Vector3 dev = new Vector3(devX, devY, devZ);

            //Debug.Log(dev);
            vertices[i] += dev;
            //Debug.Log(string.Format("  {0},  {1},  {2},  {4},  {5},  {6},  {7}", x, y, z, scale, lacunarity, persistance, octaves, octavesOffset[0]));

        }

        /*
        for (int x = 0; x< 2; ++x)
        {
            for (int y = 0; y < 2; ++y)
            {
                for (int z = 0; z < 2; ++z)
                {
                    if (name == "Tile 1")
                    {
                        Debug.Log(string.Format("{0} {1} {2}", X, Y, Z));

                    }
                    vertices[4*x+2*y+z] = new Vector3(x-0.5f,y-0.5f,z-0.5f) + source.displacements[X+x,Y+y,Z+z];
                }
            }
        }*/
        //int[] tri = new int[36] { 1,3,2, 1, 2, 0, 0,6,4,0,2,6,5,3,1,5,7,3,3,6,2,3,7,6,5,0,4,5,1,0,7,4,6,7,5,4 };// Forgive me lord



        
        myMesh.vertices = vertices;
        //myMesh.triangles = tri;
        myMesh.RecalculateNormals();
        myMesh.RecalculateBounds();
        myMesh.RecalculateTangents();
    }
    public void OnMouseDown()
    {
        DestroyTile();
    }
    public void DestroyTile()
    {
        source = GetComponentInParent(typeof(WorldGen)) as WorldGen;
        foreach (GameObject i in source.tileList)
        {
            if (i.name == name)
            {
                Debug.Log(i.name);
                source.tileList.Remove(i);
                Destroy(i);
                break;
            }
        }
    }


}
