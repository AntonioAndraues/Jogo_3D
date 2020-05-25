using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//reference https://github.com/SebLague/Procedural-Planets
public class TerrenoFace : MonoBehaviour
{

    Mesh mesh;
    int resolucao;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public TerrenoFace(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolucao = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolucao * resolucao];
        int[] triangles = new int[(resolucao - 1) * (resolucao - 1) * 6];
        int triIndex = 0;

        for (int y = 0; y < resolucao; y++)
        {
            for (int x = 0; x < resolucao; x++)
            {
                int i = x + y * resolucao;
                Vector2 percent = new Vector2(x, y) / (resolucao - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = pointOnUnitSphere;

                if (x != resolucao - 1 && y != resolucao - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolucao + 1;
                    triangles[triIndex + 2] = i + resolucao;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolucao + 1;
                    triIndex += 6;
                }
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
