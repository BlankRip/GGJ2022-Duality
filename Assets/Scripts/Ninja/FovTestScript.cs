using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovTestScript : MonoBehaviour
{
    [SerializeField] float distance = 10f;
    [SerializeField] float angle = 30;
    [SerializeField] float height = 1.0f;

    private Color meshColor = Color.red;

    Mesh mesh;
    Mesh CreateWedgeMesh()
    {
        mesh = new Mesh();
        int numTriangles = 8;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCentre = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCentre = bottomCentre + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height; 

        int vert = 0;

        //Left Side
        vertices[vert++] = bottomCentre;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCentre;
        vertices[vert++] = bottomCentre;

        //Right Side

        vertices[vert++] = bottomCentre;
        vertices[vert++] = topCentre;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCentre;

        //Far Side

        vertices[vert++] = bottomLeft;
        vertices[vert++] = bottomRight;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = topLeft;
        vertices[vert++] = bottomLeft;

        //top

        vertices[vert++] = topCentre;
        vertices[vert++] = topLeft;
        vertices[vert++] = topRight;

        //bottom

        vertices[vert++] = bottomCentre;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomLeft;

        for(int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;                    
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        return mesh;
    }

    private void OnValidate() 
    {
        mesh = CreateWedgeMesh();
    }

    private void OnDrawGizmos() 
    {
        if(mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
    }

}
