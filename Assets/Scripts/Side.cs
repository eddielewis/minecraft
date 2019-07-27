using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{
    public Vector3[] normals;
    public int[] triangles;
    public Vector2[] uvs;
    public Vector3[] vertices;

    public Side(Vector3 normal, Vector2 uvLow, Vector2 uvHigh, Vector3[] vertices)
    {
        this.normals = SetNormals(normal);
        this.triangles = new int[] { 3, 1, 0, 3, 2, 1 };
        this.uvs = SetUvs(uvLow, uvHigh);
        this.vertices = vertices;
    }

    public void OffsetVertices(Vector3Int offset)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += offset;
        }
    }

    public Side CopySide()
    {
        return new Side(normals, triangles, uvs, vertices);
    }

    public void SetNormals(Vector3 normal)
    {
        normals = new Vector3[] { normal, normal, normal, normal };
    }

    public void SetUvs(Vector2 low, Vector2 high)
    {
        uvs = new Vector2[] {
            new Vector2(high, high),
            new Vector2(low, high),
            new Vector2(low, low),
            new Vector2(high, low)
        };
    }
}
