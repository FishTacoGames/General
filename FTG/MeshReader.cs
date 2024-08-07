using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshReader
{
    private static (float, float) CalculateUVAndTriangleDensity(int[] triangles, Vector2[] uvs, Vector3[] vertices)
    {
        float totalUVArea = 0.0f;
        float totalTriangleArea = 0f;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector2 uv0 = uvs[triangles[i]];
            Vector2 uv1 = uvs[triangles[i + 1]];
            Vector2 uv2 = uvs[triangles[i + 2]];
            totalUVArea += FishTacoGames_Vec2.CalculateUVPolygonArea(uv0, uv1, uv2);

            int vertexIndex1 = triangles[i];
            int vertexIndex2 = triangles[i + 1];
            int vertexIndex3 = triangles[i + 2];
            Vector3 v1 = vertices[vertexIndex1];
            Vector3 v2 = vertices[vertexIndex2];
            Vector3 v3 = vertices[vertexIndex3];
            totalTriangleArea += FishTacoGames_Vec3.CalculateTriangleArea(v1, v2, v3);
        }
        if (totalTriangleArea > 0f && totalUVArea > 0)
        {
            float uvCoveragePercentage = (totalUVArea / 1.0f) * 100.0f;
            float triangleDensity = triangles.Length / totalTriangleArea;
            return (uvCoveragePercentage, triangleDensity);
        }
        return (0f, 0f);
    }
    public static (float,float) GetMeshCoverage(MeshFilter MF)
    {
        Vector3[] vertices = MF.sharedMesh.vertices;
        int[] currenttriangles = MF.sharedMesh.triangles;
        Vector2[] uvs = MF.sharedMesh.uv;
        return CalculateUVAndTriangleDensity(currenttriangles, uvs, vertices);
    }
    public static (float, float)[] GetSubMeshCoverages(Material[] matlist, MeshFilter mesh)
    {
        var info = new List<(float, float)>();
        foreach (Material mat in matlist)
        {
            int materialSubmeshIndex = 0;
            for (int i = 0; i < matlist.Length; i++)
            {
                if (matlist[i] == mat)
                {
                    materialSubmeshIndex = i;
                    break;
                }
            }
            int[] triangles = mesh.sharedMesh.GetTriangles(materialSubmeshIndex);
            info.Add(CalculateUVAndTriangleDensity(triangles, mesh.sharedMesh.uv, mesh.sharedMesh.vertices));
        }
        return info.ToArray();
    }
}
