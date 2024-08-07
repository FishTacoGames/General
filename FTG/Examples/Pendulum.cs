using System.Collections;
using UnityEngine;
using FishTacoGames;
public class Pendulum : MonoBehaviour
{
    public float length = 1.0f;
    public float gravity = 9.81f;
    public Vector2 initialState = new(Mathf.PI / 4, 0.0f); // Initial angle (45 degrees) and angular velocity
    public float timeStep = 0.01f;

    public GameObject end;
    public LineRenderer line; 

    private Vector2 currentState;
    void Start()
    {
        currentState = initialState;
        if (line == null)
            line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        StartCoroutine(SimulatePendulum());
    }
    // Example function for the pendulum
    Vector2 F(float _, Vector2 y)
    {
        return new Vector2(y.y, -gravity / length * Mathf.Sin(y.x));
    }
    IEnumerator SimulatePendulum()
    {
        float t = 0.0f;
        while (true)
        {
            currentState = FishTacoGames_Vec2.RungeKutta(t, currentState, timeStep, F);
            t += timeStep;
            float angle = currentState.x;

            float x = length * Mathf.Sin(angle);
            float y = -length * Mathf.Cos(angle); 

            Vector3 pendulumPosition = new(x, y, 0);
            end.transform.position = pendulumPosition;

            line.SetPosition(0, transform.position);
            line.SetPosition(1, pendulumPosition);
            yield return new WaitForSeconds(timeStep);
        }
    }
}
