using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshh : MonoBehaviour
{
    private Vector3[] Vertices;
    private int[] Triangles;
    // Start is called before the first frame update
    void Start()
    {
        Vertices = new Vector3[] 
        {
            Vector3.zero, Vector3.right, Vector3.up
        };
        Triangles = new int[]
        {
            0, 1, 2
        };
        var mesh = new Mesh
        {
            name = "Procedural Mesh"
        };
        mesh.SetVertices(Vertices);
        mesh.SetTriangles(Triangles,0);


        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
