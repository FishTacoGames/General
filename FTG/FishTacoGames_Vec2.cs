using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores Vector2 methods!
/// </summary>
namespace FishTacoGames
{
    public static class FishTacoGames_Vec2
    {
        /// <summary>
        /// Converts a hit point to a Vector2, ignoring the height component.
        /// Usefull if you do not need to store a vec3 and only need 2D coords
        /// </summary>
        /// <param name="hit">The RaycastHit object containing the hit information.</param>
        /// <returns>The Vector2 position without height.</returns>
        public static Vector2 HitPointToVector2(RaycastHit hit)
        {
            return new(hit.point.x, hit.point.z);
        }
        private static float CalculateUVPolygonArea(Vector2 uv0, Vector2 uv1, Vector2 uv2)
        {
            return 0.5f * Mathf.Abs(Vector2.Dot(uv1 - uv0, new Vector2(uv2.y - uv0.y, uv0.x - uv2.x)));
        }
        /// <summary>
        /// Rotate a 2D vector value by a given angle (in degrees)
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 Rotate(Vector2 vector, float angle)
        {
            float angleRad = angle * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angleRad);
            float sin = Mathf.Sin(angleRad);
            return new (
                vector.x * cos - vector.y * sin,
                vector.x * sin + vector.y * cos
            );
        }

        public static float AngleBetween(Vector2 from, Vector2 to)
        {
            return Mathf.Atan2(to.y, to.x) - Mathf.Atan2(from.y, from.x);
        }

        // Reflects a vector off the surface defined by a normal vector.
        public static Vector2 ReflectVec2(Vector2 vector, Vector2 normal)
        {
            // Use the formula: reflection = incident - 2 * (incident dot normal) * normal
            return vector - 2 * Vector2.Dot(vector, normal) * normal;
        }

        /// <summary>
        /// Finds the closest point among a list of points to a reference point.
        /// </summary>
        /// <param name="referencePoint">The reference point to which distances are compared (A player).</param>
        /// <param name="points">The list of points to search from.</param>
        /// <returns>The closest point from the list to the reference point.</returns>
        public static Vector2 FindClosestPoint(Vector2 referencePoint, List<Vector2> points)
        {
            if (points == null || points.Count == 0)
                return Vector2.zero;

            Vector2 closestPoint = points[0];
            float closestDistance = Vector2.Distance(referencePoint, closestPoint);
            for (int i = 1; i < points.Count; i++)
            {
                float distance = Vector2.Distance(referencePoint, points[i]);
                if (distance < closestDistance)
                {
                    closestPoint = points[i];
                    closestDistance = distance;
                }
            }
            return closestPoint;
        }

        /// <summary>
        /// Calculates the center of a set of points.
        /// NOT for mesh/ 100+ points
        /// </summary>
        /// <param name="points">The list of points.</param>
        /// <returns>The centroid as a Vector2.</returns>
        public static Vector2 CalculateCenter(List<Vector2> points)
        {
            if (points == null || points.Count == 0)
                return Vector2.zero;
            Vector2 center = Vector2.zero;
            foreach (Vector2 point in points)
                center += point;
            center /= points.Count;
            return center;
        }

        /// <summary>
        /// Solver for numerical integration of a system of ordinary differential equations (ODEs).
        /// see Pendulum for example
        /// </summary>
        /// <param name="t"></param>
        /// <param name="y"></param>
        /// <param name="dt"></param>
        /// <param name="F"></param>
        /// <returns></returns>       
        public static Vector2 RungeKutta(float t, Vector2 y, float dt, System.Func<float, Vector2, Vector2> F)
        {
            Vector2 k1, k2, k3, k4;
            k1 = dt * F(t, y);
            k2 = dt * F(t + dt / 2, y + k1 / 2);
            k3 = dt * F(t + dt / 2, y + k2 / 2);
            k4 = dt * F(t + dt, y + k3);

            return y + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
        }
    }
}
