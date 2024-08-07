using UnityEngine;
using System.Collections;
public class TerrainWaveSimulation : MonoBehaviour
{
    // Not for actual use, just as an example of using waves on a heightmap
    public Terrain terrain;
    public float waveAmplitude = 0.1f;
    public float waveFrequency = 1f;
    public float waveSpeed = 1f;
    public float steepness = 0.3f;

    private TerrainData terrainData;
    private float[,] originalHeights;
    private readonly float timeOffset = 0f;
    private bool isUpdating = false;

    void Start()
    {
        if (terrain == null)
            return;

        terrainData = terrain.terrainData;
        originalHeights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

        StartCoroutine(UpdateTerrainCoroutine());
    }

    IEnumerator UpdateTerrainCoroutine()
    {
        while (true)
        {
            if (!isUpdating)
            {
                isUpdating = true;
                ApplyWavesToTerrain();
                isUpdating = false;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    void ApplyWavesToTerrain()
    {
        if (terrainData == null || originalHeights == null)
            return;

        float time = Time.time * waveSpeed + timeOffset;

        float[,] newHeights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                float waveX = x * waveFrequency + time;
                float waveZ = z * waveFrequency + time;

                float waveY = CalculateGerstnerWave(waveX, waveZ);

                newHeights[x, z] = originalHeights[x, z] + waveY * waveAmplitude;
            }
        }

        terrainData.SetHeights(0, 0, newHeights);
    }

    float CalculateGerstnerWave(float x, float z)
    {
        float result = 0f;
        Vector2 waveDirection = new Vector2(1f, 1f).normalized;
        result += Mathf.Sin(Vector2.Dot(waveDirection, new Vector2(x, z)) * waveFrequency) * waveAmplitude;

        return result;
    }
    void OnDisable()
    {
        if (terrain != null && terrainData != null && originalHeights != null)
            terrainData.SetHeights(0, 0, originalHeights);
    }

}
