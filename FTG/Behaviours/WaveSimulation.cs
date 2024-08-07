using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class WaveSimulation : MonoBehaviour
{
    public float waveAmplitude = 0.1f;
    public float waveFrequency = 1f;
    public float waveSpeed = 1f;

    private MeshCollider meshCollider;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private Vector3[] baseVertices;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        if (mesh != null)
        {
            baseVertices = mesh.vertices;
        }
        else
        {
            Debug.LogError("Mesh not found!");
        }
    }

    private void Update()
    {
        ApplyWavesToMesh();
        UpdateCollision();
    }
    void UpdateCollision() => meshCollider.sharedMesh = mesh;
    void ApplyWavesToMesh()
    {
        Vector3[] vertices = mesh.vertices;
        float time = Time.time * waveSpeed;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            float waveX = vertex.x * waveFrequency + time;
            float waveZ = vertex.z * waveFrequency + time;

            float waveY = CalculateGerstnerWave(waveX, waveZ);

            vertex.y = waveY * waveAmplitude;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    float CalculateGerstnerWave(float x, float z)
    {
        float result = 0f;
        Vector2 waveDirection = new Vector2(1f, 1f).normalized;
        result += Mathf.Sin(Vector2.Dot(waveDirection, new Vector2(x, z)) * waveFrequency) * waveAmplitude;
        return result;
    }
}
