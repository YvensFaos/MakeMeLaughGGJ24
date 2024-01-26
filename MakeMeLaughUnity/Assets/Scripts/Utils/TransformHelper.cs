using UnityEngine;

namespace Utils
{
    public static class TransformHelper
    {
        public static void ClearObjects(Transform transform)
        {
            var children = transform.childCount;
            for (var i = children - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        public static Vector3 MoveInPolarCoordinate(float radius, float angle)
        {
            var angleInRadians = Mathf.Deg2Rad * angle;
            return new Vector3(Mathf.Cos(angleInRadians) * radius, Mathf.Sin(angleInRadians) * radius, 0);
        }

        public static float UnitVectorAngle(Vector2 unitVector) => Mathf.Atan2(unitVector.y, unitVector.x);
    }
}