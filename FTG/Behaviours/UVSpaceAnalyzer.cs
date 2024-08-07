using UnityEngine;

public static class UVSpaceAnalyzer
{
    public static float CalculateUVSpaceRatio(Mesh currentsharedMesh)
    {
        if (currentsharedMesh.uv != null && currentsharedMesh.uv.Length > 0)
        {
            Vector2[] uvs = currentsharedMesh.uv;
            int[] triangles = currentsharedMesh.triangles;
            float totalUVArea = 0.0f;
            for (int i = 0; i < triangles.Length; i += 3)
            {
                Vector2 uv0 = uvs[triangles[i]];
                Vector2 uv1 = uvs[triangles[i + 1]];
                Vector2 uv2 = uvs[triangles[i + 2]];
                float area = 0.5f * Mathf.Abs(Vector2.Dot(uv1 - uv0, new Vector2(uv2.y - uv0.y, uv0.x - uv2.x)));
                totalUVArea += area;
            }
            float uvCoveragePercentage = totalUVArea * 100.0f;

            return uvCoveragePercentage;
        }
        else return 0;
    }
}
