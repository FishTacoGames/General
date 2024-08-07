using UnityEngine;
/// <summary>
/// Stores Vector3 methods!
/// </summary>
namespace FishTacoGames
{
    public static class FishTacoGames_Vec3
    {
        public static Vector3 ReflectVec3(Vector3 direction, Vector3 normal)
        {
            return direction - 2f * Vector3.Dot(direction, normal) * normal;
        }

        /// <summary>
        /// Calculates the projection of a vector onto another vector.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="onto">The vector to project onto.</param>
        /// <returns>The projected vector.</returns>
        public static Vector3 VectorProjection(Vector3 vector, Vector3 onto)
        {
            float magnitudeSquared = onto.sqrMagnitude;
            if (magnitudeSquared < Mathf.Epsilon)
            {
                // Avoid division by zero
                return Vector3.zero;
            }
            return Vector3.Dot(vector, onto) / magnitudeSquared * onto;
        }


        /// <summary>
        /// Projects a vector onto a plane defined by a normal vector.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="planeNormal">The normal vector of the plane.</param>
        /// <returns>The projected vector.</returns>
        public static Vector3 ProjectOntoPlane(Vector3 vector, Vector3 planeNormal)
        {
            return vector - Vector3.Dot(vector, planeNormal) * planeNormal;
        }

        /// <summary>
        /// Calculates the intersection point of a line segment and a plane.
        /// </summary>
        /// <param name="point1">The first point of the line segment.</param>
        /// <param name="point2">The second point of the line segment.</param>
        /// <param name="planePoint">A point on the plane.</param>
        /// <param name="planeNormal">The normal vector of the plane.</param>
        /// <returns>The intersection point of the line segment and the plane, or null if no intersection.</returns>
        public static Vector3? LinePlaneIntersection(Vector3 point1, Vector3 point2, Vector3 planePoint, Vector3 planeNormal)
        {
            Vector3 lineDirection = point2 - point1;
            float dot = Vector3.Dot(planeNormal, lineDirection);

            // Check if the line segment and plane are not parallel
            if (Mathf.Abs(dot) > Mathf.Epsilon)
            {
                float t = Vector3.Dot(planeNormal, (planePoint - point1)) / dot;
                // Check if the intersection point is within the line segment
                if (t >= 0f && t <= 1f)
                {
                    return point1 + t * lineDirection;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if a point is inside a sphere defined by a center and a radius.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <param name="center">The center of the sphere.</param>
        /// <param name="radius">The radius of the sphere.</param>
        /// <returns>True if the point is inside the sphere, otherwise false.</returns>
        public static bool IsPointInSphere(Vector3 point, Vector3 center, float radius)
        {
            return Vector3.SqrMagnitude(point - center) < radius * radius;
        }

        /// <summary>
        /// Calculates the angle in degrees between two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The angle in degrees between the two vectors.</returns>
        public static float AngleBetweenVectors(Vector3 vector1, Vector3 vector2)
        {
            float dotProduct = Vector3.Dot(vector1.normalized, vector2.normalized);
            return Mathf.Acos(Mathf.Clamp(dotProduct, -1f, 1f)) * Mathf.Rad2Deg;
        }
        /// <summary>
        /// Expands the distance between two Vector3 points by a specified value.
        /// Access return as [0,1] where point 1 = 0
        /// </summary>
        /// <param name="point1">The first Vector3 point.</param>
        /// <param name="point2">The second Vector3 point.</param>
        /// <param name="distance">The distance to expand.</param>
        /// <returns>An array containing the expanded positions of both points.</returns>
        public static Vector3[] ExpandDistance(Vector3 point1, Vector3 point2, float distance)
        {
            Vector3 direction = (point2 - point1).normalized;
            Vector3 offset = direction * (distance / 2f);

            Vector3[] expandedPoints = new Vector3[2];
            expandedPoints[0] = point1 - offset;
            expandedPoints[1] = point2 + offset;

            return expandedPoints;
        }
        /// <summary>
        /// Calculates the area of a triangle defined by three vertices.
        /// </summary>
        /// <param name="vertex1">The first vertex of the triangle.</param>
        /// <param name="vertex2">The second vertex of the triangle.</param>
        /// <param name="vertex3">The third vertex of the triangle.</param>
        /// <returns>The area of the triangle.</returns>
        public static float TriangleArea(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
        {
            Vector3 edge1 = vertex2 - vertex1;
            Vector3 edge2 = vertex3 - vertex1;
            return 0.5f * Vector3.Cross(edge1, edge2).magnitude;
        }
        /// <summary>
        /// Reflects a vector across a specified axis.
        /// </summary>
        /// <param name="vector">The vector to reflect.</param>
        /// <param name="axis">The axis to reflect across (should be normalized).</param>
        /// <returns>The reflected vector.</returns>
        public static Vector3 ReflectAcrossAxis(Vector3 vector, Vector3 axis)
        {
            return 2 * Vector3.Dot(vector, axis) * axis - vector;
        }
        // same 2
        /// <summary>
        /// Finds a vector that is perpendicular to the given vector.
        /// </summary>
        /// <param name="vector">The vector to find a perpendicular for.</param>
        /// <returns>A perpendicular vector.</returns>
        public static Vector3 GetPerpendicular(Vector3 vector)
        {
            // Choose an arbitrary vector to cross with (vector cannot be zero vector)
            Vector3 arbitraryVector = Mathf.Abs(vector.x) > Mathf.Abs(vector.z) ? Vector3.up : Vector3.right;
            return Vector3.Cross(vector, arbitraryVector).normalized;
        }
        /// <summary>
        /// Finds a vector that is orthogonal (perpendicular) to the given vector.
        /// </summary>
        /// <param name="vector">The vector to find an orthogonal vector for.</param>
        /// <returns>An orthogonal vector.</returns>
        public static Vector3 GetOrthogonal(Vector3 vector)
        {
            return vector == Vector3.zero ? Vector3.zero : Vector3.Cross(vector, Vector3.up).normalized;
        }
        /// <summary>
        /// Calculates the bisector of the angle formed by two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The bisecting vector.</returns>
        public static Vector3 BisectingVector(Vector3 vector1, Vector3 vector2)
        {
            return (vector1.normalized + vector2.normalized).normalized;
        }
        /// <summary>
        /// Decomposes a rotation into its pitch, yaw, and roll components.
        /// </summary>
        /// <param name="rotation">The rotation quaternion.</param>
        /// <returns>A Vector3 containing pitch, yaw, and roll in degrees.</returns>
        public static Vector3 DecomposeRotation(Quaternion rotation)
        {
            float pitch = Mathf.Atan2(2 * (rotation.w * rotation.x + rotation.y * rotation.z), 1 - 2 * (rotation.x * rotation.x + rotation.y * rotation.y)) * Mathf.Rad2Deg;
            float yaw = Mathf.Asin(2 * (rotation.w * rotation.y - rotation.z * rotation.x)) * Mathf.Rad2Deg;
            float roll = Mathf.Atan2(2 * (rotation.w * rotation.z + rotation.x * rotation.y), 1 - 2 * (rotation.y * rotation.y + rotation.z * rotation.z)) * Mathf.Rad2Deg;

            return new Vector3(pitch, yaw, roll);
        }
        /// <summary>
        /// Converts a quaternion to an axis-angle representation.
        /// </summary>
        /// <param name="quaternion">The quaternion to convert.</param>
        /// <returns>A tuple containing the axis (Vector3) and the angle (float) in degrees.</returns>
        public static (Vector3 axis, float angle) QuaternionToAxisAngle(Quaternion quaternion)
        {
            if (quaternion.w > 1)
                quaternion.Normalize(); // Normalize if necessary

            float angle = 2 * Mathf.Acos(quaternion.w) * Mathf.Rad2Deg;
            float s = Mathf.Sqrt(1 - quaternion.w * quaternion.w);
            Vector3 axis = s < 0.001 ? Vector3.right : new Vector3(quaternion.x / s, quaternion.y / s, quaternion.z / s);

            return (axis, angle);
        }
        /// <summary>
        /// Calculates the tangent space (tangent and bitangent) given a surface normal.
        /// </summary>
        /// <param name="normal">The normal vector of the surface.</param>
        /// <returns>A tuple containing the tangent and bitangent vectors.</returns>
        public static (Vector3 tangent, Vector3 bitangent) CalculateTangentSpace(Vector3 normal)
        {
            Vector3 tangent = Vector3.Cross(normal, Vector3.up);
            if (tangent.sqrMagnitude < 0.001f)
                tangent = Vector3.Cross(normal, Vector3.right);

            tangent.Normalize();
            Vector3 bitangent = Vector3.Cross(normal, tangent);
            return (tangent, bitangent);
        }
        /// <summary>
        /// Creates a jitter effect for example a light source, simulating flickering flames and dynamic shadows.
        /// </summary>
        /// <param name="position">The position of the object casting shadows.</param>
        /// <param name="lightPosition">The position of the light source.</param>
        /// <param name="jitterMagnitude">The magnitude of jittering effect.</param>
        /// <param name="frequency">The frequency of jittering.</param>
        /// <param name="seed">The random seed for consistency.</param>
        /// <returns>The jittered position.</returns>
        public static Vector3 JitterVec3(Vector3 position, Vector3 lightPosition, float jitterMagnitude, float frequency, int seed)
        {
            // Use seed for consistent randomness
            Random.InitState(seed);

            Vector3 directionToLight = (lightPosition - position).normalized;

            // Calculate jitter
            float noise = Mathf.PerlinNoise(Time.time * frequency, seed) * jitterMagnitude;
            Vector3 jitterOffset = directionToLight * noise;
            return position + jitterOffset;
        }


        // Point generators
        /// <summary>
        /// Generates a random vector inside a cube defined by a center point and half extents along each axis.
        /// </summary>
        /// <param name="center">The center of the cube.</param>
        /// <param name="halfExtents">The half extents along each axis.</param>
        /// <returns>A random vector inside the cube.</returns>
        public static Vector3 GetRandomVectorInsideCollider(Vector3 center, Collider collider, bool Extent = false)
        {
            float x = Extent ? Random.Range(center.x - collider.bounds.extents.x, center.x + collider.bounds.extents.x) : Random.Range(center.x - collider.bounds.size.x, center.x + collider.bounds.size.x);
            float y = Extent ? Random.Range(center.y - collider.bounds.extents.y, center.y + collider.bounds.extents.y) : Random.Range(center.y - collider.bounds.size.y, center.y + collider.bounds.size.y);
            float z = Extent ? Random.Range(center.z - collider.bounds.extents.z, center.z + collider.bounds.extents.z) : Random.Range(center.z - collider.bounds.size.z, center.z + collider.bounds.size.z);
            return new Vector3(x, y, z);
        }
        /// <summary>
        /// Creates a bundle of random world positions around a given world position.
        /// </summary>
        /// <param name="center">The center world position.</param>
        /// <param name="count">The number of random positions to generate.</param>
        /// <param name="radius">The radius around the center to distribute random positions.</param>
        /// <returns>An array of random world positions.</returns>
        public static Vector3[] GenerateRandomBundle(Vector3 center, int count, float radius)
        {
            Vector3[] bundle = new Vector3[count];

            for (int i = 0; i < count; i++)
            {
                float angle = Random.Range(0f, Mathf.PI * 2f); // Random angle
                float distance = Random.Range(0f, radius); // Random distance within radius
                float x = center.x + distance * Mathf.Cos(angle);
                float z = center.z + distance * Mathf.Sin(angle);
                bundle[i] = new Vector3(x, center.y, z); // Keep the same height as the center
            }

            return bundle;
        }
        public static Vector3 GetRandomPointInsideSphere(Vector3 center, float radius)
        {
            Vector3 randomDirection = Random.onUnitSphere;
            float randomDistance = Random.Range(0f, radius);
            return center + randomDirection * randomDistance;
        }
        /// <summary>
        /// Finds a point along a line segment defined by two points at a specified fraction of the way.
        /// </summary>
        /// <param name="startPoint">The start point of the line segment.</param>
        /// <param name="endPoint">The end point of the line segment.</param>
        /// <param name="fraction">The fraction of the way along the line segment (0 to 1).</param>
        /// <returns>The point along the line segment.</returns>
        public static Vector3 GetPointAlongLineSegment(Vector3 startPoint, Vector3 endPoint, float fraction)
        {
            return Vector3.Lerp(startPoint, endPoint, Mathf.Clamp01(fraction));
        }
        /// <summary>
        /// Calculates a point on a cubic Bezier curve defined by four control points.
        /// </summary>
        /// <param name="p0">The first control point.</param>
        /// <param name="p1">The second control point.</param>
        /// <param name="p2">The third control point.</param>
        /// <param name="p3">The fourth control point.</param>
        /// <param name="t">The interpolation value (0 to 1).</param>
        /// <returns>The point on the Bezier curve.</returns>
        public static Vector3 GenerateBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float ttt = tt * t;
            float uuu = uu * u;

            Vector3 point = uuu * p0;
            point += 3 * uu * t * p1;
            point += 3 * u * tt * p2;
            point += ttt * p3;

            return point;
        }
    }
}
