using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WallMesh : MonoBehaviour
{
    public int MeshDetail;
    private int n;
    public float Vertices_deviation;
    private float Deviation_clamped;
    private Vector3[,] Vertices;
    private Vector3[] Vertices1D;
    private int[] Triangles;
    private string output;

    int TriFromDetail(int Detail)
    {
        int r = 0;
        if (Detail > 2)
        {
            r = (Detail - 2) * 4 + TriFromDetail(Detail - 1);
        }
        return r + 2;

    }
    //Mesh GenerateFace ()
    void Start()
    {
        output = "";
        var mesh = new Mesh
        {
            name = "Procedural Mesh"
        };
        Vertices = new Vector3[MeshDetail , MeshDetail];
        Vertices1D = new Vector3[MeshDetail * MeshDetail];
        Deviation_clamped = Mathf.Clamp(Vertices_deviation, 0, 0.2f);
        for (int i = 0; i < MeshDetail; ++i)
        {
            for (int j =0; j< MeshDetail; ++j)
            {
                Vertices[j, i] = new Vector3(1f * j / MeshDetail, 1f * i / MeshDetail, Random.Range(-Deviation_clamped / 2, Deviation_clamped / 2));
                Vertices1D[i* MeshDetail + j] = Vertices[j, i];
                Debug.Log(string.Format("Vertex {0},{1}: {2}_{3}_{4}",i,j, Vertices1D[i * MeshDetail + j].x, Vertices1D[i * MeshDetail + j].y, Vertices1D[i * MeshDetail + j].z));
            }
        }
        for (int i = 0; i < Vertices1D.Length; ++i) output = output + Vertices1D[i].ToString() + " ";
        Debug.Log(output);
        mesh.SetVertices(Vertices1D);
       Triangles = new int[TriFromDetail(MeshDetail)*3];
        for (int i =0; i< (MeshDetail-1)*(MeshDetail-1); ++i)
        {
            n = i + i / (MeshDetail - 1);
            Triangles[i*6] = n;
            Triangles[i * 6 + 1] = n+ MeshDetail;
            Triangles[i * 6 + 2] = n+ MeshDetail+1;
            Triangles[i * 6 + 3] = n;
            Triangles[i * 6 + 4] = n+ MeshDetail+1;
            Triangles[i * 6 + 5] = n+1;
        }
        
        mesh.SetTriangles(Triangles,0,true,0);
        


        
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.RecalculateNormals();
        Debug.Log("Calculated amount of triangles: " + TriFromDetail(MeshDetail));
        
    }
    void Update()
    {
        
    }

}